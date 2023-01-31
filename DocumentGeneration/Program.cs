using Application;
using Domain.IRepository;
using Domain.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Persistance;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<DynamicDocumentDBSettings>(builder.Configuration.GetSection(nameof(DynamicDocumentDBSettings)));
builder.Services.AddSingleton<IDynamicDocumentDBSettings>(sp => sp.GetRequiredService<IOptions<DynamicDocumentDBSettings>>().Value);
builder.Services.AddSingleton<IMongoClient>(s =>
    new MongoClient(builder.Configuration.GetValue<string>("DynamicDocumentDBSettings:DynamicDocumentConnectionString")));
// Add services to the container.
builder.Services.ConfigApplicationService();
builder.Services.ConfigPersistanceServices();
builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
