using Microsoft.EntityFrameworkCore;
using PrepMe.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepMe.DAL
{
    public class PrepMeDbContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Product> Products { get; set; }
        public PrepMeDbContext(DbContextOptions<PrepMeDbContext> options) : base(options)
        {

        }
        

    }
}
