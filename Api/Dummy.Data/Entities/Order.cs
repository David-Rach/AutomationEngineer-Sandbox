namespace Dummy.Data.Entities;

public class Order
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public string Title { get; set; }
    public Item Item { get; set; } = null!;
    public int Quantity { get; set; }
}