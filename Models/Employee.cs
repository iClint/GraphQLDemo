using GraphQlDemo.Enums;
using MongoDB.Bson;

namespace GraphQlDemo.Models;

public class Employee
{
    // Default constructor for HotChocolate
    public Employee()
    {
    }
    
    public Employee(string employeeNumber,string taxFileNumber, string firstName, string lastName, DateTime dateOfBirth, DateTime hireDate)
    {
        EmployeeNumber = employeeNumber;
        TaxFileNumber = taxFileNumber;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        HireDate = hireDate;
    }
    
    public ObjectId _id { get; set; }
    public string EmployeeNumber { get; set; }
    public string TaxFileNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender? Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Department? Department { get; set; }
    public Position? Position { get; set; }
    public decimal? Salary { get; set; }
    public DateTime HireDate { get; set; }
    public double SickLeaveHours { get; set; }
    public double AnnualLeaveHours { get; set; }
    public double UsedSickLeaveHours { get; set; }
    public double UsedAnnualLeaveHours { get; set; }
    public Address? ResidentialAddresses { get; set; }
    public Address? PostalAddresses { get; set; }
    public List<string>? PhoneNumbers { get; set; }
    public List<string>? Emails { get; set; }
    public bool IsActive { get; set; }
    public List<string>? Skills { get; set; }
    
    //Computed property to calculate age
    public int? Age
    {
        get
        {
            var today = DateTime.Today;
            var age = today.Year - DateOfBirth.Year;
            if (DateOfBirth > today.AddYears(-age))
            {
                age--;
            }
            return age;
        }
    }
}
