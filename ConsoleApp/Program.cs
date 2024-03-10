namespace ConsoleApp
{
    using System;
    using System.Threading.Tasks;

    internal abstract class Program
    {
        private static async Task Main(string[] args)
        {
            var reader = new DataReader();

            var fileName = GetFileName(args);

            await reader.ImportAndPrintData(fileName);
        }

        private static string GetFileName(string[] args)
        {

            string fileName;
            if (args.Length > 0)
            {
                fileName = args[0];
            }
            else
            {
                Console.WriteLine("Provide csv file name:");
                fileName = Console.ReadLine();
            }
            return fileName;
        }
    }
}
