using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Infrastructure.Services;
using Domain.Dtos;
namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]

public class CreditController{
    public readonly CreditService _customerService;
    public CreditController(CreditService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet("GetAll")]
    public async Task<Response<List<GetCreditDto>>> GetCustomers(){
        return await _customerService.GetCredits();
    }
}