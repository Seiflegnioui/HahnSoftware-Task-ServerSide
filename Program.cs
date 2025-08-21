using hahn.Application.Users.Commands;
using hahn.Application.Validators;
using hahn.Domain.Repositories;
using hahn.Infrastructure.Presistence;
using hahn.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using System.Reflection;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var conn = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(conn, ServerVersion.AutoDetect(conn));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepository,UserRepository>();

builder.Services.AddMediatR(typeof(CreateUserHandler).Assembly);
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {

        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["jwt_infos:issuer"],

        ValidateAudience = true,
        ValidAudience = builder.Configuration["jwt_infos:audience"],

        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["jwt_infos:key"]!)
        )
    };

    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine("AUTH FAILED: " + context.Exception.Message);
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            Console.WriteLine("TOKEN VALIDATED");
            return Task.CompletedTask;
        },
    };
});
builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSomeOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
          .AllowAnyHeader()
          .AllowAnyMethod()
          .AllowCredentials();

    });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseCors("AllowSomeOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();
app.UseHttpsRedirection();

app.Run();
// app.UseExceptionHandler(errorApp =>
// {
//     errorApp.Run(async context =>
//     {
//         context.Response.StatusCode = StatusCodes.Status400BadRequest;
//         context.Response.ContentType = "application/json";

//         var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
//         if (exceptionHandlerPathFeature?.Error is ValidationException validationException)
//         {
//             var errors = validationException.Errors
//                 .GroupBy(e => e.PropertyName)
//                 .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

//             await context.Response.WriteAsJsonAsync(new { Errors = errors });
//         }
//     });
// });



