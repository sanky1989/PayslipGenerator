using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using PayslipGenerator.Model;


namespace PayslipGenerator.Web.Models
{
    public class InputViewModel
    {
        [Required(ErrorMessage = "An employee file is required")]
        [Display(Description = "CSV File")]
        public IFormFile EmployeeInput { get; set; }

        public IEnumerable<PaySlip> PaySlips { get; set; }
        public IEnumerable<EmployeeViewModel> Employees { get; set; }
        public string Errors { get; set; }
    }
}
