using Domain.Entities;
using Domain.Dtos;
using AutoMapper;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Infrastructure.Services;


public class CreditService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;


    public CreditService(DataContext context)
    {
        _context = context;
    }
    
    public async Task<Response<List<GetCreditDto>>> GetCredits()
    {
        var credits = await _context.Credits.ToListAsync();
        var list = new List<GetCreditDto>();
        
        foreach (var t in credits)
        {
            var findCustomer = _context.Customers.FirstOrDefault(x=>x.CustomerId == t.CustomerId);
            var findProduct = _context.Products.FirstOrDefault(x=>x.ProductId == t.ProductId);
            var customer = new GetCreditDto()
            {
                CreditId = t.CreditId,
                Name = findCustomer.Name,
                Surname = findCustomer.Surname,
                DeptPerMonth = t.DeptPerMonth,
                Dept = t.Dept,
                Month = t.Month,
                dateTime = t.dateTime,
                ProductName = findProduct.ProductName
            };
            list.Add(customer);
        }
       return new Response<List<GetCreditDto>>(list);
    }
}