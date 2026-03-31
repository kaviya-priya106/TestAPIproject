using AutoMapper;
using TestAPIproject.Models;
using TestAPIproject.ViewModels;

namespace TestAPIproject.profile
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeCreateViewModel, Employee>();
            CreateMap< Employee,EmployeeListViewModel>();
            CreateMap<OrdersDto, Order>();
            CreateMap<Order, OrdersDto>();
        }
    }
}
