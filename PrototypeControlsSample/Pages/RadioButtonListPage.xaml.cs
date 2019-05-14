using PrototypeControlsSample.ViewModels;
using Xamarin.Forms;

namespace PrototypeControlsSample.Pages
{
    public partial class RadioButtonListPage : ContentPage
    {
        public RadioButtonListPage()
        {
            InitializeComponent();
            BindingContext = new RadioButtonListViewModel();
        }
    }
}
