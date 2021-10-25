using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SDLCSimulator_Repository.Interfaces
{
    public interface IStatelessRepository<T>
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetByCondition(Expression<Func<T,bool>> expression);
        Task<T> GetSingleByConditionAsync(Expression<Func<T,bool>> expression);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task CreateRangeAsync(ICollection<T> collection);
        Task UpdateRangeAsync(ICollection<T> collection);
        Task RemoveRangeAsync(ICollection<T> collection);
    }
}
