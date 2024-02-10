using GraphQlDemo.Enums;
using GraphQlDemo.Models;

namespace GraphQlDemo.GraphQl;

public class Input
{
    public class EmployeeUpdate
    {
        public Gender? Gender { get; set; }
        public Department? Department { get; set; }
        public Position? Position { get; set; }
        public double? Salary { get; set; }
        public double? SickLeaveHours { get; set; }
        public double? AnnualLeaveHours { get; set; }
        public double? UsedSickLeaveHours { get; set; }
        public double? UsedAnnualLeaveHours { get; set; }
        public Address? ResidentialAddress { get; set; }
        public Address? PostalAddress { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public bool? IsActive { get; set; }
        public List<string>? Skills { get; set; }
    }
}