namespace InventoryManagement.Models;

public class JeweleryProductModel : IProductModel
{
    public string Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string? Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public decimal Price { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public int Quantity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string? Ean { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string Material { get; set; } = string.Empty;
    public int Carat { get; set; }

    public JeweleryProductModel(string name, decimal price, string ean,int quantity, string material, int carat)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
        Price = price;
        Ean = ean;
        Quantity = quantity;
        Material = material;
        Carat = carat;
    }
}