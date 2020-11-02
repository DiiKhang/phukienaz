using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PhuKienAZ.Areas.Admin.Models.DataModel
{
    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Mã đơn hàng")]
        public int OrderId { get; set; }

        [Display(Name = "Mã sản phẩm")]
        [Required(ErrorMessage = "Chưa nhập dữ liệu")]
        public string ProductId { get; set; }

        [Display(Name = "Giá")]
        [Required(ErrorMessage = "Chưa nhập dữ liệu")]
        public int Price { get; set; }

        [Display(Name = "Số lượng")]
        [Required(ErrorMessage = "Chưa nhập dữ liệu")]
        public int Quantity { get; set; }


        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}