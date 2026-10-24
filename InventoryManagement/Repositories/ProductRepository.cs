using InventoryManagement.Database;
using InventoryManagement.Models;
namespace InventoryManagement.Repositories;
public class ProductRepository
{
    private List<ProductModel> _products => DBContext.GetProductModels();

    public List<ProductModel> GetAllProducts()
    {
        return _products;
    }

    public ProductModel? GetProductByEan(string ean)
    {
        return _products.FirstOrDefault(p => p.Ean == ean);
    }

    public List<ProductModel> GetProductByName(string name)
    {
        return [.. _products.Where(p => p.Name!.Contains(name, StringComparison.OrdinalIgnoreCase))];
    }

    public async Task AddProductAsync(ProductModel product)
    {
        _products.Add(product);
    }

    public async Task<ProductModel?> UpdateProductAsync(ProductModel product)
    {
        var existingProduct = GetProductByEan(product.Ean!);
        if (existingProduct != null)
        {
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Quantity = product.Quantity;
            existingProduct.Ean = product.Ean;
            return product;
        }
        return null;
    }

    public async Task<string?> DeleteProductAsync(string ean)
    {
        var product = GetProductByEan(ean);
        if (product != null)
        {
            _products.Remove(product);
            return ean;
        }
        return null;
    }
}