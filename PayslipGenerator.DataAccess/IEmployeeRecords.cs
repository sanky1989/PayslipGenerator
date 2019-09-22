using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace PayslipGenerator.DataAccess
{
    public interface IEmployeeRecords
    {
        IEnumerable<T> ReadRecords<T>(Stream stream);
        byte[] WriteRecordsToBytes<T>(IEnumerable<T> records);


    }
}
