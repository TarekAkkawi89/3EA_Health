using _3EA_Health.Data;
using _3EAHealth.DataAccessLayer;
using _3EAHealth.DataAccessLayer.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Datacontext>(options => options.UseSqlite("Data Source=app.db;Cache=Shared"));
builder.Services.AddScoped<INotesRepository, NotesRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // URL for swagger https://localhost:7228/swagger/index.html
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
