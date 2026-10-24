namespace InventoryManagement.Models;
public class ToyProductModel(string ean, string name, decimal price, int quantity, string ageGroup, bool isBatteryOperated) : ProductModel(ean, name, price, quantity)
{
    public string AgeGroup { get; set; } = ageGroup;
    public bool IsBatteryOperated { get; set; } = isBatteryOperated;

    public override string GetSpecificDetails()
    {
        return $"Age Group: {AgeGroup}\nBattery Operated: {IsBatteryOperated}";
    }
}