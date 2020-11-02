using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhuKienAZ.Areas.Customer.Models.ViewModels
{
    public class CartItem
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string Picture { get; set; }
    }
}