using DotCreative.Services.PaymentCenter.Core.Enums;

namespace DotCreative.Services.PaymentCenter.Core.Abstractions;

public class Person
{
  public string Name { get; set; }
  public string Email { get; set; }
  public Document Document { get; set; }
  public Telephone Telephone { get; set; }
  public Address Address { get; set; }

  public Person(string name, string email, Document document, Telephone telephone, Address address)
  {
    Name = name;
    Email = email;
    Document = document;
    Telephone = telephone;
    Address = address;
  }
}
