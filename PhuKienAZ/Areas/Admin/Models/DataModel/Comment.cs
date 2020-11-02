using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhuKienAZ.Areas.Admin.Models.DataModel
{
    public class Comment
    {
        public int Id { get; set; }

        [StringLength(32)]
        public string ProductId { get; set; }

        [StringLength(32)]
        public string CustomerId { get; set; }

        [Column(TypeName = "ntext")]
        [AllowHtml]
        [Display(Name = "Nội dung")]
        public string Content { get; set; }
        public DateTime Datetime { get; set; }
        public bool Deleted { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual Product Product { get; set; }
    }
}