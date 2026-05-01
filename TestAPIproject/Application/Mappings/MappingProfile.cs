using AutoMapper;
using TestAPIproject.Application.Dto;
using TestAPIproject.Domain;

namespace TestAPIproject.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeCreateDto, Employee>();
            CreateMap<Employee, EmployeeDto>();
            CreateMap<AddOrdersDto, Order>();
            CreateMap<Order, AddOrdersDto>();
        }
    }
}
