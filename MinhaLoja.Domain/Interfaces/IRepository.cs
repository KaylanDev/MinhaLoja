using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaLoja.Domain.Interface;

public interface IRepository<T>
{
    public Task<ICollection<T>> GetAllAsync();
    public Task<T> GetByIdAsync(ObjectId id);
    public Task<T> CreateAsync(T entity);
    public Task<T> UpdateAsync(T entity);
    public Task<bool> DeleteAsync(T entity);
    
}
