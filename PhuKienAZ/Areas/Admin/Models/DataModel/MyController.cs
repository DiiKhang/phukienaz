using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhuKienAZ.Areas.Admin.Models.DataModel
{
    public class MyController
    {
        [Key]
        [StringLength(2)]
        public string Id { get; set; }
        [StringLength(64)]
        public string EngName { get; set; }
        [StringLength(64)]
        [Display(Name = "Tên chức năng")]
        public string VieName { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
    }
}