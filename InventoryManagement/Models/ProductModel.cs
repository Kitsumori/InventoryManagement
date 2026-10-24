namespace InventoryManagement.Models
{
    public class ProductModel
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Ean {get; set; }

        public ProductModel(string? name, decimal price, string? ean, int quantity = 0)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Price = price;
            Quantity = quantity;
            Ean = ean;
        }
    }
}