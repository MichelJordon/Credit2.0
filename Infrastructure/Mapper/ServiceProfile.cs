using AutoMapper;
using Domain.Dtos;
using Domain.Entities;

namespace Infrastucture.Meppers;
public class ServicesProfile:Profile
{
    public ServicesProfile()
    {
        CreateMap<Customer,GetCustomerDto>().ReverseMap();
        CreateMap<Customer, AddCustomerDto>().ReverseMap();
        CreateMap<GetCustomerDto, AddCustomerDto>().ReverseMap();
        
        CreateMap<Credit,GetCreditDto>().ReverseMap();
        CreateMap<Credit,AddCreditDto>().ReverseMap();
        CreateMap<AddCreditDto, GetCreditDto>().ReverseMap();

        CreateMap<Product,GetProductDto>().ReverseMap();
        CreateMap<Product, AddProductDto>().ReverseMap();
        CreateMap<GetProductDto, AddProductDto>().ReverseMap();

    }
}