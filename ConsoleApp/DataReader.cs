using System.Text.RegularExpressions;
namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class DataReader
    {
        private readonly List<ImportedObject> _importedObjects = new List<ImportedObject>();
        private Dictionary<string, List<ImportedObject>> _tablesByDatabase;
        private Dictionary<string, List<ImportedObject>> _columnsByTable;

        public async Task ImportAndPrintData(string fileToImport)
        {
            try
            {
                ValidateInput(ref fileToImport);
                using (var streamReader = new StreamReader(fileToImport))
                {

                    await ProcessData(streamReader);

                    AssignNumberOfChildren();

                    PrintData();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        private async Task ProcessData(StreamReader streamReader)
        {

            var importedLines = new List<string>();
            while (!streamReader.EndOfStream)
            {
                var line = await streamReader.ReadLineAsync();
                if (!string.IsNullOrWhiteSpace(line))
                    importedLines.Add(line);
            }

            for (var i = 0; i < importedLines.Count; i++)
            {
                var importedLine = importedLines[i];
                var values = importedLine.Split(';');
                if (values.Length < 7)
                {
                    Array.Resize(ref values, 7);
                }
                var importedObject = new ImportedObject
                {
                    Type = values[0],
                    Name = values[1],
                    Schema = values[2],
                    ParentName = values[3],
                    ParentType = values[4],
                    DataType = values[5],
                    IsNullable = values[6]
                };
                _importedObjects.Add(importedObject);
            }

            // clear and correct imported data
            foreach (var importedObject in _importedObjects)
            {
                importedObject.Type = CleanString(importedObject.Type).ToUpper();
                importedObject.Name = CleanString(importedObject.Name);
                importedObject.Schema = CleanString(importedObject.Schema);
                importedObject.ParentName = CleanString(importedObject.ParentName);
                importedObject.ParentType = CleanString(importedObject.ParentType);
            }

            _tablesByDatabase = _importedObjects.Where(x => x.Type == ConstTypes.Table)
                .GroupBy(x => x.ParentName)
                .ToDictionary(g => g.Key, g => g.ToList());
            _columnsByTable = _importedObjects.Where(x => x.Type == ConstTypes.Column)
                .GroupBy(x => x.ParentName)
                .ToDictionary(g => g.Key, g => g.ToList());
        }
        private void PrintData()
        {

            foreach (var db in _importedObjects.Where(x => x.Type == ConstTypes.Database))
            {
                Console.WriteLine($"Database '{db.Name}' ({db.NumberOfChildren} tables)");
                if (!_tablesByDatabase.TryGetValue(db.Name, out var dbTables))
                    continue;
                foreach (var table in dbTables)
                {
                    PrintTable(table);
                }
            }
        }
        private void PrintTable(ImportedObject table)
        {

            Console.WriteLine($"\tTable '{table.Schema}.{table.Name}' ({table.NumberOfChildren} columns)");
            if (!_columnsByTable.TryGetValue(table.Name, out var tableColumns))
                return;
            PrintColumns(tableColumns);
        }
        private static void PrintColumns(List<ImportedObject> tableColumns)
        {

            foreach (var column in tableColumns)
            {
                Console.WriteLine($"\t\tColumn '{column.Name}' with {column.DataType} data type {(column.IsNullable == "1" ? "accepts nulls" : "with no nulls")}");
            }
        }

        private static void ValidateInput(ref string fileToImport)
        {
            if (string.IsNullOrWhiteSpace(fileToImport))
            {
                throw new ArgumentException("File name cannot be empty");
            }

            fileToImport = fileToImport.Trim();

            fileToImport += fileToImport.EndsWith(Constants.CsvExtension) ? "" : Constants.CsvExtension;

            if (!File.Exists(fileToImport))
            {
                throw new FileNotFoundException("File not found");
            }
        }

        private static string CleanString(string input)
        {
            return input == null ? string.Empty : Regex.Replace(input, @"\s+", "").Trim();
        }

        private void AssignNumberOfChildren()
        {
            var parentChildMap = _importedObjects.GroupBy(obj => new { obj.ParentType, obj.ParentName })
                .ToDictionary(group => group.Key, group => group.Count());

            foreach (var obj in _importedObjects)
            {
                if (parentChildMap.TryGetValue(new { ParentType = obj.Type, ParentName = obj.Name }, out var childCount))
                {
                    obj.NumberOfChildren = childCount;
                }
            }
        }
    }

    internal class ImportedObject : ImportedObjectBaseClass
    {
        public string Schema { get; set; }

        public string ParentName { get; set; }
        public string ParentType { get; set; }

        public string DataType { get; set; }
        public string IsNullable { get; set; }

        public double NumberOfChildren { get; set; }
    }

    internal class ImportedObjectBaseClass
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }

    internal static class Constants
    {
        public const string CsvExtension = ".csv";
    }

    internal static class ConstTypes
    {
        public const string Database = "DATABASE";
        public const string Table = "TABLE";
        public const string Column = "COLUMN";
    }
}
