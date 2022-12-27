using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Infrastructure.Services;
using Domain.Dtos;
namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]

public class CustomerController{
    public readonly CustomerService _customerService;
    public CustomerController(CustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet("GetAll")]
    public async Task<Response<List<GetCustomerDto>>> GetCustomers(){
        return await _customerService.GetCustomers();
    }
    [HttpPost("Add")]
    public async Task<Response<GetCustomerDto>> InsertCustomer(AddCustomerDto customer){
        return await _customerService.AddCustomer(customer);
    }
    [HttpPost("PayForCredits")]
    public async Task<Response<GetCreditDto>> PayForCreditss(PayForCredit credit){
        return await _customerService.PayForCredits(credit);
    }
    [HttpPost("TopUp")]
    public async Task<Response<GetCustomerDto>> TopUpBalance(TopUpBalance ball){
        return await _customerService.TopUpBalance(ball);
    }
    [HttpPut("Update")]
    public async Task<Response<GetCustomerDto>> Update(AddCustomerDto customer){
        return await _customerService.Update(customer);
    }
}