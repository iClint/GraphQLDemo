using GraphQlDemo.EmployeeHelpers;
using GraphQlDemo.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GraphQlDemo.Models;

public class Employee
{
    public ObjectId _id { get; set; }
    public string EmployeeNumber { get; set; }
    public string TaxFileNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    
    public string DateOfBirthFormatted => DateOfBirth.ToString("dd/MM/yyyy");

    [BsonElement("DateOfBirth")]
    private DateTime DateOfBirth { get; set; }

    public Department Department { get; set; }
    public Position Position { get; set; }
    public double Salary { get; set; }
    
    public string HireDateFormatted => HireDate.ToString("dd/MM/yyyy");
    
    [BsonElement("HireDate")]
    private DateTime HireDate { get; set; }
    public double SickLeaveHours { get; set; }
    public double AnnualLeaveHours { get; set; }
    public double UsedSickLeaveHours { get; set; }
    public double UsedAnnualLeaveHours { get; set; }
    public Address? ResidentialAddress { get; set; }
    public Address? PostalAddress { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
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
    
    public Employee()
    {
        _id = ObjectId.GenerateNewId();
        EmployeeNumber = string.Empty;
        TaxFileNumber = string.Empty;
        FirstName = string.Empty;
        LastName = string.Empty;
        Gender = Gender.NotSpecified;
        DateOfBirth = DateTime.MinValue;
        Department = Department.Null;
        Position = Position.Null;
        Salary = 0;
        HireDate = DateTime.MinValue;
        SickLeaveHours = 0.0;
        AnnualLeaveHours = 0.0;
        UsedSickLeaveHours = 0.0;
        UsedAnnualLeaveHours = 0.0;
        ResidentialAddress = new Address(string.Empty, string.Empty, string.Empty, State.Null, string.Empty);
        PostalAddress = new Address(string.Empty, string.Empty, string.Empty, State.Null, string.Empty);
        Phone = string.Empty;
        Email = string.Empty;
        IsActive = false;
        Skills = new List<string>();
    }
    
    // Constructor for essential values, setting defaults for the rest
    public Employee(
        string employeeNumber, 
        string taxFileNumber, 
        string firstName, 
        string lastName, 
        Gender gender, 
        DateTime dateOfBirth,
        DateTime hireDate)
        : this()
    {
        EmployeeNumber = employeeNumber;
        TaxFileNumber = taxFileNumber;
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        DateOfBirth = dateOfBirth;
        HireDate = hireDate;
    }
    
    private string GetFormattedDate(DateTime date)
    {
        return date.ToString("dd/MM/yyyy");
    }
}
