using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MinhaLoja.Core.Interfaces;
using MinhaLoja.Domain.Models;
using MinhaLoja.Domain.Interface;
using MinhaLoja.Domain.Interfaces;
using MinhaLoja.Infrastructure.Repository;
using MinhaLoja.Models;

namespace MinhaLoja.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProdutoService _produtoRepository;
        public HomeController(ILogger<HomeController> logger,IProdutoService produtoRepository)
        {
            _logger = logger;
            _produtoRepository = produtoRepository;
        }

        public IActionResult Index()
        {

            return View();

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
