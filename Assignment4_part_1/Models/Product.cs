namespace Assignment4_part_1.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string QuantityPerUnit { get; set; }
    private int _unitPrice;

    public int UnitPrice
    {
        get => _unitPrice;
        set
        {
            if (value <= 0) throw new ArgumentException("Price cannot be negative!", nameof(UnitPrice));
            _unitPrice = value;
        }
    }

    private int _unitsInStock;
    public int UnitsInStock
    {
        get => _unitsInStock;
        set
        {
            if (value <= 0) throw new ArgumentException("Quantity cannot be negative!", nameof(UnitsInStock));
            _unitsInStock = value;
        }
    }
    public Category Category { get; set; }
    public int CategoryId { get; set; }
}