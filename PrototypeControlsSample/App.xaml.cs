using Xamarin.Forms;

namespace PrototypeControlsSample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new Pages.MainPage();

            MainPage = new Pages.RootPage();
        }
    }
}
