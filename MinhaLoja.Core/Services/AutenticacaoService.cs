using FluentResults;
using MinhaLoja.Core.Interfaces;
using MinhaLoja.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MinhaLoja.Core.Services
{
    public class AutenticacaoService : IAutenticacaoService
    {
        private readonly AppDbContext _context;

        public AutenticacaoService(AppDbContext context)
        {
            _context = context;
        }

        public Result AutenticarUsuario(string email, string senha)
        {
            throw new NotImplementedException();
        }

        public Result RegistrarUsuario(string email, string senha)
        {
            throw new NotImplementedException();
        }
    }
}
