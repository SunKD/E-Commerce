using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace E_Commerce.Controllers
{
    public class HomeController : Controller
    {
        private EcommerceContext _context;

        public HomeController(EcommerceContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            ViewBag.Products = _context.Products.Take(6).ToList();
            ViewBag.Customers = _context.Customers.Take(3).ToList();
            var allOrder = _context.Products.Include(e => e.Orders).ThenInclude(r => r.Buyer).Take(3).ToList();
            return View(allOrder);
        }

        [HttpGet]
        [Route("Products")]
        public IActionResult Products()
        {
            ViewBag.Products = _context.Products.ToList();
            return View("Products");
        }

        [HttpPost]
        [Route("addProduct")]
        public IActionResult AddProduct(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                Product newProduct = new Product
                {
                    Name = model.Name,
                    Image = model.Image,
                    Description = model.Description,
                    Quantity = model.Quantity,
                    Price = model.Price
                };

                _context.Products.Add(newProduct);
                _context.SaveChanges();
                return RedirectToAction("Products");
            }
            ViewBag.Products = _context.Products.ToList();

            return View("Products");
        }

        [HttpGet]
        [Route("orders")]
        public IActionResult Orders()
        {
            ViewBag.AllCustomers = _context.Customers;
            var allOrder = _context.Products.Include(e => e.Orders).ThenInclude(r => r.Buyer).ToList();
            return View(allOrder);
        }

        [HttpPost]
        [Route("addOrder")]
        public IActionResult AddOrder(string CustomerName, int quantity, string ProductName)
        {
            int num = _context.Products.First(r => r.Name == ProductName).Quantity;
            if (quantity <= num)
            {
                Order newOrder = new Order
                {
                    BuyerID = _context.Customers.First(c => c.Name == CustomerName).CustomerID,
                    ProductID = _context.Products.First(r => r.Name == ProductName).ProductID,
                    Quantity = quantity,
                    Paid = false
                };
                _context.Products.First(r => r.Name == ProductName).Quantity -= quantity;
                _context.Orders.Add(newOrder);
                _context.SaveChanges();
                return RedirectToAction("Orders");
            }
            ViewBag.Error = "Maximum Quantity in the Stock: " + num;
            ViewBag.AllCustomers = _context.Customers;
            var allOrder = _context.Products.Include(e => e.Orders).ThenInclude(r => r.Buyer).ToList();
            return View("Orders", allOrder);
        }

        [HttpGet]
        [Route("Customer")]
        public IActionResult Customers()
        {
            ViewBag.Customers = _context.Customers.ToList();
            return View("Customer");
        }

        [HttpPost]
        [Route("addCustomer")]
        public IActionResult AddCustomer(string InputName)
        {
            InputName.Trim();
            bool existing = _context.Customers.Any(e => e.Name == InputName);
            if (!existing)
            {
                Customer NewCustomer = new Customer
                {
                    Name = InputName
                };
                _context.Customers.Add(NewCustomer);
                _context.SaveChanges();
                System.Console.WriteLine("Added customer name " + InputName);
                return RedirectToAction("Customers");

            }
            ViewBag.Error = "Customer name is already in the database!";
            return View("Customer");
        }

        [HttpPost]
        [Route("search")]
        public JsonResult Search(string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                return Json(_context.Customers.ToList());
            }
            var result = _context.Customers.Where(e => e.Name.ToLower().Contains(input.ToLower())).ToList();
            return Json(result);

        }

        [HttpPost]
        [Route("payment/{orderID}")]
        public IActionResult Payment(int orderID)
        {
            StripeConfiguration.SetApiKey("sk_test_8VqR9CUcB1iTwzXbSSAO9Ah8");
            // var token = model.Token;
            var options = new StripeChargeCreateOptions
            {
                Amount = 999,
                Currency = "usd",
                SourceTokenOrExistingSourceId = "tok_visa",
                StatementDescriptor = "Custom descriptor",
                ReceiptEmail = "jenny.rosen@example.com",
            };
            var service = new StripeChargeService();
            StripeCharge charge = service.Create(options);
            return RedirectToAction("Orders");
        }

        [HttpGet]
        [Route("remove/{id}")]
        public IActionResult RemoveCustomer(int id)
        {
            Customer rmvCustomer = _context.Customers.SingleOrDefault(e => e.CustomerID == id);
            _context.Customers.Remove(rmvCustomer);
            _context.SaveChanges();
            return RedirectToAction("Customer");
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
