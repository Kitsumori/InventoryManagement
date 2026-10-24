namespace InventoryManagement.Models;
public abstract class ProductModel(string ean, string name, decimal price, int quantity) : IProductModel
{
    private string Id { get; set; } = Guid.NewGuid().ToString();
    public string Ean { get; set; } = ean;
    public string Name { get; set; } = name;
    public decimal Price { get; set; } = price;
    public int Quantity { get; set; } = quantity;

    public abstract string GetSpecificDetails();
}