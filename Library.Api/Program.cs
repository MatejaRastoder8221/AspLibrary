using Library.Api;
using Library.Api.Core;
using Library.API.Core;
using Library.Application;
using Library.Application.UseCases.Commands.Categories;
using Library.Application.UseCases.Commands.Publishers;
using Library.DataAccess;
using Library.Implementation;
using Library.Implementation.UseCases.Commands.Categories;
using Library.Implementation.UseCases.Commands.Publishers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data;
using System.Text;
using FluentValidation;
using Library.API.Validation;
using Library.Application.DTO;
using Library.Application.UseCases.Commands.Authors;
using Library.Implementation.UseCases.Commands.Authors;
using Library.Implementation.Validators;

var builder = WebApplication.CreateBuilder(args);

// Load configuration from appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var settings = new AppSettings();
builder.Configuration.GetSection("AppSettings").Bind(settings); // Bind the AppSettings section to the settings object
settings.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection"); // Get the DefaultConnection string

builder.Services.AddSingleton(settings); // Add AppSettings to services

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<AspContext>(x => new AspContext(settings.ConnectionString));
builder.Services.AddScoped<IDbConnection>(x => new SqlConnection(settings.ConnectionString));

builder.Services.AddTransient<JwtTokenCreator>(services =>
{
    var context = services.GetRequiredService<AspContext>();
    var storage = services.GetRequiredService<ITokenStorage>();
    var issuer = settings.Jwt.Issuer;
    var secretKey = settings.Jwt.SecretKey;
    var seconds = settings.Jwt.Seconds;
    return new JwtTokenCreator(context, storage, issuer, secretKey, seconds);
});

builder.Services.AddUseCases();

builder.Services.AddTransient<IExceptionLogger, DbExceptionLogger>();
builder.Services.AddTransient<ITokenStorage, InMemoryTokenStorage>();

// Register IApplicationActor within the HTTP request scope
builder.Services.AddScoped<IApplicationActor>(provider =>
{
    var accessor = provider.GetRequiredService<IHttpContextAccessor>();
    if (accessor.HttpContext == null)
    {
        return new UnauthorizedActor();
    }

    // Retrieve actor from JWT token
    return provider.GetRequiredService<IApplicationActorProvider>().GetActor();
});

builder.Services.AddTransient<IApplicationActorProvider>(x =>
{
    var accessor = x.GetRequiredService<IHttpContextAccessor>();
    var request = accessor.HttpContext.Request;
    var authHeader = request.Headers["Authorization"].FirstOrDefault();

    return new JwtApplicationActorProvider(authHeader);
});

// Add JWT authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = settings.Jwt.Issuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Jwt.SecretKey))
    };
});

// Add authorization policy
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireLoggedIn", policy =>
    {
        policy.RequireAuthenticatedUser();
    });
});

// Add validators for DTOs
builder.Services.AddTransient<IValidator<CreateCategoryDto>, CreateCategoryDtoValidator>();
builder.Services.AddTransient<IValidator<UpdateCategoryDto>, UpdateCategoryDtoValidator>();
builder.Services.AddTransient<IValidator<CreatePublisherDto>, CreatePublisherDtoValidator>();
builder.Services.AddTransient<IValidator<UpdatePublisherDto>, UpdatePublisherDtoValidator>();

//Registering Author commands and validators

builder.Services.AddTransient<ICreateAuthorCommand, EfCreateAuthorCommand>();
builder.Services.AddTransient<IUpdateAuthorCommand, EfUpdateAuthorCommand>();
builder.Services.AddTransient<IDeleteAuthorCommand, EfDeleteAuthorCommand>();
builder.Services.AddTransient<IValidator<CreateAuthorDto>, CreateAuthorDtoValidator>();
builder.Services.AddTransient<IValidator<UpdateAuthorDto>, UpdateAuthorDtoValidator>();


// Register the CreateCategoryCommand
builder.Services.AddTransient<ICreateCategoryCommand, EfCreateCategoryCommand>();
// Register the UpdateCategoryCommand
builder.Services.AddTransient<IUpdateCategoryCommand, EfUpdateCategoryCommand>();
// Register the DeleteCategoryCommand
builder.Services.AddTransient<IDeleteCategoryCommand, EfDeleteCategoryCommand>();

// Register the CreatePublisherCommand
builder.Services.AddTransient<ICreatePublisherCommand, EfCreatePublisherCommand>();
// Register the UpdatePublisherCommand
builder.Services.AddTransient<IUpdatePublisherCommand, EfUpdatePublisherCommand>();
// Register the DeletePublisherCommand
builder.Services.AddTransient<IDeletePublisherCommand, EfDeletePublisherCommand>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

// Use authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
