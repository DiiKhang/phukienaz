using PhuKienAZ.Areas.Admin.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhuKienAZ.Areas.Admin.Utilities
{
    public class SystemLog
    {
        public static void Add(string cotrollerId, string action, string recordId)
        {
            using(PhuKienAZEntities db = new PhuKienAZEntities())
            {
                db.Activities.Add(new Activity()
                {
                    ControllerId = cotrollerId,
                    Action = action,
                    RecordId = recordId,
                    Datetime = DateTime.Now,
                    UserId = ((User)HttpContext.Current.Session["user"]).Id
                });
                db.SaveChanges();
            }           
        }
    }
}