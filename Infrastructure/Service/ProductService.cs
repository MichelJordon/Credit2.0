using Domain.Entities;
using Domain.Dtos;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Infrastructure.Services;

public class ProductService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public ProductService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;

    }
    
    public async Task<Response<List<GetProductDto>>> GetProducts( Month month )
    {
        var products = await _context.Products.ToListAsync();
        var list = new List<GetProductDto>();
        foreach (var t in products)
        {
            var todo = new GetProductDto()
            {
                ProductId = t.ProductId,
                Tech = t.Tech,
                ProductName = t.ProductName,
                Month = month,
                Price = t.Price,
                PricePerMouth = GetCustomerCredits( t.Tech, t.Price, month )
            };
            list.Add(todo);
        }
       return new Response<List<GetProductDto>>(list);
    }

    public async Task<Response<AddProductDto>> AddProduct(AddProductDto product)
    {
            try
            {
                var newTodo = new Product()
                {
                    ProductId = product.ProductId,
                    Tech = product.Tech,
                    ProductName = product.ProductName,
                    Price = product.Price,
                
                };  
            _context.Products.Add(newTodo);
             await _context.SaveChangesAsync();
            return new Response<AddProductDto>(product);
            }
            catch (System.Exception ex)
            {
                
                return new Response<AddProductDto>(System.Net.HttpStatusCode.InternalServerError,ex.Message);
            }
    }
    public async Task<Response<AddCreditDto>> BuyProduct(BuyProductDto product)
    {
                var findProduct = await _context.Products.FindAsync(product.ProductId);
                var acc = _context.Customers.FirstOrDefault(x=>x.PhoneNumber == product.PhoneNumber);
                var newTodo = new Credit()
                {
                    CreditId = product.CreditId,
                    DeptPerMonth = GetCustomerCredits(findProduct.Tech, findProduct.Price, product.Month ),
                    Dept = GetDept(findProduct.Tech, findProduct.Price, product.Month ),
                    Month = product.Month,
                    CustomerId = acc.CustomerId,
                    ProductId = findProduct.ProductId,
                };  
            _context.Credits.Add(newTodo);
             await _context.SaveChangesAsync();
            return new Response<AddCreditDto>(_mapper.Map<AddCreditDto>(newTodo));
    }
    public async Task<Response<AddProductDto>> Update(AddProductDto todo)
    {
        
        var find = await _context.Products.FindAsync(todo.ProductId);
        find.Tech = todo.Tech;
        find.ProductName =  todo.ProductName;
        find.Price = todo.Price;
        await _context.SaveChangesAsync();
        return new Response<AddProductDto>(todo);
    }
    
    public async Task<Product> Delete(int id)
    {
        var todo = await _context.Products.FindAsync(id);
        _context.Products.Remove(todo);
        await _context.SaveChangesAsync();
        return todo;
    }
    public decimal GetCustomerCredits(  Tech tech, decimal price, Month month )
    {
        if ( Month.m9 >= month && Tech.Phone == tech ){
            return price / ((decimal)month);
        }
        if ( Month.m12 <= month && Month.m18 >= month && Tech.Phone == tech ){
            return (price + (price * 3 / 100)) / ((decimal)month);
        }
        if ( Month.m24 >= month && Tech.Phone == tech ){
            return (price + (price * 6 / 100)) / ((decimal)month);
        }
        //------
        if ( Month.m12 >= month && Tech.Computer == tech ){
            return price / ((decimal)month);
        }
        if ( Month.m18 >= month && Tech.Computer == tech ){
            return (price + (price * 4 / 100)) / ((decimal)month);
        }
        if ( Month.m24 >= month && Tech.Computer == tech ){
            return (price + (price * 8 / 100)) / ((decimal)month);
        }
        //-------
         if ( Month.m18 >= month && Tech.TV == tech ){
            return price / ((decimal)month);
        }
        if ( Month.m24 >= month && Tech.TV == tech ){
            return (price + (price * 5 / 100)) / ((decimal)month);
        }
        return price;
    }
    public decimal GetDept(  Tech tech, decimal price, Month month )
    {
        if ( Month.m9 >= month && Tech.Phone == tech ){
            return price ;
        }
        if ( Month.m12 <= month && Month.m18 >= month && Tech.Phone == tech ){
            return (price + (price * 3 / 100));
        }
        if ( Month.m24 >= month && Tech.Phone == tech ){
            return (price + (price * 6 / 100)) ;
        }
        //------
        if ( Month.m12 >= month && Tech.Computer == tech ){
            return price;
        }
        if ( Month.m18 >= month && Tech.Computer == tech ){
            return (price + (price * 4 / 100)) ;
        }
        if ( Month.m24 >= month && Tech.Computer == tech ){
            return (price + (price * 8 / 100));
        }
        //-------
         if ( Month.m18 >= month && Tech.TV == tech ){
            return price / ((decimal)month);
        }
        if ( Month.m24 >= month && Tech.TV == tech ){
            return (price + (price * 5 / 100));
        }
        return price;
    }
}
