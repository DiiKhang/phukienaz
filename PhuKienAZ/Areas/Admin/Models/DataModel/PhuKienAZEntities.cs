using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PhuKienAZ.Areas.Admin.Models.DataModel
{
    public class PhuKienAZEntities : DbContext
    {
        public PhuKienAZEntities() : base("PhuKienAZLocalConnectionString")
        {

        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<MyController> MyControllers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<News> News { get; set; }
    }
}