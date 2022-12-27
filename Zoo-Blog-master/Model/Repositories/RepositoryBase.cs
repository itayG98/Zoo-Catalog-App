using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">A type of object which has a DBSet <T> in the requested DBContext</typeparam>
    /// <typeparam name="K">The <T> primary key type</typeparam>
    /// <typeparam name="TDBContext">A class which inherit from EntityFrameWorkCore.DBContext</typeparam>
    public abstract class RepositoryBase<T, TDBContext> : IRepositoryBase<T, TDBContext> 
        where T : class 
        where TDBContext : DbContext
    {
        protected TDBContext DbContext { get; set; }
        public RepositoryBase(TDBContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        => await DbContext.Set<T>().Where(expression).ToListAsync();

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        => DbContext.Set<T>().Where(expression);
        public async Task<T> FindFirstByConditionOrDefaultAsync(Expression<Func<T, bool>> expression)
        => await DbContext?.Set<T>().FirstOrDefaultAsync(expression);

        public T FindFirstByConditionOrDefault(Expression<Func<T, bool>> expression)
        => DbContext.Set<T>().FirstOrDefault(expression)!;

        public async Task<IEnumerable<T>> FindAllAsync()
                => await DbContext.Set<T>().ToListAsync();

        public IEnumerable<T> FindAll()
        => DbContext.Set<T>().ToList();

        public async Task<T> FindByIDAsync(Guid id)
        => await DbContext.Set<T>().FindAsync(id);

        public T Find(Guid id)
        => DbContext.Set<T>().Find(id);
        public T FindByReference(T entity)
        => DbContext.Set<T>().FirstOrDefault(x => x.Equals(entity));

        public async Task<bool> CreateAsync(T entity)
        {
            if (entity == null || FindByReference(entity) != null)
                return false;
            ValueTask<EntityEntry<T>> entityEntry = DbContext.Set<T>().AddAsync(entity);
            return await entityEntry.
                AsTask().
                ContinueWith(_ =>
                {
                    if (entityEntry.IsCompletedSuccessfully && entityEntry.Result.State == EntityState.Added)
                    {
                        Save();
                        return true;
                    }
                    return false;
                });
        }

        public bool Create(T entity)
        {
            if (entity == null || FindByReference(entity) != null)
                return false;
            EntityEntry<T> entityEntry = DbContext.Set<T>().Add(entity);
            if (entityEntry.State is EntityState.Added)
            {
                Save();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            if (entity == null || FindByReference(entity) == null)
                return false;
            Task<EntityEntry<T>> entityEntry = new(() => DbContext.Set<T>().Update(entity));
            return await entityEntry.ContinueWith(_ =>
            {
                if (entityEntry.Result.State == EntityState.Modified)
                {
                    Save();
                    return true;
                }
                return false;
            });

        }

        public bool Update(T entity)
        {
            if (entity == null || FindByReference(entity) == null)
                return false;
            EntityEntry<T> entityEntry = DbContext.Set<T>().Update(entity);
            if (entityEntry.State is EntityState.Modified)
            {
                Save();
                return true;
            }
            return false;
        }

        public bool Delete(T entity)
        {
            if (entity == null || FindByReference(entity) == null)
                return false;
            EntityEntry<T> entityEntry = DbContext.Set<T>().Remove(entity);
            if (entityEntry.State == EntityState.Deleted)
            {
                Save();
                return true;
            }
            return false;
        }

        public bool Delete(Guid id)
        {
            T entity = Find(id);
            if (entity != null)
               return Delete(entity);
            return false;

        }

        public async Task<bool> DeleteAsync(T entity)
        {
            if (entity == null || FindByReference(entity) == null)
                return false;
            Task<EntityEntry<T>> entityEntry = new Task<EntityEntry<T>>(() => DbContext.Set<T>().Remove(entity));
            return await entityEntry.ContinueWith(_ =>
            {
                if (entityEntry.Result.State == EntityState.Deleted)
                {
                    Save();
                    return true;
                }
                return false;
            });
        }

        public void Save() => DbContext.SaveChanges();
        public async Task SaveAsync() => await DbContext.SaveChangesAsync();

    }
}
