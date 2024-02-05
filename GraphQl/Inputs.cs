using GraphQlDemo.Enums;
using GraphQlDemo.Models;

namespace GraphQlDemo.GraphQl;

public class Input
{
    public class EmployeeUpdate
    {
        public Department? Department { get; set; }
        public Position? Position { get; set; }
        public decimal? Salary { get; set; }
        public double? SickLeaveHours { get; set; }
        public double? AnnualLeaveHours { get; set; }
        public double? UsedSickLeaveHours { get; set; }
        public double? UsedAnnualLeaveHours { get; set; }
        public Address? ResidentialAddresses { get; set; }
        public Address? PostalAddresses { get; set; }
        public List<string>? PhoneNumbers { get; set; }
        public List<string>? Emails { get; set; }
        public bool? IsActive { get; set; }
        public List<string>? Skills { get; set; }
    }
}