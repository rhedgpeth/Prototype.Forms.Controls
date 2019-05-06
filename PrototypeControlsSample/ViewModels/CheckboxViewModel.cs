using System;
namespace PrototypeControlsSample.ViewModels
{
    public class CheckboxViewModel : BaseNotify
    {
        public string CheckboxTitle => "This is a bound (pre-checked) check box";

        // pre-checked
        bool _isChecked = true;
        public bool IsChecked
        {
            get => _isChecked;
            set => SetPropertyChanged(ref _isChecked, value);
        }

        public CheckboxViewModel()
        { }
    }
}
