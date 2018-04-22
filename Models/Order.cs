using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    public class Order : BaseEntity
    {
        public int OrderID { get; set; }
        public int BuyerID { get; set; }
        public Customer Buyer { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public bool Paid { get; set; }
        public DateTime Created_at { get; set; } = new DateTime(DateTime.Now.Ticks);
        // public List<Order> Orders { get; set; } = new List<Order>();

    }
}