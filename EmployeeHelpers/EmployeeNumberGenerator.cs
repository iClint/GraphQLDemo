using GraphQlDemo.Models;
using GraphQlDemo.Services;

namespace GraphQlDemo.EmployeeHelpers

{
    public class EmployeeNumberGenerator
    {
        private readonly DatabaseService<Employee> _databaseService;

        public EmployeeNumberGenerator(DatabaseService<Employee> databaseService)
        {
            _databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
        }

        public string GenerateEmployeeNumber(DateTime hireDate, string taxFileNumber)
        {
            var hireDateString = hireDate.ToString("yyyyMMdd");
            var last3TaxFileNumberDigits = taxFileNumber[^3..];
            var employeeNumber = $"{hireDateString}-{last3TaxFileNumberDigits}-{NextEmployeeIndex()}";
            return employeeNumber;
        }

        private async Task<int> NextEmployeeIndex()
        {
            var employees = await _databaseService.GetAll("_id", true);

            if (employees.Count == 0) return 1;
            var lastEmployee = employees.Last();
            var lastEmployeeNumberParts = lastEmployee.EmployeeNumber.Split('-');
            return int.TryParse(lastEmployeeNumberParts.Last(), out var result) ? result + 1 : 1;
        }
    }
}