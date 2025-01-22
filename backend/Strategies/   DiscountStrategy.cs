public interface IDiscountStrategy
{
    decimal CalculateDiscount(decimal price);
}

public class NoDiscountStrategy : IDiscountStrategy
{
    public decimal CalculateDiscount(decimal price)
    {
        return 0;
    }
}

public class PercentageDiscountStrategy : IDiscountStrategy
{
    public static decimal CalculateDiscount(decimal price, double discountPercentage)
    {
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
