namespace Domain.Entities;
public class Product
{
    public int ProductId { get; set; }
    public Tech Tech;
    public string ProductName {get; set;}
    public decimal Price {get; set;}
    public List<Credit> Credits {get; set;}
}
public enum Tech   
{
    Phone,
    Computer,
    TV  
}