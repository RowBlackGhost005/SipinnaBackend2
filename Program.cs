using SipinnaBackend2.Models;
using SipinnaBackend2.Services;
using Microsoft.EntityFrameworkCore;

var allowLocalHost = "localhostOrigin";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowLocalHost,
    policy =>
    {
        policy.WithOrigins("http://localhost")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
    });
});

// Add services to the container.
string? cadena = builder.Configuration.GetConnectionString("DefaultConnection") ?? "otracadena";
builder.Services.AddControllers();
//conexion a base de datos
builder.Services.AddDbContext<Conexiones>(opt =>
    opt.UseMySQL(cadena));

//Transients
builder.Services.AddTransient<DominioDAO>();
builder.Services.AddTransient<NoticiasDAO>();
builder.Services.AddTransient<IndicadorDAO>();
builder.Services.AddTransient<EnlacesDAO>();
builder.Services.AddTransient<RubrosDAO>(); 
builder.Services.AddTransient<LoggerBD>();    


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(allowLocalHost);
//create database if not exists
using (var serviceScope = app.Services.CreateScope())
{
    var serviceProvider = serviceScope.ServiceProvider;
    var dbContext = serviceProvider.GetRequiredService<Conexiones>();
    dbContext.Database.EnsureCreated();
}


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

