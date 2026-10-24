using System.Text.Json;
using InventoryManagement.Models;
namespace InventoryManagement.Database
{
    public class DBContext
    {
        private static readonly string _connectionString = "InventoyDatabase.json";

        public static List<ProductModel> GetProductModels()
        {
            if (!File.Exists(_connectionString))
            {
                File.WriteAllText(_connectionString, "[]");
            }

            var json = File.ReadAllText(_connectionString);
            return JsonSerializer.Deserialize<List<ProductModel>>(json) ?? [];
        }

        public static void SaveProductModels(List<ProductModel> products)
        {
            var json = JsonSerializer.Serialize(products, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_connectionString, json);
        }

    }
}