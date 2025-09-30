using Microsoft.EntityFrameworkCore;
using MinhaLoja.Domain.Interface;
using MinhaLoja.Domain.Models;
using MinhaLoja.Infrastructure.Context;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaLoja.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
           var entities = await _context.Set<T>().ToListAsync();
            return entities;
        }

        public async Task<T> GetByIdAsync(ObjectId id)
        {
           var entity = await _context.Set<T>().FindAsync(id);
            return entity;
        }


        public async Task<T> CreateAsync(T entity)
        {
            var createdEntity = await _context.Set<T>().AddAsync(entity);
           var result = await _context.SaveChangesAsync();

            var i = result == 1 ? createdEntity.Entity : null;
            return i;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            var deleted = _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }



        public async Task<T> UpdateAsync(T entity)
        {
            var entityUpdated = _context.Set<T>().Update(entity);
            var result = await _context.SaveChangesAsync();

            var i = result == 1 ? entityUpdated.Entity : null;
            return i;
           
        }

    }
}
    