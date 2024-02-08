using Microsoft.EntityFrameworkCore;
using PizzaApi.Repository;
using PizzaApi.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRabitMQProducer, RabitMQProducer>();
builder.Services.AddTransient<IPizzaRepository, PizzaRepository>();

builder.Services.AddDbContext<PizzaApi.Data.PizzaContext>(
  options =>
  {
      options.UseSqlServer(builder.Configuration.GetConnectionString("DBConStr"));
  });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
