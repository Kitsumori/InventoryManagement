using InventoryManagement.Database;
using InventoryManagement.Models;
namespace InventoryManagement.Repositories;
public class ProductRepository
{
    private List<IProductModel> _products => DBContext.GetProducts();

    public List<IProductModel> GetAllProducts()
    {
        return _products;
    }

    public IProductModel? GetProductByEan(string ean)
    {
        return _products.FirstOrDefault(p => p.Ean == ean);
    }

    public List<IProductModel> GetProductByName(string name)
    {
        return [.. _products.Where(p => p.Name!.Contains(name, StringComparison.OrdinalIgnoreCase))];
    }

    public List<IProductModel> GetProductByFilter(Func<IProductModel, bool> filter)
    {
        return [.. _products.Where(filter)];
    }

    public async Task AddProductAsync(IProductModel product)
    {
        _products.Add(product);
        DBContext.SaveProducts(_products);
    }

    public async Task<IProductModel?> UpdateProductAsync(IProductModel product)
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