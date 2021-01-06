using AppRootStack.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;
using System.Reactive.Disposables;
using Xamarin.Forms.Xaml;

namespace AppRootStack
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ReactiveTabbedPage<MainViewModel>
    {
        public MainPage()
        {
            InitializeComponent();
            //Inicializo y vinculo viewmodels
            this.WhenActivated(
            disposables =>
            {
                this
                    .OneWayBind(ViewModel, x => x.Child1, x => x.ChildListItemsView.ViewModel)
                    .DisposeWith(disposables);
            });
        }
    }
}