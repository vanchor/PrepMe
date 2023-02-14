using PrepMe.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepMe.DAL.Implementations
{
    internal class BaseRepository<T> : IBaseRepository<T> where T : class
    {
    }
}
