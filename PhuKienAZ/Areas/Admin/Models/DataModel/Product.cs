using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhuKienAZ.Areas.Admin.Models.DataModel
{
    public class Product
    {
        [Key]
        [Display(Name = "Mã số")]
        public string Id { get; set; }

        [Display(Name = "Danh mục")]
        [Required(ErrorMessage = "Chưa nhập dữ liệu")]
        public int CategoryId { get; set; }

        [StringLength(128, MinimumLength = 6, ErrorMessage = "Dữ liệu phải có độ dài từ 6 - 128 ký tự")]
        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Chưa nhập dữ liệu")]
        public string Name { get; set; }

        [Display(Name = "Hãng sản xuất")]
        [Required(ErrorMessage = "Chưa nhập dữ liệu")]
        public int ManufacturerId { get; set; }

        [StringLength(64, ErrorMessage = "Dữ liệu phải có độ dài nhỏ hơn 64 ký tự")]
        [Display(Name = "Nước sản xuất")]
        [Required(ErrorMessage = "Chưa nhập dữ liệu")]
        public string ProducingCountry { get; set; }

        [Display(Name = "Thời gian bảo hành (tháng)")]
        [Required(ErrorMessage = "Chưa nhập dữ liệu")]
        public int WarrantyMonths { get; set; }

        [Display(Name = "Giá")]
        [Required(ErrorMessage = "Chưa nhập dữ liệu")]
        public int Price { get; set; }

        [Display(Name = "Giá khuyến mãi")]
        public int? SellOff { get; set; }

        [StringLength(32, MinimumLength = 2, ErrorMessage = "Dữ liệu phải có độ dài từ 2 - 32 ký tự")]
        [Display(Name = "Đơn vị tính")]
        [Required(ErrorMessage = "Chưa nhập dữ liệu")]
        public string Unit { get; set; }

        [StringLength(128, MinimumLength = 6, ErrorMessage = "Dữ liệu phải có độ dài từ 6 - 128 ký tự")]
        [Display(Name = "Ảnh")]
        public string Picture { get; set; }

        [Column(TypeName = "ntext")]
        [AllowHtml]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        public bool Deleted { get; set; }

        public virtual Category Category { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }

        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}