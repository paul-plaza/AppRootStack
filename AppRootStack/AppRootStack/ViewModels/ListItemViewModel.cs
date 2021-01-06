using AppRootStack.Models;
using AppRootStack.Services;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace AppRootStack.ViewModels
{
    public class ListItemViewModel : BaseViewModel, IEnableLogger
    {
        #region Services
        private readonly IDataService _dataService;
        #endregion

        #region Properties

        private int currentPage { get; set; } = 1;
        private const int itemsToLoad = 50;

        [Reactive]
        public int ItemTreshold { get; set; }
        [Reactive]
        public bool IsRefreshing { get; set; }

        [Reactive]
        public string SearchText { get; set; }

        protected ObservableAsPropertyHelper<bool> _hasMessage;

        public bool HasMessage => _hasMessage.Value;
        //Campo observable para indicar cuando esta en ejecucion un command
        protected ObservableAsPropertyHelper<bool> _isLoading;

        public bool IsLoading => _isLoading.Value;
        #endregion

        private SourceList<Result> sourceDataList = new SourceList<Result>();

        private readonly ReadOnlyObservableCollection<Result> resultList;

        public ReadOnlyObservableCollection<Result> DataList => resultList;
        public ListItemViewModel(IDataService dataService = null)
        {
            UrlPathSegment = "Lista";
            _dataService = dataService ?? Locator.Current.GetService<IDataService>();

            sourceDataList.Connect()
                //Filtro items si existe algo en el campo de buscar
                .Filter(t => string.IsNullOrEmpty(SearchText) ? true : t.Name.Names.ToLower().Contains(SearchText.ToLower()))
                .Bind(out resultList)
                .Log(this, "Load data")
                .Subscribe();

            this.WhenAnyValue(x => x.SearchText)
                .Throttle(TimeSpan.FromSeconds(.45))
                .InvokeCommand(SearchCommand);


            this.WhenAnyValue(x => x.Message)
                .Select(x => !string.IsNullOrEmpty(x))
                .ToProperty(this, v => v.HasMessage, out _hasMessage);

            SearchCommand.IsExecuting.ToProperty(this, x => x.IsLoading, out _isLoading);
            ItemTresholdReachedCommand.IsExecuting.ToProperty(this, x => x.IsLoading, out _isLoading);
            RefreshDataCommand.IsExecuting.ToProperty(this, x => x.IsLoading, out _isLoading);
        }

        public ReactiveCommand<string, Task> SearchCommand => ReactiveCommand.Create(async (string text) =>
        {
            if (!IsLoading && IfNotFirtsTime)
            {
                //Get items with condition of the current list for display data filtered
                var getItems = sourceDataList.Items.Where(aux => aux.Name.Names.ToLower().Contains(SearchText.ToLower()));
                sourceDataList.Clear();
                sourceDataList.AddRange(getItems);

                //Search items of page 2
                currentPage = 2;
                //load items for more items filtered
                await LoadData();

                //If search text is empty the pagination reset to page number 1
                if (string.IsNullOrEmpty(text))
                {
                    currentPage = 1;
                    //load items for more items filtered
                    await LoadData();
                }
            }
        });

        public ReactiveCommand<Unit, Task> ItemTresholdReachedCommand => ReactiveCommand.Create(async () =>
        {
            if (IsBusy)
                return;

            IsBusy = true;
            IfNotFirtsTime = true;

            try
            {
                var anyItem = await LoadItemsInList();
                //Si ya no existe resultados
                if (!anyItem)
                {
                    ItemTreshold = -1;
                    return;
                }
            }
            catch (Exception ex)
            {
                this.Log().Error($"No se pudo recuperar los datos en {nameof(ListItemViewModel)}", ex);
                Message = ex.Message;
                await Task.Delay(TimeSpan.FromSeconds(4));
                Message = string.Empty;
            }
            finally
            {
                IsBusy = false;
            }
        });


        /// <summary>
        /// Reload data
        /// </summary>
        public ReactiveCommand<Unit, Task> RefreshDataCommand => ReactiveCommand.Create(async () =>
        {
            //reset pagination
            currentPage = 1;
            ItemTreshold = 1;
            sourceDataList.Clear();
            await LoadData();
            IsRefreshing = false;
        });


        public async Task LoadData()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                await LoadItemsInList();
            }
            catch (Exception ex)
            {
                this.Log().Error($"No se pudo recuperar los datos en {nameof(ListItemViewModel)}", ex);
                Message = ex.Message;
                await Task.Delay(TimeSpan.FromSeconds(4));
                Message = string.Empty;
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Load Items from api
        /// </summary>
        /// <returns>returns a boolean indicating if the query had results</returns>
        private async Task<bool> LoadItemsInList()
        {
            var items = await _dataService.GetAll(currentPage, itemsToLoad);
            currentPage = items.Info.Page + 1;
            if (items.Results.Any())
            {
                sourceDataList.AddRange(items.Results);
            }
            this.Log().Info($"{items.Results.Count()} {sourceDataList.Count} ");

            return items.Results.Any();
        }
    }
}
