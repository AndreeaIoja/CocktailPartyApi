using Business.Contracts;
using Business.Mapping;
using Business.Services;
using CocktailParty.Extensions;
using DataAccess;
using DataAccess.Repositories;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString"));
}
);
builder.Services.AddMeilisearch(builder.Configuration);
builder.Services.AddAutoMapper(typeof(RecipeProfile));
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IMeiliSearchService, MeiliSearchService>();
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["Redis:ConnectionString"];
    options.InstanceName = "Cocktail:";
});
builder.Services.AddAutoMapper(typeof(RecipeProfile));
builder.AddCustomLogging();

var app = builder.Build();
app.UseGlobalExceptionHandling();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Theme = ScalarTheme.DeepSpace; 
    });

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
