using Domain.Entities;
namespace Domain.Dtos;
public class BuyProductDto
{
    public int CreditId { get; set; }
    public int ProductId { get; set; }
    public string PhoneNumber {get; set;}
    public Month Month {get; set;}
}