using BenchTask.API.Models;
using BenchTask.API.Repository;
using BenchTask.API.Services;
using EudHub.API.Repositories;
using EudHub.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(op =>
{
    op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ClockSkew = TimeSpan.Zero,
            ValidateIssuerSigningKey = true

        };
    });
builder.Services.AddDbContext<EduHubInfoContext>(option => option.UseSqlServer("name=ConnectionStrings:MyDBConnection"));

builder.Services.AddScoped<IAuthenticationService, AuthenticationRepository>();
builder.Services.AddScoped<IUserService, UserRepository>();
builder.Services.AddScoped<ICourseService, CourseRepository>();
builder.Services.AddScoped<IFeedBackService, FeedbackRepository>();
builder.Services.AddScoped<IEnquriyService,EnquiryRepository>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
           {
               options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
               options.JsonSerializerOptions.IgnoreReadOnlyProperties = true; // Example: Ignore read-only properties
              // options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
           });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
