using RobertsShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobertsShop.DataAccess.SQL
{
    public class DataContext : DbContext
    {
        //Konstruktor bestimmt im Aufruf der Elternklasse Connectionsstring mit Name in Config
        public DataContext() : base("DefaultConnection")
        {

        }
        // Block to set Models to Tables -> Migration scripting etc
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories {get; set;}
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        

    }
}
