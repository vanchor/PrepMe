using PrepMe.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepMe.DAL.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly PrepMeDbContext _context;
        // CONTEXT
        public BaseRepository(PrepMeDbContext context)
        {
            _context = context;
        }

        public void Add(T item)
        {
            // return _context.Set<T>().Tolist();
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<T> items)
        {
            _context.Set<T>().AddRange(items);
        }

        public IEnumerable<T> Get()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public T Update(T item)
        {
            throw new NotImplementedException();
        }
    }
}
