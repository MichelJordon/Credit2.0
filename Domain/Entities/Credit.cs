namespace Domain.Entities;
public class Credit
{
    public int CreditId { get; set; }
    public decimal DeptPerMonth {get; set;}
    public decimal Dept {get; set;}
    public Month Month {get; set;}
    public DateTime dateTime{get; set;}
    public Credit ()
    {
        dateTime = DateTime.UtcNow;
    }
    public int CustomerId {get; set;}
    public virtual Customer Customer { get; set; }
    public int ProductId {get; set;}
    public virtual Product Product { get; set; }
}
// 3 6 9 12 18 24
public enum Month   
{
    m3 = 3,
    m6 = 6,
    m9 = 9,
    m12 = 12,
    m18 = 18,
    m24 = 24   
}