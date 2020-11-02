using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhuKienAZ.Areas.Admin.Models.DataModel
{
    public class News
    {
        [Key]
        [Display(Name = "Mã số")]
        public int Id { get; set; }

        [StringLength(128, MinimumLength = 6, ErrorMessage = "Dữ liệu phải có độ dài từ 6 - 128 ký tự")]
        [Display(Name = "Tiêu đề")]
        [Required(ErrorMessage = "Chưa nhập dữ liệu")]
        public string Title { get; set; }

        [StringLength(128, MinimumLength = 6, ErrorMessage = "Dữ liệu phải có độ dài từ 6 - 128 ký tự")]
        [Display(Name = "Ảnh")]
        public string Picture { get; set; }

        [StringLength(512, MinimumLength = 64, ErrorMessage = "Dữ liệu phải có độ dài từ 64 - 256 ký tự")]
        [Display(Name = "Tóm tắt")]
        [Required(ErrorMessage = "Chưa nhập dữ liệu")]
        public string Brief { get; set; }

        [Column(TypeName = "ntext")]
        [AllowHtml]
        [Display(Name = "Nội dung")]
        public string Content { get; set; }

        [Display(Name = "Người tạo")]
        [StringLength(32)]
        public string UserId { get; set; }

        [Display(Name = "Thời gian")]
        public DateTime? Date { get; set; }

        public bool Deleted { get; set; }

        public virtual User User { get; set; }
    }
}