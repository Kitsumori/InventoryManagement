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

    public List<IProductModel> GetAllProducts()
    {
        return _productRepository.GetAllProducts();
    }

    public IProductModel GetProductByEan(string ean)
    {
        return _productRepository.GetProductByEan(ean) ?? throw new ArgumentException($"Product with ID {ean} not found.");
    }

    public List<IProductModel> GetProductModelsByName(string name)
    {
        var products = _productRepository.GetProductByName(name);
        if (products.Count == 0)
        {
            throw new ArgumentException($"No products found with name containing '{name}'.");
        }
        return products;
    }

    public async Task AddProductAsync(IProductModel product)
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
        await _productRepository.AddProductAsync(product);
    }

    public async Task<IProductModel> UpdateProductAsync(IProductModel product)
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
        return await _productRepository.UpdateProductAsync(product) ?? throw new ArgumentException($"Product with ID {product.Id} not found.");
    }

    public async Task<string> DeleteProductAsync(string ean)
    {
        return await _productRepository.DeleteProductAsync(ean) ?? throw new ArgumentException($"Product with ID {ean} not found.");
    }
}