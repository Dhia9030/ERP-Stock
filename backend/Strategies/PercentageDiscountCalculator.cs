using StockManagement.Models;

public class PercentageDiscountCalculator 
{
    public static decimal CalculateDiscount(decimal price, double discountPercentage)
    {
        if (discountPercentage == 0)
        {
            return 0;
        }
        return price * (decimal)discountPercentage / 100;
    }
    
    public static decimal CalculateTotal(List<Product> products, double discountPercentageOrder)
    {
        decimal total = 0;
        foreach (var product in products)
        {
            total += product.Price - PercentageDiscountCalculator.CalculateDiscount(product.Price, product.DiscountPercentage);
        }

        return total - PercentageDiscountCalculator.CalculateDiscount(total, discountPercentageOrder);
    }
}
