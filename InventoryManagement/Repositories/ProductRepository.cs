using InventoryManagement.Database;
using InventoryManagement.Models;
namespace InventoryManagement.Repositories;
public class ProductRepository
{
    private List<ProductModel> _products;

    public ProductRepository()
    {
        _products = DBContext.GetProducts();
    }

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

    public List<ProductModel> GetProductByFilter(Func<ProductModel, bool> filter)
    {
        return [.. _products.Where(filter)];
    }

    public async Task AddProductAsync(ProductModel product)
    {
        _products.Add(product);
        DBContext.SaveProducts(_products);
        await Task.Delay(100); // Simulate async work before adding the product
    }

    public async Task<ProductModel?> UpdateProductAsync(ProductModel product)
    {
        await Task.Delay(100); // Simulate async work before adding the product
        var existingProduct = GetProductByEan(product.Ean!);
        if (existingProduct != null)
        {
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Quantity = product.Quantity;
            existingProduct.Ean = product.Ean;
            if (existingProduct.GetType() == typeof(JeweleryProductModel))
            {
                var jewelry = (JeweleryProductModel)existingProduct;
                jewelry.Material = ((JeweleryProductModel)product).Material;
                jewelry.Carat = ((JeweleryProductModel)product).Carat;
                existingProduct = jewelry;
            }
            else if (existingProduct.GetType() == typeof(ToyProductModel))
            {
                var toy = (ToyProductModel)existingProduct;
                toy.AgeGroup = ((ToyProductModel)product).AgeGroup;
                toy.IsBatteryOperated = ((ToyProductModel)product).IsBatteryOperated;
                existingProduct = toy;
            }
            return existingProduct;
        }
        return null;
    }

    public async Task<string?> DeleteProductAsync(string ean)
    {
        await Task.Delay(100); // Simulate async work before adding the product
        var product = GetProductByEan(ean);
        if (product != null)
        {
            _products.Remove(product);
            return ean;
        }
        return null;
    }
}