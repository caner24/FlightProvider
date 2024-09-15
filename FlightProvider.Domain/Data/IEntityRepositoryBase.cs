using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FlightProvider.Domain.Data
{
    public interface IEntityRepositoryBase<TEntity> where TEntity : IEntity, new()
    {
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, bool isTracking = false);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter, bool isTracking = false);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
