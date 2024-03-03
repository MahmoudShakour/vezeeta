using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Interfaces.Repos;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repos
{
    public class BaseRepo<T, TId> : IBaseRepo<T, TId> where T : class
    {
        private readonly ApplicationDbContext _context;

        public BaseRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Count(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().CountAsync(predicate);
        }

        public async Task<int> Count()
        {
            return await _context.Set<T>().CountAsync();
        }

        public async Task<T> Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<List<T>> FindAll(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<T?> FindOne(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetAll(int skip, int take)
        {
            return await _context.Set<T>().Skip(skip).Take(take).ToListAsync();
        }

        public async Task<List<T>> GetAll(int skip, int take,string[] includes)
        {
            IQueryable<T> query= _context.Set<T>();

            foreach (string include in includes){
                query.Include(include);
            }

            return await query.Skip(skip).Take(take).ToListAsync();
        }

        public async Task<List<T>> GetAll(int skip, int take,Expression<Func<T,bool>> predicate,string[] includes)
        {
            IQueryable<T> query= _context.Set<T>();

            foreach (string include in includes){
                query.Include(include);
            }

            return await query.Where(predicate).ToListAsync();
        }

        public async Task<T?> GetById(TId id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
    }
}