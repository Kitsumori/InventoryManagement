using System.Text.Json;
using InventoryManagement.Models;
namespace InventoryManagement.Database
{
    public class DBContext
    {
        private static readonly string _connectionString = string.Join("/",Directory.GetCurrentDirectory(),"InventoyDatabase.json" );

        public static List<ProductModel> GetProducts()
        {
            if (!File.Exists(_connectionString))
            {
                File.WriteAllText(_connectionString, "[]");
            }

            var json = File.ReadAllText(_connectionString);
            return JsonSerializer.Deserialize<List<ProductModel>>(json) ?? [];
        }

        public static void SaveProducts(List<ProductModel> products)
        {
            var json = JsonSerializer.Serialize(products, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_connectionString, json);
        }

    }
}