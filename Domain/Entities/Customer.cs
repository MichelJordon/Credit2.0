namespace Domain.Entities;
public class Customer
{
    public int CustomerId { get; set; }
    public string Name {get; set;}
    public string Surname {get; set;}
    public string PhoneNumber {get; set;}
    public decimal Balance {get; set;}
    public List<Credit> Credits {get; set;}
    public Customer()
    {
        Balance = 0;
    }
}