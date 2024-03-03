using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Interfaces.Repos
{
    public interface IBaseRepo<T,TId>
    {
        Task<T?> GetById(TId id);
        Task<List<T>> GetAll();
        Task<List<T>> GetAll(int skip,int take);
        void Delete(T entity);
        Task<T> Create(T entity);
        Task<List<T>> FindAll(Expression<Func<T, bool>> predicate);
        Task<T?> FindOne(Expression<Func<T, bool>> predicate);
        Task<int> Count(Expression<Func<T, bool>> predicate);
        Task<int> Count();
    }
}