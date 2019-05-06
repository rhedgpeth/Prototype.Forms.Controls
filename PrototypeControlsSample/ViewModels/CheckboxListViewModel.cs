using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PrototypeControlsSample.Models;

namespace PrototypeControlsSample.ViewModels
{
    public class CheckboxListViewModel : BaseNotify
    {
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


        public CheckboxListViewModel()
        {
            for (int i = 0; i < 10; i++)
            {
                Items.Add(new SelectableItem { Id = i, Description = $"Item {i}" });
            }
        }
    }
}
