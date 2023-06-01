using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories;

public interface IRepositoryBase<T> where T : BaseEntity
{ 
    DbSet<T> Table { get; }
}