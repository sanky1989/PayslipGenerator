using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PayslipGenerator.Web.Models;
using PayslipGenerator.Service;
using Microsoft.Extensions.Logging;
using PayslipGenerator.Model;
using Microsoft.AspNetCore.Http;

namespace PayslipGenerator.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPayslipService _payslipService;
        private readonly ILogger<HomeController> _logger;
        private static string PayslipDataSessionKey = "payslipcsv";
        private const string FileDownloadName = "payslips.csv";

        public HomeController(IPayslipService payslipService, ILogger<HomeController> logger)
        {
            _payslipService = payslipService ?? throw new ArgumentNullException(nameof(payslipService));
            _logger = logger;
        }
   
        public IActionResult Index()
        {
            //return View();
            return View(new InputViewModel());
        }

        [HttpPost]
        public IActionResult Index([FromForm] InputViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Errors = "Some errors were detected in your submission. Please correct any field errors and re-submit.";
                return View(viewModel);
            }

            var employees = ReadEmployeesFromViewModel(viewModel);
            if (employees != null)
            {
                var employeesList = employees.ToList();
                try
                {
                    GeneratePayslips(employeesList, viewModel);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error generating Payslips from uploaded Employee file");
                    viewModel.Errors = $"Error generating Payslips from uploaded Employee file: {e.Message}";
                    return View(viewModel);
                }

                viewModel.Employees = employeesList.Select(e => new EmployeeViewModel(e));
            }
            return View(viewModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DownloadFile()
        {
            var bytes = HttpContext.Session.Get(PayslipDataSessionKey);
            return File(bytes, "text/csv", FileDownloadName);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
     

        #region Payslip Service
        private IEnumerable<Employee> ReadEmployeesFromViewModel(InputViewModel viewModel)
        {
            var stream = viewModel.EmployeeInput.OpenReadStream();
            IEnumerable<Employee> employees = null;
            try
            {
                employees = _payslipService.ReadRecords(stream).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error reading Employees from CSV File");
                viewModel.Errors = $"Error reading Employees from CSV File: {e.Message}";
            }

            return employees;
        }

        private void GeneratePayslips(IEnumerable<Employee> employees, InputViewModel viewModel)
        {
            var validation = new Validation();
            var result = _payslipService.GeneratePayslips(employees, validation);

            if (validation.IsValid)
            {
                var payslips = result.ToList();
                viewModel.PaySlips = payslips;
                StoreToSessionCache(payslips);
            }
            else
            {
                viewModel.Errors = $"Validation Error: {String.Join(",", validation.Errors)}";
            }
        }

        private void StoreToSessionCache(IEnumerable<PaySlip> payslips)
        {
            var bytes = _payslipService.WriteToByteArray(payslips);
            HttpContext.Session.Set(PayslipDataSessionKey, bytes);
        }



        #endregion
    }
}
