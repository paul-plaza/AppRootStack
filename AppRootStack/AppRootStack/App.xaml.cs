using Xamarin.Forms;

namespace AppRootStack
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var bootstrapper = new AppBootstrapper();
            MainPage = bootstrapper.CreateMainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
