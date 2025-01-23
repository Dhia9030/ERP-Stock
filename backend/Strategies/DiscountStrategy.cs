using StockManagement.Models;

public class PercentageDiscountStrategy 
{
    public static decimal CalculateDiscount(decimal price, double discountPercentage)
    {
        if (discountPercentage == 0)
        {
            return 0;
        }
        return price * (decimal)discountPercentage / 100;
    }
}
