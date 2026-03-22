using GrillFusion_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.ComponentModel;
using System.Reflection.Emit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GrillFusion_API.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
            
        }

        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<MenuItem>().HasData(
// Burgers
new MenuItem { Id = 1, Name = "Classic Smash Burger", Description = "Smashed beef patty with cheddar, lettuce, tomato, pickles, and house sauce on a brioche bun.", Category = "Burger", SpecialTag = "Best Seller", Price = 12.99, Image = "https://images.unsplash.com/photo-1568901346375-23c9450c58cd?w=400" },
new MenuItem { Id = 2, Name = "Double Inferno Burger", Description = "Double beef patties with pepper jack, jalapeños, and spicy inferno sauce.", Category = "Burger", SpecialTag = "Spicy", Price = 15.99, Image = "https://images.unsplash.com/photo-1553979459-d2229ba7433b?w=400" },
new MenuItem { Id = 3, Name = "BBQ Bacon Burger", Description = "...", Category = "Burger", SpecialTag = "", Price = 14.49, Image = "https://images.unsplash.com/photo-1594212699903-ec8a3eca50f5?w=400" },
new MenuItem { Id = 4, Name = "Mushroom Swiss Burger", Description = "...", Category = "Burger", SpecialTag = "", Price = 13.99, Image = "https://images.unsplash.com/photo-1520072959219-c595dc870360?w=400" },

// Grills
new MenuItem { Id = 5, Name = "Ribeye Steak", Description = "Juicy ribeye steak grilled with house seasoning.", Category = "Grill", SpecialTag = "Chef's Choice", Price = 34.99, Image = "https://images.unsplash.com/photo-1558030006-450675393462?w=400" },
new MenuItem { Id = 6, Name = "BBQ Ribs Half Rack", Description = "...", Category = "Grill", SpecialTag = "Best Seller", Price = 24.99, Image = "https://images.unsplash.com/photo-1544025162-d76694265947?w=400" },
new MenuItem { Id = 7, Name = "Grilled Chicken Platter", Description = "Flame-grilled marinated chicken breast with smoky flavor.", Category = "Grill", SpecialTag = "", Price = 18.99, Image = "https://images.unsplash.com/photo-1598515214211-89d3c73ae83b?w=400" },
new MenuItem { Id = 8, Name = "Lamb Chops", Description = "...", Category = "Grill", SpecialTag = "New", Price = 29.99, Image = "https://images.unsplash.com/photo-1611489142329-7f31aa425f47?w=400" },

// Sides
new MenuItem { Id = 9, Name = "Loaded Fries", Description = "...", Category = "Sides", SpecialTag = "Best Seller", Price = 7.99, Image = "https://images.unsplash.com/photo-1573080496219-bb080dd4f877?w=400" },
new MenuItem { Id = 10, Name = "Onion Rings", Description = "Golden fried onion rings with a crispy coating.", Category = "Sides", SpecialTag = "", Price = 5.99, Image = "https://images.unsplash.com/photo-1639024471283-03518883512d?w=400" },
new MenuItem { Id = 11, Name = "Mac & Cheese", Description = "Creamy macaroni pasta in rich melted cheese.", Category = "Sides", SpecialTag = "", Price = 6.49, Image = "https://images.unsplash.com/photo-1543339494-b4cd4f7ba686?w=400" },
new MenuItem { Id = 12, Name = "Grilled Corn", Description = "...", Category = "Sides", SpecialTag = "", Price = 4.49, Image = "https://images.unsplash.com/photo-1551754655-cd27e38d2076?w=400" },

// Drinks
new MenuItem { Id = 13, Name = "Mango Lemonade", Description = "...", Category = "Drinks", SpecialTag = "New", Price = 4.99, Image = "https://images.unsplash.com/photo-1546173159-315724a31696?w=400" },
new MenuItem { Id = 14, Name = "Smoky BBQ Shake", Description = "...", Category = "Drinks", SpecialTag = "", Price = 6.99, Image = "https://images.unsplash.com/photo-1572490122747-3968b75cc699?w=400" },
new MenuItem { Id = 15, Name = "Iced Craft Lemonade", Description = "Freshly squeezed lemonade served over ice.", Category = "Drinks", SpecialTag = "", Price = 3.99, Image = "https://images.unsplash.com/photo-1523677011781-c91d1bbe2f9e?w=400" }
);
        }
    }
}
