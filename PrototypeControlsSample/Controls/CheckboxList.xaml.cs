using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Xamarin.Forms;
using PrototypeControlsSample.Models;

namespace PrototypeControlsSample.Controls
{
    public partial class CheckboxList : Xamarin.Forms.ListView
    {
        public string ItemTypeDescription { get; set; }

        public static readonly BindableProperty SelectedItemsProperty
                = BindableProperty.Create(nameof(SelectedItems),
                                        typeof(List<SelectableItem>),
                                          typeof(CheckboxList),
                                          new List<SelectableItem>(), BindingMode.TwoWay,
                                        propertyChanged: HandleSelectedItemsPropertyChanged);

        List<SelectableItem> _selectedItems = new List<SelectableItem>();

        public List<SelectableItem> SelectedItems
        {
            get { return (List<SelectableItem>)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        static void HandleSelectedItemsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var checkboxList = bindable as CheckboxList;

            if (newValue != null)
            {
                var item = newValue as List<SelectableItem>;

                if (item.Count > 0)
                {
                    checkboxList.UpdateSelectedItemsDescription(item[0].Description);
                }
            }
        }

        public static readonly BindableProperty SelectedItemsDescriptionProperty
                = BindableProperty.Create(nameof(SelectedItemsDescription),
                                          typeof(string),
                                        typeof(CheckboxList), default(string), BindingMode.TwoWay); 

        public string SelectedItemsDescription
        {
            get { return (string)GetValue(SelectedItemsDescriptionProperty); }
            set { SetValue(SelectedItemsDescriptionProperty, value); }
        }

        public CheckboxList()
        {
            InitializeComponent();
        }

        void UpdateSelectedItemsDescription(string singleDescription)
        {
            if (SelectedItems.Count == 0 || SelectedItems.Count == _totalItemCount)
            {
                SelectedItemsDescription = "All";
            }
            else if (SelectedItems.Count == 1)
            {
                SelectedItemsDescription = singleDescription;
            }
            else
            {
                SelectedItemsDescription = $"{SelectedItems.Count} {ItemTypeDescription}";
            }
        }

        int _totalItemCount;

        void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            SelectedItem = null;
        }

        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            _totalItemCount = 0;

            if (e.Item is SelectableItem item)
            {
                item.IsSelected = !item.IsSelected;

                var pInfos = (this as ItemsView<Cell>).GetType().GetRuntimeProperties();

                var templatedItems = pInfos.FirstOrDefault(info => info.Name == "TemplatedItems");

                if (templatedItems != null)
                {
                    SelectableItem rootItem = null;

                    bool allChecked = true;
                    bool allUnchecked = true;

                    var cells = templatedItems.GetValue(this);

                    foreach (var cell in cells as ITemplatedItemsList<Cell>)
                    {
                        if (cell?.BindingContext is SelectableItem target)
                        {
                            if (item.Id == -1)
                            {
                                target.IsSelected = item.IsSelected;
                            }
                            else if (target.Id == -1)
                            {
                                rootItem = target;
                            }
                            else
                            {
                                if (allChecked)
                                {
                                    allChecked = target.IsSelected;
                                }

                                if (allUnchecked)
                                {
                                    allUnchecked = !target.IsSelected;
                                }
                            }

                            if (target.Id >= 0)
                            {
                                _totalItemCount++;

                                if (target.IsSelected && !_selectedItems.Contains(target))
                                {
                                    _selectedItems.Add(target);
                                }
                                else if (!target.IsSelected && _selectedItems.Contains(target))
                                {
                                    _selectedItems.Remove(target);
                                }
                            }
                        }
                    }

                    // Force the update
                    SelectedItems = null;
                    SelectedItems = _selectedItems;

                    UpdateSelectedItemsDescription(item.Description);

                    if (rootItem != null)
                    {
                        rootItem.IsSelected = (allChecked || allUnchecked);
                    }
                }
            }
        }
    }
}