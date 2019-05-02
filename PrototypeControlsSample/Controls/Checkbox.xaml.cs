using System.Windows.Input;
using Xamarin.Forms;

namespace PrototypeControlsSample.Controls
{
    public partial class Checkbox : StackLayout
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text),
                                                                                        typeof(string),
                                                                                        typeof(Checkbox),
                                                                                        default(string),
                                                                                        propertyChanged: HandleTextPropertyChanged);

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        static void HandleTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var cb = bindable as Checkbox;
            cb.descriptionLabel.Text = newValue as string;
        }

        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked),
                                                                                        typeof(bool),
                                                                                        typeof(Checkbox),
                                                                                        default(bool), BindingMode.TwoWay,
                                                                                        propertyChanged: HandleIsCheckedPropertyChanged);

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        static void HandleIsCheckedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var cb = bindable as Checkbox;
            cb.toggleButton.Checked = (bool)newValue;
        }

        ICommand _checkedCommand;
        public ICommand CheckedCommand
        {
            get
            {
                if (_checkedCommand == null)
                {
                    _checkedCommand = new Command(() =>
                    {
                        IsChecked = toggleButton.Checked;
                    });
                }

                return _checkedCommand;
            }
        }

        public Checkbox()
        {
            InitializeComponent();
        }
    }
}
