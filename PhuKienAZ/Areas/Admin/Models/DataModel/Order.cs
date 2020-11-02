using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PhuKienAZ.Areas.Admin.Models.DataModel
{
    public class Order
    {
        [Key]
        [Display(Name = "Mã số")]
        public int Id { get; set; }

        [Display(Name = "Mã khách hàng")]
        [StringLength(32)]
        public string CustomerId { get; set; }

        [StringLength(128)]
        [Display(Name = "Địa chỉ giao hàng")]
        public string Destination { get; set; }

        [StringLength(64)]
        [Display(Name = "Ghi chú")]
        public string Description { get; set; }

        [Display(Name = "Thanh toán trực tuyến")]
        public bool PayByBank { get; set; }

        [Display(Name = "Thời gian")]
        public DateTime? Date { get; set; }

        [Display(Name = "Hoàn thành")]
        [Column(TypeName = "bit")]
        public bool Completed { get; set; }

        public bool Deleted { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}