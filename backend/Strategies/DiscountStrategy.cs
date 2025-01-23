
public class PercentageDiscountStrategy 
{
    public static decimal CalculateDiscount(decimal price, double discountPercentage)
    {
        if (discountPercentage == 0)
        {
            return 0;
        }
        return price * discountPercentage / 100;
    }
}


public class Commande
{
    public decimal CalculateTotal(List<Product> products, double discountPercentageOrder)
    {
        decimal total = 0;
        foreach (var product in products)
        {
            total += product.Price - PercentageDiscountStrategy.CalculateDiscount(product.Price, product.DiscountPercentage);
        }

        return total - PercentageDiscountStrategy.CalculateDiscount(total, discountPercentageOrder);
    }
}
