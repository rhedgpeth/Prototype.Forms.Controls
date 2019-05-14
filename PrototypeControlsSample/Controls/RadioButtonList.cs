using System;
using System.Collections;
using System.Collections.Generic;
using PrototypeControlsSample.Events;
using Xamarin.Forms;

namespace PrototypeControlsSample.Controls
{
    public class RadioButtonList : StackLayout
    {
        public List<RadioButton> rads;

        public RadioButtonList()
        {
            rads = new List<RadioButton>();
        }

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource),
                                                                                        typeof(IEnumerable),
                                                                                        typeof(RadioButtonList),
                                                                                        default(IEnumerable),
                                                                                        propertyChanged: OnItemsSourceChanged);
                                                                                        
        public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(nameof(SelectedIndex),
                                                                                        typeof(int),
                                                                                        typeof(RadioButtonList),
                                                                                        default(int),
                                                                                        BindingMode.TwoWay,
                                                                                        propertyChanged: OnSelectedIndexChanged);

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }


        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        public event EventHandler<int> CheckedChanged;

        static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var radButtons = bindable as RadioButtonList;

            radButtons.rads.Clear();
            radButtons.Children.Clear();

            if (newValue != null)
            {
                int radIndex = 0;

                if (newValue is IEnumerable enumerables)
                {
                    foreach (var item in newValue as IEnumerable)
                    {
                        var rad = new RadioButton
                        {
                            Text = item.ToString(),
                            Id = radIndex
                        };

                        rad.CheckedChanged += radButtons.OnCheckedChanged;

                        radButtons.rads.Add(rad);

                        radButtons.Children.Add(rad);
                        radIndex++;
                    }
                }
            }
        }

        void OnCheckedChanged(object sender, EventArgs<bool> e)
        {
            if (e.Value == false)
            {
                return;
            }

            var selectedRad = sender as RadioButton;

            foreach (var rad in rads)
            {
                if (!selectedRad.Id.Equals(rad.Id))
                {
                    rad.Checked = false;
                }
                else
                {
                    if (CheckedChanged != null)
                    {
                        CheckedChanged.Invoke(sender, rad.Id);
                    }
                }
            }
        }

        static void OnSelectedIndexChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if ((int)newValue == -1)
            {
                return;
            }

            var bindableRadioGroup = bindable as RadioButtonList;

            foreach (var rad in bindableRadioGroup.rads)
            {
                if (rad.Id == bindableRadioGroup.SelectedIndex)
                {
                    rad.Checked = true;
                }
            }
        }
    }
}
