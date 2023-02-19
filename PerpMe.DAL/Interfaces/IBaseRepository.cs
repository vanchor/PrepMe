using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepMe.DAL.Interfaces
{
    public interface IBaseRepository<T>
        where T : class
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        void Add(T item);
        void AddRange(IEnumerable<T> items);

        T Update(T item);
        void Remove(T item);
        void RemoveRange(IEnumerable<T> entities);

        void SaveChanges();
        Task SaveChangesAsync();
        Task<IEnumerable<T>> GetAllAsync();
    }
}
