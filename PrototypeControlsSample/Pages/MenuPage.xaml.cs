using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace PrototypeControlsSample.Pages
{
    public partial class MenuPage : ContentPage
    {
        Action<Type> NavigateTo { get; set; }

        public MenuPage(Action<Type> navigateTo)
        {
            InitializeComponent();

            NavigateTo = navigateTo;

            LoadMenuItems();
        }

        void LoadMenuItems()
        {
            listView.ItemsSource = new List<Models.MenuItem>
            {
                GetMenuItem("Home", typeof(HomePage)),
                GetMenuItem("Checkbox", typeof(CheckboxPage)),
                GetMenuItem("CheckboxList", typeof(CheckboxListPage)),
                GetMenuItem("EditableLabel", typeof(EditableLabelPage)),
                GetMenuItem("ListView", typeof(ListViewPage)),
                GetMenuItem("RadioButtonList", typeof(RadioButtonListPage)),
                GetMenuItem ("TabControl", typeof(TabControlPage)),
                GetMenuItem("TappableLabel", typeof(TappableLabelPage))
            };
        }

        Models.MenuItem GetMenuItem(string title, Type type)
        {
            return new Models.MenuItem
            {
                Title = title,
                Type = type
            };
        }

        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is Models.MenuItem menuItem)
            {
                NavigateTo(menuItem.Type);
            }
        }
    }
}
