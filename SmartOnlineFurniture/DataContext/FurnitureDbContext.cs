using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartOnlineFurniture.Models;
using System.Data.Entity;

namespace SmartOnlineFurniture.DataContext
{
    public class FurnitureDbContext:DbContext
    {
        public DbSet<Companies> Companies { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Registers> Registers { get; set; }
        public DbSet<Woods> Woods { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<FStyles> FStyles { get; set; }
        public DbSet<DrawingOrders> DrawingOrders { get; set; }
        public DbSet<Prices> Prices { get; set; }
        public DbSet<Orders> Orders { get; set; }
    }
}