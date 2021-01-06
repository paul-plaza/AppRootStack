using AppRootStack.ViewModels;
using ReactiveUI.XamForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppRootStack.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListItemsView : ReactiveContentPage<ListItemViewModel>
    {
        public ListItemsView()
        {
            InitializeComponent();
        }
    }
}