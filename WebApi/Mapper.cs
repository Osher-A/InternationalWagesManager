using ApiContracts;
using InternationalWagesManager.Models;
using AutoMapper;

namespace WebApi
{
    public class Mapper : Profile
    {
       public Mapper()
        {
            CreateMap<EmployeeRequest, Employee>().ReverseMap();
            CreateMap<EmployeeResponse, Employee>().ReverseMap();
            CreateMap<SalaryRequest, Salary>().ReverseMap();
            CreateMap<SalaryResponse, Salary>().ReverseMap();
            CreateMap<SalaryComponentsRequest, SalaryComponents>().ReverseMap();
            CreateMap<SalaryComponentsResponse, SalaryComponents>().ReverseMap();
            CreateMap<WorkConditionsRequest, WorkConditions>().ReverseMap();
            CreateMap<WorkConditionsResponse, WorkConditions>().ReverseMap();
            CreateMap<PaymentRequest, Payment>().ReverseMap();
            CreateMap<PaymentResponse, Payment>().ReverseMap();
            CreateMap<CurrencyRequest, Currency>().ReverseMap();
            CreateMap<CurrencyResponse, Currency>().ReverseMap();
        }
    }
}
