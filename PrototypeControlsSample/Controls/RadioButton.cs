using System;
using PrototypeControlsSample.Events;
using Xamarin.Forms;

namespace PrototypeControlsSample.Controls
{
    public class RadioButton : View
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text),
                                                                                        typeof(string),
                                                                                        typeof(Checkbox),
                                                                                        default(string),
                                                                                        propertyChanged: HandleTextPropertyChanged);

        static void HandleTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var rb = bindable as RadioButton;
            rb.Text = newValue as string;
        }

        public static readonly BindableProperty CheckedProperty = BindableProperty.Create(nameof(Checked),
                                                                                        typeof(bool),
                                                                                        typeof(RadioButton),
                                                                                        default(bool), BindingMode.TwoWay,
                                                                                        propertyChanged: HandleCheckedPropertyChanged);

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create("TextColor", typeof(Color), typeof(RadioButton), Color.Black);

        static void HandleCheckedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var cb = bindable as RadioButton;
            cb.Checked = (bool)newValue;
        }

        public EventHandler<EventArgs<bool>> CheckedChanged;

        public new int Id { get; set; }

        public bool Checked
        {
            get => (bool)GetValue(CheckedProperty);
            set
            {
                SetValue(CheckedProperty, value);
                CheckedChanged?.Invoke(this, value);
            }
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }
    }
}
