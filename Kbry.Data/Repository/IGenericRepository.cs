using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Kbry.Data.Repository
{
    public interface IGenericRepository<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task SaveAsync();
        bool HasChanges();
        void Add(T model);
        void Remove(int id);
        void Update(T entityToUpdate);
        Task<IEnumerable<T>> GetByPredicate(Expression<Func<T, bool>> filter);
    }
}