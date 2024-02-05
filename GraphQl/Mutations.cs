using GraphQlDemo.EmployeeHelpers;
using GraphQlDemo.Exceptions;
using GraphQlDemo.Models;
using GraphQlDemo.Services;
using MongoDB.Driver;

namespace GraphQlDemo.GraphQl;

public class Mutation
{
    private readonly DatabaseService<Employee> _databaseService;

    public Mutation(DatabaseService<Employee> databaseService)
    {
        _databaseService = databaseService;
    }

    public Employee CreateEmployee(string taxFileNumber, string firstName, string lastName, DateTime dateOfBirth,
        DateTime hireDate)
    {
        if (string.IsNullOrEmpty(taxFileNumber) || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
        {
            throw new ArgumentException("Invalid input parameters");
        }

        var employeeNumberGenerator = new EmployeeNumberGenerator(_databaseService);
        var employeeNumber = employeeNumberGenerator.GenerateEmployeeNumber(hireDate, taxFileNumber);
        var newEmployee = new Employee(employeeNumber, taxFileNumber, firstName, lastName, dateOfBirth, hireDate);

        try
        {
            _databaseService.InsertOne(newEmployee);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error inserting employee: {ex.Message}");
            throw;
        }

        return newEmployee;
    }

    public Employee UpdateEmployeeByEmployeeNumber(string employeeNumber, Input.EmployeeUpdate update)
    {
        if (string.IsNullOrEmpty(employeeNumber) || update == null)
        {
            throw new ArgumentException("Invalid input parameters");
        }

        var filter = Builders<Employee>.Filter.Eq(e => e.EmployeeNumber, employeeNumber);
        var updateBuilder = Builders<Employee>.Update;

        var updateDefinitions = new List<UpdateDefinition<Employee>>();

        if (update.Department != null)
        {
            updateDefinitions.Add(updateBuilder.Set(e => e.Department, update.Department));
        }

        if (update.Position != null)
        {
            updateDefinitions.Add(updateBuilder.Set(e => e.Position, update.Position));
        }

        if (update.Salary != null)
        {
            updateDefinitions.Add(updateBuilder.Set(e => e.Salary, update.Salary));
        }

        if (update.SickLeaveHours != null)
        {
            updateDefinitions.Add(updateBuilder.Set(e => e.SickLeaveHours, update.SickLeaveHours));
        }

        if (update.AnnualLeaveHours != null)
        {
            updateDefinitions.Add(updateBuilder.Set(e => e.AnnualLeaveHours, update.AnnualLeaveHours));
        }

        if (update.UsedSickLeaveHours != null)
        {
            updateDefinitions.Add(updateBuilder.Set(e => e.UsedSickLeaveHours, update.UsedSickLeaveHours));
        }

        if (update.UsedAnnualLeaveHours != null)
        {
            updateDefinitions.Add(updateBuilder.Set(e => e.UsedAnnualLeaveHours, update.UsedAnnualLeaveHours));
        }

        if (update.ResidentialAddresses != null)
        {
            updateDefinitions.Add(updateBuilder.Set(e => e.ResidentialAddresses, update.ResidentialAddresses));
        }

        if (update.PostalAddresses != null)
        {
            updateDefinitions.Add(updateBuilder.Set(e => e.PostalAddresses, update.PostalAddresses));
        }

        if (update.PhoneNumbers != null)
        {
            updateDefinitions.Add(updateBuilder.Set(e => e.PhoneNumbers, update.PhoneNumbers));
        }

        if (update.Emails != null)
        {
            updateDefinitions.Add(updateBuilder.Set(e => e.Emails, update.Emails));
        }

        if (update.IsActive != null)
        {
            updateDefinitions.Add(updateBuilder.Set(e => e.IsActive, update.IsActive));
        }

        if (update.Skills != null)
        {
            updateDefinitions.Add(updateBuilder.Set(e => e.Skills, update.Skills));
        }

        var combinedUpdateDefinition = updateBuilder.Combine(updateDefinitions);

        
        var options = new FindOneAndUpdateOptions<Employee>
        {
            ReturnDocument = ReturnDocument.After,
        };
        
        try
        {
            var updatedEmployee = _databaseService.FindAndUpdateByFilter(filter, combinedUpdateDefinition, options);

            if (updatedEmployee == null)
            {
                throw new NotFoundException($"Employee with EmployeeNumber {employeeNumber} not found");
            }

            return updatedEmployee;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating employee: {ex.GetType().Name} - {ex.Message}");
            throw;
        }
    }
    
    public bool DeleteEmployeeByEmployeeNumber(string employeeNumber)
    {
        var filter = Builders<Employee>.Filter.Eq(e => e.EmployeeNumber, employeeNumber);
        return _databaseService.DeleteByFilter(filter);
    }
}
