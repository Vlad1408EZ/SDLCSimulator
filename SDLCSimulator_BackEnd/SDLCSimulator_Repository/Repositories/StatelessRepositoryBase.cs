using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SDLCSimulator_Data;
using SDLCSimulator_Repository.Interfaces;

namespace SDLCSimulator_Repository.Repositories
{
    public abstract class StatelessRepositoryBase<T> : IStatelessRepository<T> where T : class
    {
        protected IDbContextFactory<SDLCSimulatorDbContext> _contextFactory;

        protected StatelessRepositoryBase(IDbContextFactory<SDLCSimulatorDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IQueryable<T> GetAll()
        {
            var context = _contextFactory.CreateDbContext();

            return context.Set<T>().AsNoTracking();
        }

        public IQueryable<T> GetByCondition(Expression<Func<T,bool>> expression)
        {
            var context = _contextFactory.CreateDbContext();

            return context.Set<T>().Where(expression).AsNoTracking();   
        }

        public async Task<T> GetSingleByConditionAsync(Expression<Func<T,bool>> expression)
        {
            var context = _contextFactory.CreateDbContext();

            return await context.Set<T>().FirstOrDefaultAsync(expression);
        }

        public async System.Threading.Tasks.Task CreateAsync(T entity)
        {
            var context = _contextFactory.CreateDbContext();

            context.Set<T>().Add(entity);

            await context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task UpdateAsync(T entity)
        {
            var context = _contextFactory.CreateDbContext();

            context.Set<T>().Update(entity);

            await context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task RemoveAsync(T entity)
        {
            var context = _contextFactory.CreateDbContext();

            context.Set<T>().Remove(entity);

            await context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task CreateRangeAsync(ICollection<T> collection)
        {
            var context = _contextFactory.CreateDbContext();

            context.Set<T>().AddRange(collection);

            await context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task UpdateRangeAsync(ICollection<T> collection)
        {
            var context = _contextFactory.CreateDbContext();

            context.Set<T>().UpdateRange(collection);

            await context.SaveChangesAsync();
        }
        public async System.Threading.Tasks.Task RemoveRangeAsync(ICollection<T> collection)
        {
            var context = _contextFactory.CreateDbContext();

            context.Set<T>().RemoveRange(collection);

            await context.SaveChangesAsync();
        }
    }
}
