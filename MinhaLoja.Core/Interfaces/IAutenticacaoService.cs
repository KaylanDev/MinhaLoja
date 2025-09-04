using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaLoja.Core.Interfaces
{
    public interface IAutenticacaoService
    {
        public Result RegistrarUsuario(string email, string senha);
        public Result AutenticarUsuario(string email, string senha);
    }
}
