using Microsoft.EntityFrameworkCore;
using StockManagement.Data;
using StockManagement.Models;
using StockManagement.Repositories;

namespace StockManagement.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderRepository _orderRepository;
        private readonly ProductBlockRepository _productBlockRepository;
        private readonly StockMovementRepository _stockMovementRepository;
        private readonly LocationRepository _locationRepository;
        private readonly ProductItem _productItem;

        public OrderService()
        {
            
        }
        
        public async Task ExecuteBuyOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId, 
                q => q.Include(o => o.Warehouse)
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product));

            if (order == null)
            {
                throw new ArgumentException("Order not found");
            }

            if (order.Status != OrderStatus.Pending)
            {
                throw new InvalidOperationException("Order is already in execution or completed");
            }
            
            if (order.OrderProducts == null)
            {
                throw new InvalidOperationException("Order is empty");
            }

            order.Status = OrderStatus.Processing;
            order.RealExecutionDate = DateTime.UtcNow;

            foreach (var orderProduct in order.OrderProducts)
            {
                var location = _locationRepository.GetFirstEmptyLocationForWarehouse(order.WarehouseId);
                ProductBlock productBlock;
                
                if (orderProduct.Product is FoodProduct)
                {
                    productBlock = new FoodProductBlock
                    {
                        ProductId = orderProduct.ProductId,
                        LocationId = location.Id,
                        Quantity = orderProduct.Quantity,
                        Status = ProductBlockStatus.InStock,
                        ExpirationDate = orderProduct.ExpirationDate
                    };
                }
                
                else
                {   productBlock = new ProductBlock
                    {
                        ProductId = orderProduct.ProductId,
                        LocationId = location.Id,
                        Quantity = orderProduct.Quantity,
                        Status = ProductBlockStatus.InStock
                    };
                }
                await _productBlockRepository.AddAsync(productBlock);

                for (int i = 0; i < orderProduct.Quantity; i++)
                {
                    var productItem = new ProductItem
                    {
                        ProductBlockId = productBlock.ProductBlockId,
                        Status = ProductItemStatus.InStock,
                        PurchaseOrder = order
                    };
                    
                }
                
                
                var stockMovement = new StockMovement
                {
                    MovementType =  StockMovementStatus.Incoming ,
                    CreatedBy = "System", 
                    MovementDate = DateTime.UtcNow,
                    SourceProductBlockId = productBlock.ProductBlockId,
                    DestinationProductBlockId = productBlock.ProductBlockId,
                    SourceLocationId = _locationRepository.GetSupplierAreaLocation(order.Warehouse.Name).Id, 
                    DestinationLocationId = location.Id, 
                    Quantity = orderProduct.Quantity,
                    OrderId = order.OrderId
                };

                await _stockMovementRepository.AddAsync(stockMovement);
            }
        }
        
        public async Task ExecuteSellOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId, 
                q => 
                    q.Include(o => o.Warehouse)
                    .Include(o => o.Client)
                    .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product));

            if (order == null)
            {
                throw new ArgumentException("Order not found");
            }

            if (order.Status != OrderStatus.Pending)
            {
                throw new InvalidOperationException("Order is already in execution or completed");
            }

            order.Status = OrderStatus.Processing;
            order.RealExecutionDate = DateTime.UtcNow;

            if (order.OrderProducts == null)
            {
                throw new InvalidOperationException("Order is empty");
            }

            foreach (var orderProduct in order.OrderProducts)
            {
               
                ProductBlock productBlock;
                
                if (orderProduct.Product is FoodProduct)
                {
                   
                }
                
                else
                {   productBlock = new ProductBlock
                    {
                       
                    };
                }
                await _productBlockRepository.AddAsync(productBlock);
                
                var stockMovement = new StockMovement
                {
                    MovementType =  StockMovementStatus.Incoming ,
                    CreatedBy = "System", 
                    MovementDate = DateTime.UtcNow,
                    SourceProductBlockId = productBlock.ProductBlockId,
                    DestinationProductBlockId = productBlock.ProductBlockId,
                    SourceLocationId = _locationRepository.GetSupplierAreaLocation(order.Warehouse.Name).Id, 
                    DestinationLocationId = location.Id, 
                    Quantity = orderProduct.Quantity,
                    OrderId = order.OrderId
                };

                await _stockMovementRepository.AddAsync(stockMovement);
            }
        }
    }
}