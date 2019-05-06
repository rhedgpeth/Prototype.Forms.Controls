using System;
using System.Collections.Generic;
using PrototypeControlsSample.ViewModels;
using Xamarin.Forms;

namespace PrototypeControlsSample.Pages
{
    public partial class CheckboxListPage : ContentPage
    {
        public CheckboxListPage()
        {
            InitializeComponent();
            BindingContext = new CheckboxListViewModel();
        }
    }
}
