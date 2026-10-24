namespace InventoryManagement.Models;
public interface IProductModel
{
    string Id { get; set; }
    string? Name { get; set; }
    decimal Price { get; set; }
    int Quantity { get; set; }
    string? Ean { get; set; }
}