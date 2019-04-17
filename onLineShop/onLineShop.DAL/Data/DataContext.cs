using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onLineShop.Model;

namespace onLineShop.DAL.Data
{
    public class DataContext : DbContext
    {

        public DataContext()
            : base("DefaultConnection") { }


        /// <summary>
        /// Care modele trebuie sa aiba un tabel
        /// </summary>
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<CartItem> ShoppingCartItems { get; set; }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }
    }
}
