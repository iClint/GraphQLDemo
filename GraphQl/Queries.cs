using System.Linq.Expressions;
using GraphQlDemo.Models;
using GraphQlDemo.Services;
using MongoDB.Driver;

namespace GraphQlDemo.GraphQl;

public class Query
{
    private readonly DatabaseService<Employee> _databaseService;

    public Query(DatabaseService<Employee> databaseService)
    {
        _databaseService = databaseService;
    }

    public List<Employee> GetAllEmployees() => _databaseService.GetAll();

    public Employee GetEmployeeByEmployeeNumber(string employeeNumber)
    {
        var filter = Builders<Employee>.Filter.Eq(e => e.EmployeeNumber, employeeNumber); 
        return _databaseService.GetByFilter(filter);
    }
}