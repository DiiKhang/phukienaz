using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PhuKienAZ.Areas.Admin.Models.DataModel
{
    public class Like
    {
        [Key, Column(Order = 0)]
        [StringLength(32)]
        public string ProductId { get; set; }

        [Key, Column(Order = 1)]
        [StringLength(32)]
        public string CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Product Product { get; set; }
    }
}