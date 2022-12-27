using Domain.Entities;
namespace Domain.Dtos;
public class GetProductDto
{
    public int ProductId { get; set; }
    public Tech Tech;
    public string ProductName {get; set;}
    public Month Month {get; set;}
    public decimal Price {get; set;}
    public decimal PricePerMouth {get; set;}
}