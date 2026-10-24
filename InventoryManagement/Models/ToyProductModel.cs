namespace InventoryManagement.Models;
public class ToyProductModel : IProductModel
{
    public string Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string? Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public decimal Price { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public int Quantity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string? Ean { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string AgeGroup { get; set; } = string.Empty;
    public bool IsBatteryOperated { get; set; }

    public ToyProductModel(string name, decimal price, string ean, int quantity, string ageGroup, bool isBatteryOperated)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
        Price = price;
        Ean = ean;
        Quantity = quantity;
        AgeGroup = ageGroup;
        IsBatteryOperated = isBatteryOperated;
    }
}