namespace InventoryManagement.Models;

public class JeweleryProductModel(string ean, string name, decimal price, int quantity, string material, int carat) : ProductModel(ean, name, price, quantity)
{
    public string Material { get; set; } = material;
    public int Carat { get; set; } = carat;


    public override string GetSpecificDetails() => $"Carat: {Carat}\nMaterial: {Material}";

}