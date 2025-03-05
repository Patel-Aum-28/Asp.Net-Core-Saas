using AutoMapper;
using PharmaApi.Models;

namespace PharmaApi.MappingProfiles
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<UserTable, UserViewModel>();
        }
    }
}
