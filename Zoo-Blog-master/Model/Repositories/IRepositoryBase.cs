using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repositories
{
    /// <summary>
    /// A base interface of data repository including both async and sync CRUD operation
    /// </summary>
    /// <typeparam name="T">Type of object</typeparam>
    /// <typeparam name="TDBContext">DBContext derived class </typeparam>

    public interface IRepositoryBase<T,TDBContext>
        where T : class
        where TDBContext : DbContext
    {
        //IQueryable<T> GetAllSortedBy(IComparer<T> comp); //May add later
        public Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);
        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression);
        public Task<IEnumerable<T>> FindAllAsync();  
        public IEnumerable<T> FindAll();
        public Task<T> FindByIDAsync(Guid id);  
        public T Find(Guid id);
        public Task<bool> CreateAsync(T entity);   
        public bool Create(T entity);
        public Task<bool> UpdateAsync(T entity); 
        public bool Update(T entity);
        public bool Delete(T entity);
        public Task SaveAsync();  
        public void Save();

    }
}
