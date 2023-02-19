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

        public bool IsProductExist(string product)
        {
            return _context.Products.Any(p => p.ProductName.Equals(product));
        }

        public IEnumerable<Product> Search(string query, int count)
        {
            return _context.Products.Where(p => p.ProductName.Contains(query)).Take(count).OrderBy(x => x.ProductName).ToList();
        }
    }
}
