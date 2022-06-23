using AutoMapper;

namespace InternationalWagesManager.Domain.Utilities
{
    public class DTOConvertor : Profile
    {
        public DTOConvertor()
        {
            CreateMap<DTO.Employee, Models.Employee>().ReverseMap();
            CreateMap<DTO.Salary, Models.Salary>().ReverseMap();
            CreateMap<DTO.SalaryComponents, Models.SalaryComponents>().ReverseMap();
            CreateMap<DTO.WorkConditions, Models.WorkConditions>().ReverseMap();
            CreateMap<DTO.Payment, Models.Payment>().ReverseMap();
        }
    }
}