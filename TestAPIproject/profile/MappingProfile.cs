using AutoMapper;
using TestAPIproject.Models;
using TestAPIproject.ViewModels;
using TestAPIproject.Dto;

namespace TestAPIproject.profile
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeCreateDto, Employee>();
            CreateMap< Employee,EmployeeDto>();
            CreateMap<AddOrdersDto, Order>();
            CreateMap<Order, AddOrdersDto>();
        }
    }
}
