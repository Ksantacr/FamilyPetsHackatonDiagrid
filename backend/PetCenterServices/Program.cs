using PetCenter.Interfaces;
using PetCenter.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Adds Amazon Cognito as Identity Provider
builder.Services.AddCognitoIdentity();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPetCenterServices, PetCenterServices>();
builder.Services.AddScoped<IPetCenterRepository, PetCenterRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// If not already enabled, you will need to enable ASP.NET Core authentication
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
