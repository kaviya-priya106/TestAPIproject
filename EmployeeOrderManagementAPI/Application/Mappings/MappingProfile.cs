using AutoMapper;
using EmployeeOrderManagementAPI.Application.Dto;
using EmployeeOrderManagementAPI.Domain;

namespace EmployeeOrderManagementAPI.Application.Mappings
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
