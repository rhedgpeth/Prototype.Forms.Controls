using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace PrototypeControlsSample.Controls
{
    public partial class EditableLabel : Grid
    {
        public static readonly BindableProperty ValueProperty
            = BindableProperty.Create(nameof(Value),
                                      typeof(string),
                                      typeof(EditableLabel),
                                      string.Empty,
                                      BindingMode.TwoWay,
                                      propertyChanged: HandleValuePropertyChanged);

        public string Value
        {
            get => (string)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        static void HandleValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var editableLabel = bindable as EditableLabel;
            editableLabel.entry.Text = editableLabel.label.Text = (string)newValue;
        }

        public static readonly BindableProperty EditIconProperty
                        = BindableProperty.Create(nameof(EditIcon),
                                                  typeof(string),
                                                  typeof(EditableLabel),
                                                  string.Empty,
                                                  propertyChanged: HandleEditIconPropertyChanged);

        public bool EditIcon
        {
            get { return (bool)GetValue(EditIconProperty); }
            set { SetValue(EditIconProperty, value); }
        }

        static void HandleEditIconPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var editableLabel = bindable as EditableLabel;
            editableLabel.imageEditIcon.Source = (string)newValue;
        }

        public static readonly BindableProperty ValueChangedCommandProperty =
            BindableProperty.Create(nameof(ValueChangedCommand), typeof(ICommand), typeof(EditableLabel));

        public ICommand ValueChangedCommand
        {
            get { return (ICommand)GetValue(ValueChangedCommandProperty); }
            set { SetValue(ValueChangedCommandProperty, value); }
        }

        public static readonly BindableProperty ValueChangedCommandParameterProperty =
            BindableProperty.Create("CommandParameter", typeof(object), typeof(EditableLabel), null);

        public object ValueChangedCommandParameter
        {
            get { return GetValue(ValueChangedCommandParameterProperty); }
            set { SetValue(ValueChangedCommandParameterProperty, value); }
        }

        public EditableLabel()
        {
            InitializeComponent();
        }

        void Handle_Tapped(object sender, EventArgs e)
        {
            if (label.IsVisible)
            {
                label.IsVisible = false;
                entry.IsVisible = true;
                entry.Focus();
            }
            else
            {
                if (label.Text != entry.Text)
                {
                    Value = label.Text = entry.Text;
                    ValueChangedCommand?.Execute(ValueChangedCommandParameter);
                }

                label.IsVisible = true;
                entry.IsVisible = false;
            }
        }
    }
}
