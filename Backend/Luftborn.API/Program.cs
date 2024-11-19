using FluentValidation;
using Luftborn.Repositories.ApplicationContext;
using Luftborn.Repositories.CustomerRepository;
using Luftborn.Services.CustomerService.Command;
using Luftborn.Services.CustomerService.Commands;
using Luftborn.Services.CustomerService.Validators;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DeafultConnection")));

builder.Services.AddTransient<IValidator<CreateCustomerCommend>, CreateCustomerCommandValidator>();
builder.Services.AddTransient<IValidator<UpdateCustomerCommand>, UpdateCustomerCommandValidator>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.AllowAnyOrigin()
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
app.UseCors("AllowSpecificOrigins");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
