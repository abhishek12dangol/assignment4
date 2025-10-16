using Assignment4_part_1.Models;
using Assignment4_part_1.EF;
using Assignment4_part_1.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Assignment4_part_1.Service;

public interface IDataService
{
   Product GetProduct(int id);
   List<Category> GetCategories();
   Category GetCategory(int id);
   Category CreateCategory(string name, string description);
   bool DeleteCategory(int id);
   bool UpdateCategory(int id, string newName, string newDescription);
  List<ProductByCategoryDTO> GetProductByCategory(int id);
   List<ProductByNameDTO> GetProductByName(string name);
   Order GetOrder(int id);
   List<Order> GetOrders();
   List<OrderDetails> GetOrderDetailsByOrderId(int id);
   List<OrderDetails> GetOrderDetailsByProductId(int productId);
}

public class DataService : IDataService
{
   private NorthwindContext _db;

   public DataService()
   {
      _db = new NorthwindContext();
   }

   public Product GetProduct(int id)
   {
      return _db.Products.Where(p => p.Id == id)
         .Select(p => new Product
         {
            Id = p.Id,
            Name = p.Name,
            UnitPrice = p.UnitPrice,
            QuantityPerUnit = p.QuantityPerUnit,
            UnitsInStock = p.UnitsInStock,
            CategoryId = p.CategoryId,
            Category = new Category
            {
               Id = p.Category.Id,
               Name = p.Category.Name,
               Description = p.Category.Description
            }
         }).FirstOrDefault();
   }

   public List<Category> GetCategories()
   {
      return _db.Categories.Where(c => c.Id >= 1 && c.Id <= 8).OrderBy(c => c.Name).ToList();
   }

   public Category GetCategory(int id)
   {
      return _db.Categories.SingleOrDefault(c => c.Id == id);
   }

   public Category CreateCategory(string name, string description)
   {
      var nextId = (_db.Categories.OrderByDescending(c => c.Id).Select(c => c.Id).Max()) + 1;
      
      var category = new Category { Id = nextId, Name = name, Description = description };
      _db.Categories.Add(category);
      _db.SaveChanges();
      return category;
   }

   public bool DeleteCategory(int id)
   {
      var cat = _db.Categories.Find(id);
      if (cat is null) return false;
      _db.Categories.Remove(cat);
      _db.SaveChanges();
      return true;
   }

   public bool UpdateCategory(int id, string newName, string newDescription)
   {
      var category = _db.Categories.FirstOrDefault(c => c.Id == id);
      if (category is null) return false;
      category.Name = newName;
      category.Description = newDescription;
      _db.SaveChanges();
      return true;
   }

   public List<ProductByNameDTO> GetProductByName(string name)
   {
      return _db.Products.Where(p => p.Name.Contains(name)).Select(p => new ProductByNameDTO
      {
         ProductName = p.Name
      }).ToList();
   }

   public List<ProductByCategoryDTO> GetProductByCategory(int id)
   {
      return _db.Products.Where(p => p.CategoryId == id).Select(p => new ProductByCategoryDTO
      {
         Name = p.Name,
         CategoryName = p.Category.Name
      }).ToList();
   }

   public Order GetOrder(int id)
   {
      var order = _db.Orders.Include(o => o.OrderDetails)
         .ThenInclude(od => od.Product)
         .ThenInclude(p => p.Category).SingleOrDefault(o => o.Id == id);
      return order;
   }

   public List<Order> GetOrders()
   {
      return _db.Orders.ToList();
   }

   public List<OrderDetails> GetOrderDetailsByOrderId(int id)
   {
      var query = _db.OrderDetails.Where(od => od.OrderId == id)
         .Include(od => od.Product).ToList();
      return query;
   }

   public List<OrderDetails> GetOrderDetailsByProductId(int productId)
   {
      var query = _db.OrderDetails.Where(od => od.ProductId == productId)
         .Include(od => od.Order).ToList();
      return query;
   }
   

}