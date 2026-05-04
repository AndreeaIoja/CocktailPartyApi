using AutoMapper;
using Business.DTOs;
using Domain.Entities;

namespace Business.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<User, UserResponseDTO>();
        }
    }
}
