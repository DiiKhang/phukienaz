using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhuKienAZ.Areas.Admin.Models.ViewModel
{
    public class LikeViewModel
    {
        [Key]
        [DisplayName("Mã sản phẩm")]
        public string ProductId { get; set; }

        [DisplayName("Tên sản phẩm")]
        public string ProductName { get; set; }

        [DisplayName("Tên danh mục")]
        public string CategoryName { get; set; }

        [DisplayName("Số lượt thích")]
        public int Likes { get; set; }
    }
}