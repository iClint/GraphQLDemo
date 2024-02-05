namespace GraphQlDemo.Models;

public class Address
{
    public Address(string line1, string line2, string city, string state, string postcode)
    {
        Line1 = line1;
        Line2 = line2;
        City = city;
        State = state;
        Postcode = postcode;
    }

    public string? Line1 { get; set; }
    public string? Line2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Postcode { get; set; }
}