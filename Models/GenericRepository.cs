using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProdHiFiApi.Data;
using ProdHiFiApi.Models.Interface;

namespace ProdHiFiApi.Models
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private ProductDbContext _dbContext;
        public GenericRepository(ProductDbContext prodDbContext)
        {
            _dbContext = prodDbContext;
        }

        public void Create(T customObject)
        {
            _dbContext.Set<T>().Add(customObject);
        }

        public void Remove(T customObject)
        {
            _dbContext.Set<T>().Remove(customObject);
        }

        public IQueryable<T> GetAll()
        {
            return this._dbContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> GetByCustomCondition(Expression<Func<T, bool>> expression)
        {
            return this._dbContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Update(T customObject)
        {
            _dbContext.Set<T>().Update(customObject);
        }

        public IQueryable<T> GetByCustomConditionForEditing(Expression<Func<T, bool>> expression)
        {
            return this._dbContext.Set<T>().Where(expression);
        }
    }
}