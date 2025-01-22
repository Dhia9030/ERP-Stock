public abstract class ProductFactory
{
    public abstract Product CreateProduct();
}

public class ClothingProductFactory : ProductFactory
{
    public override Product CreateProduct()
    {
        return new ClothingProduct();
    }
}

public class ElectronicsProductFactory : ProductFactory
{
    public override Product CreateProduct()
    {
        return new ElectronicsProduct();
    }
}

public class FoodProductFactory : ProductFactory
{
    public override Product CreateProduct()
    {
        return new ElectronicsProduct();
    }
}

public class ProductCreator
{
    public Product GetProduct(string type)
    {
        ProductFactory factory;
        if (type == "Clothing")
        {
            factory = new ClothingProductFactory();
        }
        else if (type == "Food")
        {
            factory = new FoodProductFactory();
        }
        else if (type == "Electronics")
        {
            factory = new ElectronicsProductFactory();
        }

        return factory.CreateProduct();
    }
}
