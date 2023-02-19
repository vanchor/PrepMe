using PrepMe.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepMe.DAL.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        bool IsProductExist(string product);
        IEnumerable<Product> Search(string query, int count);
    }
}
