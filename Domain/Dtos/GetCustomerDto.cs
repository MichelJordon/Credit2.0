namespace Domain.Dtos;
public class GetCustomerDto
{
    public int CustomerId { get; set; }
    public string Name {get; set;}
    public string Surname {get; set;}
    public string PhoneNumber {get; set;}
    public decimal Balance {get; set;}
    public int Credits {get; set;}
}