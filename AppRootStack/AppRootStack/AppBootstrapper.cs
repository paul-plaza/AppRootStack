using AppRootStack.Services;
using AppRootStack.ViewModels;
using AppRootStack.Views;
using ReactiveUI;
using ReactiveUI.XamForms;
using Splat;
using Xamarin.Forms;

namespace AppRootStack
{
    public class AppBootstrapper : ReactiveObject, IScreen
    {
        public AppBootstrapper()
        {
            // IScreen 
            Locator.CurrentMutable.RegisterConstant(this, typeof(IScreen));

            // Services
            RegisterServices();

            // Views and ViewModels
            RegisterViewModelsWithViews();

            // Routing
            Router = new RoutingState();
            Router.NavigationStack.Add(new MainViewModel());
        }

        /// <summary>
        /// Register service with lazy singleton
        /// </summary>
        public void RegisterServices()
        {
            Locator.CurrentMutable.RegisterLazySingleton(() => new DataService(),
                                                            typeof(IDataService));
        }

        /// <summary>
        /// Register an view model for view
        /// </summary>
        public void RegisterViewModelsWithViews()
        {
            Locator.CurrentMutable.RegisterLazySingleton(() => new ListItemsView(),
                                                            typeof(IViewFor<ListItemViewModel>));
            Locator.CurrentMutable.RegisterLazySingleton(() => new MainPage(),
                                                            typeof(IViewFor<MainViewModel>));
        }

        public RoutingState Router { get; protected set; }

        public Page CreateMainPage()
        {
            return new RoutedViewHost();
        }
    }
}
