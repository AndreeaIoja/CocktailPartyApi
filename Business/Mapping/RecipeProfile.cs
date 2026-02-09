using AutoMapper;
using Business.DTOs;
using Business.Helpers;
using Domain.Entities;

namespace Business.Mapping
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            CreateMap<Recipe, RecipeDTO>();

            CreateMap<Recipe, RecipeDetailsDTO>()
                .ForMember(
                    dest => dest.RecipeIngredients,
                    opt => opt.MapFrom(src => src.RecipesIngredients)
                )
                .ForMember(
                    dest => dest.RecipeSteps,
                    opt => opt.MapFrom(src => src.RecipeSteps)
                );

            CreateMap<RecipesIngredients, RecipeIngredientDTO>()
               .ForMember(
                   dest => dest.Name,
                   opt => opt.MapFrom(src => src.Ingredient.Name)
               )
               .ForMember(
                   dest => dest.ImagePath,
                   opt => opt.MapFrom(src => src.Ingredient.ImagePath)
               )
               .ForMember(
                   dest => dest.Alternative,
                   opt => opt.MapFrom(src => src.Ingredient.Alternative)
               )
               .ForMember(
                   dest => dest.CategoryId,
                   opt => opt.MapFrom(src => src.Ingredient.CategoryId)
               )
               .ForMember(
                   dest => dest.UnitName,
                   opt => opt.MapFrom(src => src.Unit.Name)
               )
               .ForMember(
                   dest => dest.UnitAbbr,
                   opt => opt.MapFrom(src => src.Unit.Abbr)
               );

           CreateMap<RecipeSteps, RecipeStepDTO>();

           CreateMap<PagedList<Recipe>, RecipesPageDTO>()
            .ForMember(dest => dest.Recipes, opt => opt.MapFrom(src => src.Items))
            .ForMember(dest => dest.IsLastPage, opt => opt.MapFrom(src => src.IsLastPage));

        }
    }
}
