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
                    DBContext.SaveProducts(productService.GetAllProducts());
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
            IProductModel newProduct;
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
                Console.WriteLine("What is the product type?\n1. Jewlery\n2. Toy\n");
                var productTypeInput = Console.ReadLine();
                if (productTypeInput != "1" && productTypeInput != "2")
                {
                    throw new ArgumentException("Invalid product type. Please enter 1 for Jewlery or 2 for Toy.");
                }
                if (productTypeInput == "1")
                {
                    Console.Write("What is the material of the jewelry? ");
                    var material = Console.ReadLine() ?? throw new ArgumentException("Material cannot be null or empty.");
                    Console.Write("What is the carat of the jewelry? ");
                    var carat = Console.ReadLine() ?? throw new ArgumentException("Carat cannot be null or empty.");
                    newProduct = new JeweleryProductModel(name!, price, ean!, quantity, material, int.Parse(carat));
                    await productService.AddProductAsync(newProduct);
                }
                else if (productTypeInput == "2")
                {
                    Console.Write("What is the age group for the toy? ");
                    var ageGroup = Console.ReadLine() ?? throw new ArgumentException("Age group cannot be null or empty.");
                    Console.Write("What is battery operated toy?\n1. Yes\n2. No\n");
                    var isBatteryOperated = Console.ReadLine() ?? throw new ArgumentException("Battery cannot be null or empty.");
                    if (isBatteryOperated != "1" && isBatteryOperated != "2")
                    {
                        throw new ArgumentException("Invalid input for battery operated. Please enter 1 for Yes or 2 for No.");
                    }
                    if (isBatteryOperated == "1")
                    {
                        isBatteryOperated = "true";
                    }
                    else
                    {
                        isBatteryOperated = "false";
                    }
                    newProduct = new ToyProductModel(name!, price, ean!, quantity, ageGroup, bool.Parse(isBatteryOperated));
                    await productService.AddProductAsync(newProduct);
                }
                else
                {
                    throw new ArgumentException("Invalid product type. Please enter 1 for Jewlery or 2 for Toy.");
                }
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
            IProductModel updatedProduct;
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

                if (existingProduct.GetType() == typeof(JeweleryProductModel))
                {
                    var jewelryProduct = (JeweleryProductModel)existingProduct;
                    Console.Write($"Enter new material (current: {jewelryProduct.Material}): ");
                    var material = Console.ReadLine();
                    Console.Write($"Enter new carat (current: {jewelryProduct.Carat}): ");
                    var carat = int.Parse(Console.ReadLine()!);
                    jewelryProduct.Material = material!;
                    jewelryProduct.Carat = carat;
                    updatedProduct = new JeweleryProductModel(name!, price, ean!, quantity, material!, carat);
                }
                else if (existingProduct.GetType() == typeof(ToyProductModel))
                {
                    var toyProduct = (ToyProductModel)existingProduct;
                    Console.Write($"Enter new age group (current: {toyProduct.AgeGroup}): ");
                    var ageGroup = Console.ReadLine();
                    Console.Write($"Is the toy battery operated? (current: {toyProduct.IsBatteryOperated}) [true/false]: ");
                    var isBatteryOperatedInput = Console.ReadLine();
                    bool isBatteryOperated;
                    if (!bool.TryParse(isBatteryOperatedInput, out isBatteryOperated))
                    {
                        throw new ArgumentException("Invalid input for battery operated. Please enter true or false.");
                    }
                    toyProduct.AgeGroup = ageGroup!;
                    toyProduct.IsBatteryOperated = isBatteryOperated;
                    updatedProduct = new ToyProductModel(name, price, ean, quantity, ageGroup, isBatteryOperated);
                }
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