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
    
    public static decimal CalculateTotal(List<ProductItem> products, double discountPercentageOrder)
    {
        decimal total = 0;
        foreach (var productItem in products)
        {
            total += productItem.Product.Price - PercentageDiscountCalculator.CalculateDiscount(productItem.Product.Price, productItem.DiscountPercentage);
        }

        return total - PercentageDiscountCalculator.CalculateDiscount(total, discountPercentageOrder);
    }
}
