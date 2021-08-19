using System;
using System.Linq;
using System.Linq.Expressions;

namespace ProdHiFiApi.Repository.Interface
{
    public interface IGenericRepository<T>
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetByCustomCondition(Expression<Func<T, bool>> expression);
        IQueryable<T> GetByCustomConditionForEditing(Expression<Func<T, bool>> expression);
        void Create(T customObject);
        void Update(T customObject);
        void Remove(T customObject);
    }
}