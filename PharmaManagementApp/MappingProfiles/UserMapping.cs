using AutoMapper;
using PharmaManagementApp.Models;

namespace PharmaManagementApp.MappingProfiles
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<UserTable, UserViewModel>();
            // To map specific entity 
            //.ForMember(dest => dest.ProductName // Destination, opt => opt.MapFrom(src => src.Name) // Mapping Value)
        }
    }
}
