using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Infrastructure.Services;
using Domain.Dtos;
namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]

public class ProductController{
    public readonly ProductService _productService;
    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("GetAll")]
    public async Task<Response<List<GetProductDto>>> GetProducts( Month month){
        return await _productService.GetProducts(month);
    }
    [HttpPost("Add")]
    public async Task<Response<AddProductDto>> InsertProduct(AddProductDto product){
        return await _productService.AddProduct(product);
    }
    [HttpPost("Buy")]
    public async Task<Response<AddCreditDto>> BuyProduct(BuyProductDto product){
        return await _productService.BuyProduct(product);
    }
    [HttpPut("Update")]
    public async Task<Response<AddProductDto>> Update(AddProductDto product){
        return await _productService.Update(product);
    }
    [HttpDelete("Dalate")]
    public async Task<Product> Update(int id){
        return await _productService.Delete(id);
    }
}