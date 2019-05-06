using System;
using System.Collections.Generic;
using PrototypeControlsSample.ViewModels;
using Xamarin.Forms;

namespace PrototypeControlsSample.Pages
{
    public partial class CheckboxPage : ContentPage
    {
        public CheckboxPage()
        {
            InitializeComponent();

            BindingContext = new CheckboxViewModel();
        }
    }
}
