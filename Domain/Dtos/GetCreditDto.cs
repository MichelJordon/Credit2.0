namespace Domain.Entities;
public class GetCreditDto
{
    public int CreditId { get; set; }
    public string? Name {get; set;}
    public string? Surname {get; set;}
    public decimal DeptPerMonth {get; set;}
    public decimal Dept {get; set;}
    public Month Month {get; set;}
    public DateTime dateTime{get; set;}
    public string? ProductName {get; set;}  
}