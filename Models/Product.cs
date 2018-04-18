using System;
using System.Collections.Generic;

namespace E_Commerce.Models
{
    public class Product : BaseEntity
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public DateTime Created_at { get; set; } = new DateTime(DateTime.UtcNow.Ticks);
        public DateTime Updated_at { get; set; } = new DateTime(DateTime.UtcNow.Ticks);
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}