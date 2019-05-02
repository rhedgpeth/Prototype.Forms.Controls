using PrototypeControlsSample.ViewModels;
using Xamarin.Forms;

namespace PrototypeControlsSample.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainViewModel();
        }
    }
}
