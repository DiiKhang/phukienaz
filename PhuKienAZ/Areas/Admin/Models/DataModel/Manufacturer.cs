using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhuKienAZ.Areas.Admin.Models.DataModel
{
    public class Manufacturer
    {
        [Key]
        [Display(Name = "Mã số")]
        public int Id { get; set; }

        [StringLength(32, MinimumLength = 3, ErrorMessage = "Dữ liệu phải có độ dài từ 3 - 32 ký tự")]
        [Display(Name = "Tên hãng sản xuất")]
        [Required(ErrorMessage = "Chưa nhập dữ liệu")]
        public string Name { get; set; }

        public bool Deleted { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}