namespace AppRootStack.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ListItemViewModel Child1 => new ListItemViewModel();

        public MainViewModel()
        {
            UrlPathSegment = "Tabbed Page";
        }
    }
}
