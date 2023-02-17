using PrepMe.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepMe.DAL.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        public bool HasCategory(string category);
    }
       
    
    
}
