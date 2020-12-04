namespace CandidatosProyect.Client.Controllers
{
    using CandidatosProyect.Api.Client;
    using CandidatosProyect.Client.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Diagnostics;
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ICandidatosClient candidatosClient)
        {
            this._logger = logger;
        }

        public IActionResult Index() => this.View();

        public IActionResult Privacy() => this.View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
    }
}