using AutoMapper;
using Business.DTOs;
using Domain.Entities;

namespace Business.Mapping
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            CreateMap<Recipe, RecipeDTO>();
        }
    }
}
