namespace DotCreative.Services.PaymentCenter.Core.Abstractions;

public class Address
{
  public string Street { get; set; }
  public int Number { get; set; }
  public string Neighborhood { get; set; }
  public string City { get; set; }
  public string State { get; set; }
  public string Zipcode { get; set; }
  public string Complement { get; set; }

  public Address(string street, int number, string neighborhood, string city, string state, string zipcode, string complement)
  {
    Street = street;
    Number = number;
    Neighborhood = neighborhood;
    City = city;
    State = state;
    Zipcode = zipcode;
    Complement = complement;
  }

  public Address(string street, int number, string neighborhood, string city, string state, string zipcode)
  {
    Street = street;
    Number = number;
    Neighborhood = neighborhood;
    City = city;
    State = state;
    Zipcode = zipcode;
  }
}
