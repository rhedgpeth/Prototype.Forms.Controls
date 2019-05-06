namespace PrototypeControlsSample.Models
{
    public interface ISelectableItem
    {
        int Id { get; set; }
        string Description { get; set; }
        bool IsSelected { get; set; }
    }
}
