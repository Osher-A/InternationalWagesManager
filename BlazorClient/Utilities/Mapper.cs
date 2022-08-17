using AutoMapper;

namespace BlazorClient.Utilities
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<ViewModel.Employee, InternationalWagesManager.DTO.Employee>().ReverseMap();
            CreateMap<InternationalWagesManager.DTO.Employee, InternationalWagesManager.Models.Employee>().ReverseMap();
            CreateMap<InternationalWagesManager.DTO.Salary, InternationalWagesManager.Models.Salary>().ReverseMap();
            CreateMap<InternationalWagesManager.DTO.SalaryComponents, InternationalWagesManager.Models.SalaryComponents>().ReverseMap();
            CreateMap<InternationalWagesManager.DTO.WorkConditions, InternationalWagesManager.Models.WorkConditions>().ReverseMap();
            CreateMap<InternationalWagesManager.DTO.Payment, InternationalWagesManager.Models.Payment>().ReverseMap();
            CreateMap<InternationalWagesManager.DTO.Currency, InternationalWagesManager.Models.Currency>().ReverseMap();
        }
    }
}
