using System.Linq.Expressions;
using Domain.Entities.Common;

namespace Application.Repositories;

public interface IReadRepository<T> : IRepositoryBase<T> where T : BaseEntity
{
    IQueryable<T> GetALl(bool tracking = true);

    IQueryable<T> GetWhere(Expression<Func<T,bool>> method, bool tracking = true);

    Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);

    Task<T> GetByIdAsync(string id, bool tracking = true);
    
}