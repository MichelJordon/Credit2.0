using Domain.Entities;
using Domain.Dtos;
using AutoMapper;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Infrastructure.Services;


public class CustomerService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public readonly ProductService _productService;



    public CustomerService(DataContext context, IMapper mapper, ProductService productService)
    {
        _context = context;
        _mapper = mapper;
        _productService = productService;
    }
    
    public async Task<Response<List<GetCustomerDto>>> GetCustomers()
    {
        var customers = await _context.Customers.ToListAsync();
        var list = new List<GetCustomerDto>();
        
        foreach (var t in customers)
        {
            var customer = new GetCustomerDto()
            {
                CustomerId = t.CustomerId,
                Name = t.Name,
                Surname = t.Surname,
                PhoneNumber = t.PhoneNumber,
                Balance = t.Balance,
                Credits = GetCustomerCredits(t.CustomerId)
            };
            list.Add(customer);
        }
       return new Response<List<GetCustomerDto>>(list);
    }

    public async Task<Response<GetCustomerDto>> AddCustomer(AddCustomerDto customer)
    {
            var newCustomer = new Customer()
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                Surname = customer.Surname,
                PhoneNumber = customer.PhoneNumber,
            };
        _context.Customers.Add(newCustomer);
         await _context.SaveChangesAsync();
        return new Response<GetCustomerDto>(_mapper.Map<GetCustomerDto>(newCustomer));
    }
    public async Task<Response<GetCreditDto>> PayForCredits(PayForCredit credit)
    {
        var findCustomer = _context.Customers.FirstOrDefault(x=>x.PhoneNumber == credit.PhoneNumber);
        var findCredits = _context.Credits.FirstOrDefault(x=>x.CustomerId == findCustomer.CustomerId && x.CreditId == credit.CreditId);
        var findProduct = _context.Products.FirstOrDefault(x=>x.ProductId == findCredits.ProductId);

        if ( findCustomer.Balance >= credit.Price ){
            decimal deffDept = _productService.GetCustomerCredits(findProduct.Tech, findProduct.Price, findCredits.Month);
                if ( findCredits.Dept <= credit.Price ){
                    findCredits.DeptPerMonth = 0;
                    findCredits.Dept = 0;
                    findCredits.dateTime = DateTime.UtcNow;
                }
                else{
                     findCredits.Dept = Math.Max(0 , findCredits.Dept - credit.Price) ;
                    if ( findCredits.DeptPerMonth < credit.Price ){
                        if ( deffDept < credit.Price ){
                            findCredits.DeptPerMonth = deffDept - ( credit.Price % deffDept);
                            findCredits.dateTime = DateTime.UtcNow.AddMonths((int)(credit.Price / deffDept));
                        }
                        else{
                            findCredits.DeptPerMonth = (deffDept - credit.Price);
                            findCredits.dateTime = DateTime.UtcNow.AddMonths(1);
                        }
                    }
                    else{
                        findCredits.DeptPerMonth -= credit.Price;
                    }
                }

        }
        else
        {
            return new Response<GetCreditDto>(HttpStatusCode.NotFound,"Payment Eror");
        }
        findCustomer.Balance -= credit.Price;
         await _context.SaveChangesAsync();
        return new Response<GetCreditDto>(_mapper.Map<GetCreditDto>(findCredits));
    }
    public async Task<Response<GetCustomerDto>> TopUpBalance(TopUpBalance ball)
    {
        var acc = _context.Customers.FirstOrDefault(x=>x.PhoneNumber == ball.PhoneNumber);
        if(acc == null) return new Response<GetCustomerDto>(HttpStatusCode.NotFound,"Customer not found");
        acc.Balance += ball.Balance;
        await _context.SaveChangesAsync();
        var get = new GetCustomerDto(){
            Name = acc.Name,
            Surname = acc.Surname,
            PhoneNumber = acc.PhoneNumber,
            Balance = acc.Balance,
            Credits = GetCustomerCredits(acc.CustomerId)
        };
        await _context.SaveChangesAsync();
        return new Response<GetCustomerDto>(_mapper.Map<GetCustomerDto>(acc));
    }
    public async Task<Response<GetCustomerDto>> Update(AddCustomerDto customer)
    {
        
        var find = await _context.Customers.FindAsync(customer.CustomerId);
        find.Name = customer.Name;
        find.Surname = customer.Surname;
        find.PhoneNumber = customer.PhoneNumber;
        await _context.SaveChangesAsync();
        return new Response<GetCustomerDto>(_mapper.Map<GetCustomerDto>(find));
    }
    
    public async Task<Customer> Delete(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
        return customer;
    }
    public int GetCustomerCredits( int id )
    {
        int credits = 0;
        foreach (var t in _context.Credits)
        {
            if ( t.CustomerId == id )
                credits ++;
        }
        return credits;
    }
}