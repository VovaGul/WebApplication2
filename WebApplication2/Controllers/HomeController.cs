using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(new CalculatorForm());
        }

        [HttpPost]
        public IActionResult Index(CalculatorForm calculatorForm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            return RedirectToAction("PaymentSchedule", calculatorForm);
        }

        [HttpPost]
        public IActionResult PaymentSchedule(CalculatorForm calculatorForm)
        {
            var paymentSchedule = Models.PaymentSchedule.Calculate(calculatorForm, DateTime.Now);
            return View(paymentSchedule);
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
