namespace Assignment4_part_1.Models;

public class OrderDetails
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }

    private int _unitPrice;

    public int UnitPrice
    {
        get => _unitPrice;
        set
        {
            if (value < 0) throw new ArgumentException("Price cannot be negative", nameof(UnitPrice));
            _unitPrice = value;
        }

    }

    private int _unitQuantity;
    public int Quantity
    {
        get => _unitQuantity;
        set
        {
            if (value <= 0) throw new ArgumentException("Quantity cannot be less than zero", nameof(Quantity));
            _unitQuantity = value;
        }
    }

    private int _discount;

    public int Discount
    {
        get => _discount;
        set
        {
            if (value < 0 || value > 1) throw new ArgumentException("Discount must be 0 and 1", nameof(Discount));
            _discount = value;
        }
    }

    public Order Order { get; set; }
    public Product Product { get; set; }
}