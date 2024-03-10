using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp;
using Xunit;

namespace ConsoleAppTests
{
    public class Tests
    {
        [Fact]
        public async Task ImportAndPrintData_ValidFile_ShouldProcessData()
        {
            // Arrange
            const string mockFilePath = "test.csv";
            var dataReader = new DataReader();

            var stringBuilder = new StringBuilder();
            using (var stringWriter = new StringWriter(stringBuilder))
            {
                Console.SetOut(stringWriter);

                // Act
                await dataReader.ImportAndPrintData(mockFilePath);

                // Assert
                var output = stringBuilder.ToString();
                Assert.Contains("Database 'AdventureWorks2016_EXT' (16 tables)", output);
                Assert.Contains("Column 'ModifiedDate' with datetime data type with no nulls", output);
            }

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
        }

        [Fact]
        public async Task ImportAndPrintData_InvalidFile_ShouldInformUser()
        {
            // Arrange
            const string mockFilePath = "invalid.csv";
            var dataReader = new DataReader();

            var stringBuilder = new StringBuilder();
            using (var stringWriter = new StringWriter(stringBuilder))
            {
                Console.SetOut(stringWriter);

                // Act
                await dataReader.ImportAndPrintData(mockFilePath);

                // Assert
                var output = stringBuilder.ToString();
                Assert.Contains("Error: File not found", output);
            }

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
        }

    }
}
