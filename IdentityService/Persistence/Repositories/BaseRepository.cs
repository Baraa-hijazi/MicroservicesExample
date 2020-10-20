using Microsoft.EntityFrameworkCore;
using IdentityService.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IdentityService.Persistence.Contexts;
using IdentityService.Persistence.Interfaces;

namespace IdentityService.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context = null;
        private readonly DbSet<T> table = null;
     
        public BaseRepository(ApplicationDbContext context) 
        {
            _context = context;
            table = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate = null, string Includes = null)
        {
            if(predicate != null)
            {
            var query = table.Where(predicate);
                if(Includes !=null)
                {
                    foreach (var str in Includes.Split(","))
                        query = query.Include(str).AsQueryable();
                }
                return await query.ToListAsync();
            }
            return await table.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllIncluded(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] Includes)
        {
            var query = table.Where(predicate);
            foreach (var Include in Includes)
            {
                query = query.Include(Include);
            }
            return await query.ToListAsync();
        }

        public async Task<PagedResultDto<T>> GetAllIncludedPagnation(Expression<Func<T, bool>> predicate = null, int? pageIndex = 1, int? pageSize = 10, params Expression<Func<T, object>>[] Includes)
        {
            if (pageIndex <= 0) pageIndex = 1;
            if (pageSize <= 0) pageSize = 10;

            var query = table.Where(predicate);
            foreach (var Include in Includes)
            {
                query = query.Include(Include);
            }

            return new PagedResultDto<T>
            {
                TotalCount = await query.CountAsync(),
                Result = await query.Skip((int)((pageIndex - 1) * pageSize)).Take((int)pageSize).ToListAsync()
            };
        }

        public async Task<T> GetById(object id)
        { 
            return await table.FindAsync(id);
        }

        public void Add(T obj)
        { 
            table.Add(obj); 
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public T Delete(T existing)
        {
            table.Remove(existing);
            return existing;
        }
        
        public Task DeleteRange(List<T> entites)
        {
            table.RemoveRange(entites);
            return Task.CompletedTask;
        }
    } 
}
