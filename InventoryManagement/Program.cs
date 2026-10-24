using InventoryManagement.Menus;
using InventoryManagement.Services;
using InventoryManagement.Repositories;

var repository = new ProductRepository();
var service = new ProductService(repository);
await ProductMenus.ShowProductMenuAsync(service);