using System;
using System.Collections.Generic;
using System.Text;

namespace PayslipGenerator.Model
{
    public interface IValidation
    {
        IEnumerable<string> Errors { get; }
        void AddErrorMessage(string message);
        bool IsValid { get; }
    }
}
