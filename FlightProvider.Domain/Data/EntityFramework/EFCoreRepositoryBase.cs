using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FlightProvider.Domain.Data.EntityFramework
{
    public class EFCoreRepositoryBase<TContext, TEntity> : IEntityRepositoryBase<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {

        private readonly TContext _tContext;
        public EFCoreRepositoryBase(TContext tContext)
        {
            _tContext = tContext;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _tContext.AddAsync(entity);
            await _tContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _tContext.Remove(entity);
            await _tContext.SaveChangesAsync();
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter, bool isTracking = false)
        {
            return isTracking == true ? _tContext.Set<TEntity>().Where(filter) : _tContext.Set<TEntity>().Where(filter).AsNoTracking();
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, bool isTracking = false)
        {
            return filter == null ? isTracking == false ? _tContext.Set<TEntity>().AsNoTracking() : _tContext.Set<TEntity>() : isTracking == false ? _tContext.Set<TEntity>().Where(filter).AsNoTracking() : _tContext.Set<TEntity>();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _tContext.Update(entity);
            await _tContext.SaveChangesAsync();
            return entity;
        }
    }
}
