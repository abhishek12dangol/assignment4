using Assignment4_part_1.Models;
using Assignment4_part_1.EF;
using Assignment4_part_1.Service;
using Assignment4_part_1.DTOs;


var db = new NorthwindContext();
var service = new DataService();

var value = "em";
var query = db.Products.Where(p => p.Name.Contains(value)).Select(p => p.Name).ToList();
foreach (var item in query)
{
    Console.WriteLine(item);
}