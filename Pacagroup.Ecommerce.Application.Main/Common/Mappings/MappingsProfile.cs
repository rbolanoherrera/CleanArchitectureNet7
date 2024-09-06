
using AutoMapper;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Domain.Entities;

namespace Pacagroup.Ecommerce.Application.UseCases.Common.Mappings
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Categories, CategoriesDTO>().ReverseMap();
            CreateMap<Discount, DiscountDTO>().ReverseMap();
        }
    }
}