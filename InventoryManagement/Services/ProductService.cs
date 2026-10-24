using InventoryManagement.Models;
using InventoryManagement.Repositories;

namespace InventoryManagement.Services;
public class ProductService : IProductService
{
    private readonly ProductRepository _productRepository;

    public ProductService(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public List<ProductModel> GetAllProducts()
    {
        return _productRepository.GetAllProducts();
    }

    public ProductModel GetProductByEan(string ean)
    {
        return _productRepository.GetProductByEan(ean) ?? throw new ArgumentException($"Product with ID {ean} not found.");
    }

    public List<ProductModel> GetProductModelsByName(string name)
    {
        var products = _productRepository.GetProductByName(name);
        if (products.Count == 0)
        {
            throw new ArgumentException($"No products found with name containing '{name}'.");
        }
        return products;
    }

    public List<ProductModel> GetProductModelsByFilter(string filter)
    {
        List<ProductModel> products = [];
        if (string.IsNullOrWhiteSpace(filter))
        {
            throw new ArgumentException("Filter cannot be null or empty.");
        }
        if (filter == "Jewlery") products = [.. _productRepository.GetProductByFilter(p => p.GetType() == typeof(JeweleryProductModel))];
        if (filter == "Toy") products = [.. _productRepository.GetProductByFilter(p => p.GetType() == typeof(ToyProductModel))];
        if (products.Count == 0)
        {
            throw new ArgumentException($"No products found with filter '{filter}'.");
        }
        return products;
    }

    public async Task AddProductAsync(ProductModel product)
    {
        if (string.IsNullOrWhiteSpace(product.Name))
        {
            throw new ArgumentException("Product name cannot be null or empty.");
        }
        if (product.Price < 0)
        {
            throw new ArgumentException("Product price cannot be negative.");
        }
        if (product.Quantity < 0)
        {
            throw new ArgumentException("Product quantity cannot be negative.");
        }
        if (string.IsNullOrWhiteSpace(product.Ean))
        {
            throw new ArgumentException("Product EAN cannot be null or empty.");
        }
        if (product.Ean.Length != 13)
        {
            throw new ArgumentException("Product EAN must be 13 characters long.");
        }
        await Task.Delay(100); // Simulate async work before adding the product
        await _productRepository.AddProductAsync(product);
    }

    public async Task<ProductModel> UpdateProductAsync(ProductModel product)
    {
        if (string.IsNullOrWhiteSpace(product.Name))
        {
            throw new ArgumentException("Product name cannot be null or empty.");
        }
        if (product.Price < 0)
        {
            throw new ArgumentException("Product price cannot be negative.");
        }
        if (product.Quantity < 0)
        {
            throw new ArgumentException("Product quantity cannot be negative.");
        }
        if (string.IsNullOrWhiteSpace(product.Ean))
        {
            throw new ArgumentException("Product EAN cannot be null or empty.");
        }
        if (product.Ean.Length != 13)
        {
            throw new ArgumentException("Product EAN must be 13 characters long.");
        }
        await Task.Delay(100); // Simulate async work before adding the product
        return await _productRepository.UpdateProductAsync(product) ?? throw new ArgumentException($"Product with ID {product.Id} not found.");
    }

    public async Task<string> DeleteProductAsync(string ean)
    {
        await Task.Delay(100); // Simulate async work before adding the product
        return await _productRepository.DeleteProductAsync(ean) ?? throw new ArgumentException($"Product with ID {ean} not found.");
    }
}