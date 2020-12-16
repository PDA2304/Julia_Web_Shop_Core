using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Julia_Web_Shop_Core.Models
{
    public class AppDBContent : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Users> User { get; set; }

        public AppDBContent(DbContextOptions<AppDBContent> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
