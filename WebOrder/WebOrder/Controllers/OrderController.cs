using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebOrder.Models;
using WebOrder.Models.ViewModels;

namespace WebOrder.Controllers;

[Authorize(Roles = "SalesManager")]
public class OrderController : Controller
{
    private readonly ErpDbContext _Erpdb;
    private readonly ApplicationDbContext _Appdb;
    
    public OrderController(ErpDbContext Erpdb, ApplicationDbContext Appdb)
    {
        _Erpdb = Erpdb;
        _Appdb = Appdb;
    }
     
    
    
    public IActionResult Products()
    { 
        
        var order = _Appdb.TempOrders.FirstOrDefault();
        if (order == null)
        {
            return RedirectToAction("CreateOrder");
        }
        var products = _Erpdb.Products.ToList();
        
        return View(products);
    }

    public IActionResult CreateOrder()
    {
        var order = _Appdb.TempOrders.FirstOrDefault();
        if (order != null)
        {
            return RedirectToAction("Products");
        }
        order = new TempOrder()
        {
            OrderDate = DateTime.Now

        };
        _Appdb.TempOrders.Add(order);
        _Appdb.SaveChanges();
        return RedirectToAction("Products");
    }
    
    
    
    
    [HttpGet]
    public IActionResult AddProduct(int productId)
    {
        var order = _Appdb.TempOrders.FirstOrDefault();
        if (order == null)
        {
            return RedirectToAction("CreateOrder");
        }
        var product = _Erpdb.Products.Find(productId);
        
        if (product == null)
        {
            return RedirectToAction("Products");
        }
        var productQuantity = new ProductQuantityVm()
        {
            productId = product.ProductId
        };
        return View(productQuantity);
    }
    
    
    
    [HttpPost]
    
    public IActionResult AddProduct(ProductQuantityVm productQuantity)
    {
        var order = _Appdb.TempOrders.FirstOrDefault();
        if (order == null)
        {
            return RedirectToAction("CreateOrder");
        }
        var product = _Erpdb.Products.Find(productQuantity.productId);
        if (product == null)
        {
            return RedirectToAction("Products");
        }
        
        var orderProduct = _Appdb.TempOrderProducts
            .FirstOrDefault(op => op.TempOrderId == order.TempOrderId && op.ProductId == product.ProductId);

        if (orderProduct != null)
        {
            orderProduct.Quantity = productQuantity.quantity;
            _Appdb.TempOrderProducts.Update(orderProduct);
        }
        else
        {
            orderProduct = new TempOrderProduct()
            {
                TempOrderId = order.TempOrderId,
                ProductId = product.ProductId,
                Quantity = productQuantity.quantity
            };
            _Appdb.TempOrderProducts.Add(orderProduct);
        }
        
        _Appdb.SaveChanges();
        return RedirectToAction("getOrders");
    }
    
    public IActionResult RemoveProduct(int productId)
    {
        var order = _Appdb.TempOrders.FirstOrDefault();
        if (order == null)
        {
            return RedirectToAction("CreateOrder");
        }
        var orderProduct = _Appdb.TempOrderProducts.FirstOrDefault(x => x.ProductId == productId);
        if (orderProduct == null)
        {
            return RedirectToAction("Products");
        }
        _Appdb.TempOrderProducts.Remove(orderProduct);
        _Appdb.SaveChanges();
        return RedirectToAction("getOrders");
    }

    public IActionResult getOrders()
    {
        var orders = _Appdb.TempOrders.FirstOrDefault();
        if(orders == null)
        {
            ViewBag.msg = "no order exists";
            return View();
        }
        var orderProducts = _Appdb.TempOrderProducts.FirstOrDefault();
        if(orderProducts == null)
        {
            ViewBag.msg = "no product exists";
            return View(new OrderViewModel()
            {
                Id = orders.TempOrderId,
                Date = orders.OrderDate,
                Items = new List<OrderItemViewModel>()
            });
        }

        var products = _Erpdb.Products.ToDictionary(p => p.ProductId, p => new{p.Name, p.Price});

        var orderdata = _Appdb.TempOrders
            .Select(x => new OrderViewModel()
            {
                Id = x.TempOrderId,
                Date = x.OrderDate,
                Items = x.TempOrderProducts.Select(y => new OrderItemViewModel()
                {
                    ProductId = y.ProductId,
                    ProductName = products.ContainsKey(y.ProductId) ? products[y.ProductId].Name : "Unknown",
                    ProductPrice = products.ContainsKey(y.ProductId) ? products[y.ProductId].Price : 0,
                    Total = products.ContainsKey(y.ProductId) ? products[y.ProductId].Price * y.Quantity : 0,
                    Quantity = y.Quantity
                }).ToList()
            }).FirstOrDefault();
       
        
        
        return View(orderdata);
    }
    public IActionResult deleteOrder()
    {
        var order = _Appdb.TempOrders.FirstOrDefault();
        if (order != null)
        {
            _Appdb.TempOrders.Remove(order);
        }
        var orderProducts = _Appdb.TempOrderProducts.FirstOrDefault();
        if (orderProducts != null)
        {
            _Appdb.TempOrderProducts.RemoveRange(_Appdb.TempOrderProducts);
        }
        _Appdb.SaveChanges();
        return RedirectToAction("getOrders");
    }

    public IActionResult ConfirmOrder()
    {
        var order = _Appdb.TempOrders.FirstOrDefault();
        if (order == null)
        {
            
            return RedirectToAction("getOrders");;
        }
        var orderProducts = _Appdb.TempOrderProducts.FirstOrDefault();
        if (orderProducts == null)
        {
            
            return RedirectToAction("getOrders");
        }
        decimal totalPrice = 0;
        var orderProductsList = _Appdb.TempOrderProducts.ToList();
        foreach (var orderProduct in orderProductsList)
        {
            var product = _Erpdb.Products.Find(orderProduct.ProductId);
            totalPrice += product.Price * orderProduct.Quantity;
        }
        ViewBag.TotalPrice = totalPrice;
        ViewBag.Users = _Erpdb.Clients.Select(c => new SelectListItem()
        {
            Value = c.ClientId.ToString(),
            Text = c.FirstName + " " + c.LastName
        }).ToList();
        return View();
    }

    [HttpPost]
    public IActionResult ConfirmOrder(ConfirmationviewModel confirmation)
    {
        var order = _Appdb.TempOrders.FirstOrDefault();
        if (order == null)
        {
            ViewBag.msg = "no order exists";
            return View();
        }

        var orderProducts = _Appdb.TempOrderProducts.FirstOrDefault();
        if (orderProducts == null)
        {
            ViewBag.msg = "no product exists";
            return View();
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Users = _Erpdb.Clients.Select(c => new SelectListItem()
            {
                Value = c.ClientId.ToString(),
                Text = c.FirstName + " " + c.LastName
            }).ToList();
            return View(confirmation);
        }
        var orderProductsList = _Appdb.TempOrderProducts.ToList();
        decimal TotalPrice = 0;
        foreach (var orderProduct in orderProductsList)
        {
            var product = _Erpdb.Products.Find(orderProduct.ProductId);
            TotalPrice += product.Price * orderProduct.Quantity;
        }
        

        var newOrder = new Order()
        {
            ClientId = confirmation.ClientId,
            CreationDate = _Appdb.TempOrders.FirstOrDefault().OrderDate,
            ExecutionDate = confirmation.ExecutionDate,
            Status = 0, // OrderStatus.Pending
            Type = 1, // OrderType.Sales
            DiscountPercentage = confirmation.Discount,
            TotalAmount = TotalPrice,
            OrderType = "SellOrder",
            WarehouseId = 1
            
        };
        _Erpdb.Orders.Add(newOrder);
        _Erpdb.SaveChanges();
        
        foreach (var orderProduct in orderProductsList)
        {
            var newOrderProduct = new OrderProduct()
            {
                OrderId = newOrder.OrderId,
                ProductId = orderProduct.ProductId,
                Quantity = orderProduct.Quantity,
                ExpirationDate = null
            };
            _Erpdb.OrderProducts.Add(newOrderProduct);
        }

        _Erpdb.SaveChanges();
        _Appdb.TempOrders.RemoveRange(_Appdb.TempOrders);
        _Appdb.TempOrderProducts.RemoveRange(_Appdb.TempOrderProducts);
        _Appdb.SaveChanges();
        String data = "Order has been created successfully";
        return RedirectToAction("Index", "Home", new { msg = data });
    }

    public IActionResult getCommande()
    {
       
        
            var orders = _Erpdb.Orders
                .Include(o => o.Client)
                .Include(o => o.OrderProducts)
                
                .ThenInclude(op => op.Product)
                .ToList() // Materialize the query here to detach it from EF
                .Select(o => new CommandeViewModel
                {
                    Client = o.Client,
                    Order = o,
                    OrderProducts = o.OrderProducts.ToList()
                })
                .ToList();
    
        return View(orders);


    } 
    
    public IActionResult CancelCommande(int orderId)
    {
        var order = _Erpdb.Orders.Find(orderId);
        if (order != null)
        {
            order.Status = 3;
            _Erpdb.Orders.Update(order);
            _Erpdb.SaveChanges();
        }

        
        return RedirectToAction("getCommande");
    }
    
    public IActionResult ActiverCommande(int orderId)
    {
        var order = _Erpdb.Orders.Find(orderId);
        if (order != null)
        {
            order.Status = 0;
        }

        _Erpdb.Orders.Update(order);
        _Erpdb.SaveChanges();
        return RedirectToAction("getCommande");
    }
    















}