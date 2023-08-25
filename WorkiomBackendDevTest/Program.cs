using MongoDB.Driver;
using WorkiomBackendDevTest.Entities;
using WorkiomBackendDevTest.Repositories.Classes;
using WorkiomBackendDevTest.Repositories.Interfaces;
using WorkiomBackendDevTest.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var mongoDbConnectionString = "mongodb+srv://jamalfx:12345@cluster0.hb9syfz.mongodb.net/?retryWrites=true&w=majority";
var mongoClient = new MongoClient(mongoDbConnectionString);
var mongoDatabase = mongoClient.GetDatabase("Workiomstore");

// Register repositories
builder.Services.AddScoped<IRepository<Company>>(sp => new CompanyRepository(mongoDatabase));
builder.Services.AddScoped<IRepository<Contact>>(sp => new ContactRepository(mongoDatabase));


builder.Services.AddSingleton<CompanyService>();
builder.Services.AddSingleton<ContactService>();


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
