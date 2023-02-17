using PrepMe.DAL.Interfaces;
using PrepMe.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepMe.DAL.Implementations
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(PrepMeDbContext context) : base(context)
        {

        }

        public bool HasProduct(string product)
        {
            bool check = _context.Products.Any(p => p.Equals(product));
            return check;
        }
    }
}
