using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MinhaLoja.Domain.Interfaces;
using MinhaLoja.Domain.Models;
using MinhaLoja.Infrastructure.ContextPgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaLoja.Infrastructure.TokenGenerate;

public class Token : IToken
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<UserIdentity> _userManager;

    public Token(IConfiguration configuration, UserManager<UserIdentity> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }

    public string GenerateRefreshToken()
    {
        throw new NotImplementedException();
    }

    public string GenerateToken(Usuario user, List<string> claims)
    {
        throw new NotImplementedException();
    }
}
