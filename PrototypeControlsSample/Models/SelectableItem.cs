namespace PrototypeControlsSample.Models
{
    // An example class that implements ISelectableItem - this can be whatever you need though
    public class SelectableItem : BaseNotify, ISelectableItem
    {
        public int Id { get; set; }
        public string Description { get; set; }

        bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetPropertyChanged(ref _isSelected, value);
        }
    }
}
