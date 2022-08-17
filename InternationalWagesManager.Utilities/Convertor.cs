using ApiContracts;
using AutoMapper;

namespace InternationalWagesManager.Utilities
{
    public class Convertor : Profile
    {
        public Convertor()
        {
            CreateMap<DTO.Employee, Models.Employee>().ReverseMap();
            CreateMap<DTO.Salary, Models.Salary>().ReverseMap();
            CreateMap<DTO.SalaryComponents, Models.SalaryComponents>().ReverseMap();
            CreateMap<DTO.WorkConditions, Models.WorkConditions>().ReverseMap();
            CreateMap<DTO.Payment, Models.Payment>().ReverseMap();
            CreateMap<DTO.Currency, Models.Currency>().ReverseMap();

           
            
        }
    }
}