using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace PayslipGenerator.Model
{
    public class Validation : IValidation
    {
        private List<string> _Errors = new List<string>();





        //Assign Values
        public IEnumerable<string> Errors => _Errors;       
        public void AddErrorMessage(string message)
        {
            _Errors.Add(message);
        }

        public bool IsValid => !Errors.Any();
    }
}
