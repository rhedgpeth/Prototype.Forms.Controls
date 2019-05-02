namespace PrototypeControlsSample.Models
{
    public class SelectableItem : BaseNotify
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
