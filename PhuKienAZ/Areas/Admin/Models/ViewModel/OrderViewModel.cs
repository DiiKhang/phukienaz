using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhuKienAZ.Areas.Admin.Models.ViewModel
{
    public class OrderViewModel
    {
        [Key]
        [Display(Name = "Mã số")]
        public int Id { get; set; }

        [Display(Name = "Tên khách hàng")]
        public string CustomerName { get; set; }

        [Display(Name = "Tên khách hàng")]
        public string CustomerId { get; set; }

        [Display(Name = "Hình thức thanh toán")]
        public string PayType { get; set; }

        [Display(Name = "Trạng thái")]
        public string Status { get; set; }

        [Display(Name = "Thời gian")]
        public DateTime Datetime { get; set; }

        public int TotalQuantity { get; set; }

        public int Total { get; set; }

    }
}