using MinhaLoja.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaLoja.Domain.Interfaces
{
    public interface IToken
    {
        public string GenerateToken(Usuario user,List<string> claims);
        public string GenerateRefreshToken();

    }
}
