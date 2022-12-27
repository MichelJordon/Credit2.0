namespace Domain.Entities;
public class AddCreditDto
{
    public int CreditId { get; set; }
    public decimal DeptPerMonth {get; set;}
    public decimal Dept {get; set;}
    public Month Month {get; set;}
    public DateTime dateTime;
}