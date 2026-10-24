using InventoryManagement.Models;

namespace InventoryManagement.Services;
public interface IProductService
{
    List<ProductModel> GetAllProducts();
    ProductModel GetProductByEan(string ean);
    List<ProductModel> GetProductModelsByName(string name);
    Task AddProductAsync(ProductModel product);
    Task<ProductModel> UpdateProductAsync(ProductModel product);
}