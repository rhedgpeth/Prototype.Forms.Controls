using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PrototypeControlsSample.Models;

namespace PrototypeControlsSample.ViewModels
{
    public class MainViewModel : BaseNotify
    {
        public string CheckboxTitle => "This is a bound (pre-checked) check box";

        // pre-checked
        bool _isChecked = true;
        public bool IsChecked
        {
            get => _isChecked;
            set => SetPropertyChanged(ref _isChecked, value);
        }

        ObservableCollection<SelectableItem> _items = new ObservableCollection<SelectableItem>();
        public ObservableCollection<SelectableItem> Items
        {
            get => _items;
            set => SetPropertyChanged(ref _items, value);
        }

        List<SelectableItem> _selectedItems;
        public List<SelectableItem> SelectedItems
        {
            get => _selectedItems;
            set
            {
                SetPropertyChanged(ref _selectedItems, value);
                SetPropertyChanged(nameof(ItemsSelectedDescription));
            }
        }

        public string ItemsSelectedDescription
        {
            get
            {
                return $"{SelectedItems?.Count ?? 0} items selected";
            }
        }


        public MainViewModel()
        {
            for (int i = 0; i < 10; i++)
            {
                Items.Add(new SelectableItem { Id = i, Description = $"Item {i}" });
            }
        }
    }
}
