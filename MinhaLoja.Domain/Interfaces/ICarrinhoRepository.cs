using MinhaLoja.Domain.Interface;
using MinhaLoja.Domain.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaLoja.Domain.Interfaces
{
    public interface ICarrinhoRepository : IRepository<Carrinho>
    {
    public Task<Carrinho> GetByUserIdAsync(ObjectId usuarioId);
    }
}
