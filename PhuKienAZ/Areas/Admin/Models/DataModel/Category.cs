using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhuKienAZ.Areas.Admin.Models.DataModel
{
    public class Category
    {
        [Key]
        [Display(Name = "Mã số")]
        public int Id { get; set; }

        [StringLength(64, MinimumLength = 3, ErrorMessage = "Dữ liệu phải có độ dài từ 3 - 64 ký tự")]
        [Display(Name = "Tên danh mục")]
        [Required(ErrorMessage = "Chưa nhập dữ liệu")]
        public string Name { get; set; }

        [StringLength(128)]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        public bool Deleted { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}