using InventoryManagement.Database;
using InventoryManagement.Models;
using InventoryManagement.Services;
namespace InventoryManagement.Menus;
public class ProductMenus
{
    public static async Task ShowProductMenuAsync(ProductService productService)
    {
        while (true)
        {
            Console.WriteLine("\n------ Product Management Menu ------");
            Console.WriteLine("Product Menu:");
            Console.WriteLine("1. View All Products");
            Console.WriteLine("2. Search Product by EAN");
            Console.WriteLine("3. Search Product by Name");
            Console.WriteLine("4. Add New Product");
            Console.WriteLine("5. Update Product");
            Console.WriteLine("6. Delete Product");
            Console.WriteLine("7. Exit to Main Menu");
            Console.Write("Select an option: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    ViewAllProducts(productService);
                    break;
                case "2":
                    SearchProductByEan(productService);
                    break;
                case "3":
                    SearchProductByName(productService);
                    break;
                case "4":
                    await AddNewProductAsync(productService);
                    break;
                case "5":
                    await UpdateProductAsync(productService);
                    break;
                case "6":
                    await DeleteProductAsync(productService);
                    break;
                case "7":
                    DBContext.SaveProductModels(productService.GetAllProducts());
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    private static void ViewAllProducts(ProductService productService)
        {
            try
            {
                var products = productService.GetAllProducts();
                foreach (var product in products)
                {
                    Console.WriteLine($"EAN: {product.Ean}\nName: {product.Name}\nPrice: {product.Price}\nQuantity: {product.Quantity}\n---");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void SearchProductByEan(ProductService productService)
        {
            Console.Write("Enter EAN: ");
            try
            {
                var ean = Console.ReadLine() ?? throw new ArgumentException("EAN cannot be null or empty.");
                var product = productService.GetProductByEan(ean);
                Console.WriteLine($"EAN: {product.Ean}\nName: {product.Name}\nPrice: {product.Price}\nQuantity: {product.Quantity}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void SearchProductByName(ProductService productService)
        {
            Console.Write("Enter product name: ");
            try
            {
                var name = Console.ReadLine() ?? throw new ArgumentException("Product name cannot be null or empty.");
                var products = productService.GetProductModelsByName(name);
                foreach (var product in products)
                {
                    Console.WriteLine($"EAN: {product.Ean}\nName: {product.Name}\nPrice: {product.Price}\nQuantity: {product.Quantity}\n---");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static async Task AddNewProductAsync(ProductService productService)
        {
            try
            {
                Console.Write("Enter EAN: ");
                var ean = Console.ReadLine();
                Console.Write("Enter product name: ");
                var name = Console.ReadLine();
                Console.Write("Enter product price: ");
                var price = decimal.Parse(Console.ReadLine()!);
                Console.Write("Enter product quantity: ");
                var quantity = int.Parse(Console.ReadLine()!);

                var newProduct = new ProductModel(name, price, ean, quantity);

                await productService.AddProductAsync(newProduct);
                Console.WriteLine("Product added successfully.");
            }
            
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static async Task UpdateProductAsync(ProductService productService)
        {
            try
            {
                Console.Write("Enter EAN of the product to update: ");
                var ean = Console.ReadLine();
                var existingProduct = productService.GetProductByEan(ean!);

                Console.Write($"Enter new name (current: {existingProduct.Name}): ");
                var name = Console.ReadLine();
                Console.Write($"Enter new price (current: {existingProduct.Price}): ");
                var price = decimal.Parse(Console.ReadLine()!);
                Console.Write($"Enter new quantity (current: {existingProduct.Quantity}): ");
                var quantity = int.Parse(Console.ReadLine()!);

                var updatedProduct = new ProductModel(name, price, ean, quantity);
                await productService.UpdateProductAsync(updatedProduct);
                Console.WriteLine("Product updated successfully.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static async Task DeleteProductAsync(ProductService productService)
        {
            try
            {
                Console.Write("Enter EAN of the product to delete: ");
                var ean = Console.ReadLine();
                await productService.DeleteProductAsync(ean!);
                Console.WriteLine("Product deleted successfully.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
}