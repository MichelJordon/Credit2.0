using Domain.Entities;
using Domain.Dtos;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class FunctionService
{
    private readonly DataContext _context;

    public FunctionService(DataContext context)
    {
        _context = context;
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