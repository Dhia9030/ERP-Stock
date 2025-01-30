using Microsoft.EntityFrameworkCore;
using StockManagement.Data;
using StockManagement.Models;
using StockManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductBlockRepository _productBlockRepository;
        private readonly IStockMovementRepository _stockMovementRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IProductItemRepository _productItemRepository;
        private readonly IStockMovementItemsRepository _stockMovementItemRepository;

        public OrderService(
            IOrderRepository orderRepository,
            IProductBlockRepository productBlockRepository,
            IStockMovementRepository stockMovementRepository,
            ILocationRepository locationRepository,
            IProductItemRepository productItemRepository,
            IStockMovementItemsRepository stockMovementItemRepository)
        {
            _orderRepository = orderRepository;
            _productBlockRepository = productBlockRepository;
            _stockMovementRepository = stockMovementRepository;
            _locationRepository = locationRepository;
            _productItemRepository = productItemRepository;
            _stockMovementItemRepository = stockMovementItemRepository;
        }

        public async Task ExecuteBuyOrderAsync(int orderId)
{
    var order = await _orderRepository.GetByIdAsync("OrderId", orderId,
        q => q.Include(o => o.Warehouse)
              .Include(o => o.OrderProducts)
              .ThenInclude(op => op.Product));

    if (order == null)
    {
        throw new ArgumentException($"Order with ID {orderId} not found");
    }

    if (order.Status != OrderStatus.Pending)
    {
        throw new InvalidOperationException("Order is already in execution or completed");
    }

    if (order.OrderProducts == null || !order.OrderProducts.Any())
    {
        throw new InvalidOperationException("Order is empty");
    }

    order.Status = OrderStatus.Processing;
    order.RealExecutionDate = DateTime.UtcNow;
    await _orderRepository.UpdateAsync(order);

    foreach (var orderProduct in order.OrderProducts)
    {
        if (orderProduct == null || orderProduct.Product == null)
        {
            throw new InvalidOperationException("Invalid order product data");
        }

        var location = await _locationRepository.GetFirstEmptyLocationForWarehouse(order.WarehouseId);
        
        if (location == null)
        {
            throw new InvalidOperationException("No empty location found in the warehouse");
        }
        
        ProductBlock productBlock;

        if (orderProduct.Product is FoodProduct)
        {
            productBlock = new FoodProductBlock
            {
                ProductId = orderProduct.ProductId,
                LocationId = location.LocationId,
                Quantity = orderProduct.Quantity,
                Status = ProductBlockStatus.InStock,
                ExpirationDate = orderProduct.ExpirationDate
            };
        }
        else
        {
            productBlock = new ProductBlock
            {
                ProductId = orderProduct.ProductId,
                LocationId = location.LocationId,
                Quantity = orderProduct.Quantity,
                Status = ProductBlockStatus.InStock
            };
        }

        await _productBlockRepository.AddAsync(productBlock);

        var supplierAreaLocation = await _locationRepository.GetSupplierAreaLocation(order.Warehouse.Name);
        if (supplierAreaLocation == null)
        {
            throw new InvalidOperationException("Supplier area location not found");
        }

        var stockMovement = new StockMovement
        {
            MovementType = StockMovementStatus.Incoming,
            CreatedBy = "System",
            MovementDate = DateTime.UtcNow,
            SourceProductBlockId = productBlock.ProductBlockId,
            DestinationProductBlockId = productBlock.ProductBlockId,
            SourceLocationId = supplierAreaLocation.LocationId,
            DestinationLocationId = location.LocationId,
            Quantity = orderProduct.Quantity,
            OrderId = order.OrderId,
        };

        await _stockMovementRepository.AddAsync(stockMovement);

        for (int i = 0; i < orderProduct.Quantity; i++)
        {
            var productItem = new ProductItem
            {
                ProductBlockId = productBlock.ProductBlockId,
                Status = ProductItemStatus.InStock,
                PurchaseOrder = order
            };

            await _productItemRepository.AddAsync(productItem);

            var stockMovementItem = new StockMovementItems
            {
                ProductItemId = productItem.ProductItemId,
                StockMovementId = stockMovement.StockMovementId
            };

            await _stockMovementItemRepository.AddAsync(stockMovementItem);
        }
        location.isEmpty = false;
        await _locationRepository.UpdateAsync(location);
    }

    order.Status = OrderStatus.Delivered;
    await _orderRepository.UpdateAsync(order);
    
}

        
        public async Task ExecuteSellOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync("OrderId",orderId,
                q => q.Include(o => o.Warehouse)
                    .Include(o => o.Client)
                    .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                    .ThenInclude(p => p.ProductBlocks));

            if (order == null)
                throw new ArgumentException("Order not found");

            if (order.Status != OrderStatus.Pending)
                throw new InvalidOperationException("Order is already in execution or completed");

            if (order.OrderProducts == null || !order.OrderProducts.Any())
                throw new InvalidOperationException("Order is empty");

            order.Status = OrderStatus.Processing;
            order.RealExecutionDate = DateTime.UtcNow;
            await _orderRepository.UpdateAsync(order);

            foreach (var orderProduct in order.OrderProducts)
            {
                if (orderProduct.Quantity > orderProduct.Product.StockQuantity)
                    throw new InvalidOperationException(
                        $"Product {orderProduct.Product.Name} has only {orderProduct.Product.StockQuantity} in stock");
            }

            foreach (var orderProduct in order.OrderProducts)
            {
                IEnumerable<ProductBlock> productBlocks;
                
                if (orderProduct.Product is FoodProduct)
                {
                    productBlocks = productBlocks = (IEnumerable<ProductBlock>)await _productBlockRepository.GetAllFoodProductBlockOrderedByExpirationDateAsync(
                        q => q.Include(p => p.ProductItems));
                }
                else
                {
                    productBlocks = orderProduct.Product.ProductBlocks.ToList();
                }

                if (!productBlocks.Any())
                    throw new InvalidOperationException($"No stock available for product {orderProduct.Product.Name}");
                
                int remainingQuantity = orderProduct.Quantity;
                
                foreach (var productBlock in
                         productBlocks) 
                {
                    if (remainingQuantity <= 0)
                        break;

                    int deductedQuantity = Math.Min(productBlock.Quantity,remainingQuantity);
                    productBlock.Quantity -= deductedQuantity;
                    remainingQuantity -= deductedQuantity;
                    
                    var productItems = productBlock.ProductItems.ToList();
                    for (int i = 0; i < deductedQuantity; i++)
                    {
                        productItems[i].Status = ProductItemStatus.Sold;
                        productItems[i].ProductBlockId = null;
                        productItems[i].SaleOrder = order;
                    }
                    
                    var stockMovement = new StockMovement
                    {
                        MovementType = StockMovementStatus.Outgoing,
                        CreatedBy = "System",
                        MovementDate = DateTime.UtcNow,
                        SourceProductBlockId = productBlock.ProductBlockId,
                        DestinationLocationId = _locationRepository.GetBuyerAreaLocation(order.Warehouse.Name).Id,
                        SourceLocationId = productBlock.LocationId ?? throw new InvalidOperationException("Product block location is null"),
                        Quantity = deductedQuantity,
                        OrderId = order.OrderId
                    };
                    
                    if (productBlock.Quantity <= remainingQuantity)
                    {
                        productBlock.Quantity = 0;
                        productBlock.Status = ProductBlockStatus.Sold;
                        productBlock.LocationId = null;
                    }
                    else
                    {
                        productBlock.Quantity -=deductedQuantity ;
                    }
                   
                    await _stockMovementRepository.AddAsync(stockMovement);
                    await _productBlockRepository.UpdateAsync(productBlock);
                }


            }

            order.Status = OrderStatus.Delivered;
            await _orderRepository.UpdateAsync(order);
        }
    }
}