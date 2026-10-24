using System.Text.Json;
using System.Text.Json.Serialization;

namespace InventoryManagement.Models;
[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(ToyProductModel), "toy")]
[JsonDerivedType(typeof(JeweleryProductModel), "jewelery")]
public interface IProductModel
{
    string Name { get; set; }
    decimal Price { get; set; }
    int Quantity { get; set; }
    string Ean { get; set; }
}