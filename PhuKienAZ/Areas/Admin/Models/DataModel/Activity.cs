using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PhuKienAZ.Areas.Admin.Models.DataModel
{
    public class Activity
    {
        public int Id { get; set; }

        [StringLength(36)]
        public string UserId { get; set; }

        public string ControllerId { get; set; }

        [Display(Name = "Hành động")]
        public string Action { get; set; }

        [StringLength(32)]
        public string RecordId { get; set; }

        [Display(Name = "Thời gian")]
        public DateTime? Datetime { get; set; }

        public virtual User User { get; set; }

        [ForeignKey("ControllerId")]
        public virtual MyController Controller { get; set; }
    }
}