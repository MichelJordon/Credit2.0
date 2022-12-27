using Domain.Entities;
namespace Domain.Dtos;
public class PayForCredit
{
    public string PhoneNumber {get; set;}
    public int CreditId { get; set; } 
    public decimal Price {get; set;}
}