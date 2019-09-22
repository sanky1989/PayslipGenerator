using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using CsvHelper;



namespace PayslipGenerator.DataAccess
{
    public sealed class EmployeeRecords:IEmployeeRecords 
    {
        public IEnumerable<T> ReadRecords<T>(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            using (var csvReader = new CsvReader(reader))
            {
                csvReader.Configuration.RegisterClassMap<EmployeeRecordsMapping>();
                csvReader.Configuration.PrepareHeaderForMatch = header => header.Replace("_", string.Empty).ToLowerInvariant();     
                return csvReader.GetRecords<T>().ToArray();
            }
        }

        public byte[] WriteRecordsToBytes<T>(IEnumerable<T> records)
        {
            using (var memoryStream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memoryStream))
            using (var csvWriter = new CsvWriter(streamWriter))
            {
                csvWriter.WriteRecords(records);
                streamWriter.Flush();
                return memoryStream.ToArray();
            }
        }
    }
}
