using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        var products = _Erpdb.Products.ToDictionary(p => p.ProductId, p => p.Name);

        var orderdata = _Appdb.TempOrders
            .Select(x => new OrderViewModel()
            {
                Id = x.TempOrderId,
                Date = x.OrderDate,
                Items = x.TempOrderProducts.Select(y => new OrderItemViewModel()
                {
                    ProductId = y.ProductId,
                    ProductName = products.ContainsKey(y.ProductId) ? products[y.ProductId] : "Unknown",
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

    /*public IActionResult chekout()
    {
        
        
    }*/
    
    
    
    
    
    


}