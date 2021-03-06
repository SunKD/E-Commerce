using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    public class Customer : BaseEntity
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public DateTime Created_at { get; set; } = DateTime.Now;
        public DateTime Updated_at { get; set; } = DateTime.Now;
        public List<Order> Orders { get; set; } = new List<Order>();

        public string displayDate{
            get{
                return this.Created_at.ToString("MMMM dd, yyyy");
            }
        }
    }
}