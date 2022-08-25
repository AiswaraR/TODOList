using DataAccessLayer;
using Microsoft.OpenApi.Models;
using ServiceLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IToDoListDL, ToDoListDL>();
builder.Services.AddSingleton<IToDoListSL, ToDoListSL>();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "ToDoListAPI", Description = "APIs for managing TODO List" });
    x.ResolveConflictingActions(description => description.First());
    x.IncludeXmlComments($"ToDoList.XML");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDoListAPI"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
