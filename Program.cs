using MyExam.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var Configuration = builder.Configuration;

builder.Services.AddControllers()
    .AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MyDbContext>(options => options.UseSqlServer(
    Configuration["ConnectionStrings:SoKingDb"]));
builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
