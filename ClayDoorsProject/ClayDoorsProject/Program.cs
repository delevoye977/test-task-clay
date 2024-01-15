using ClayDoorsDatabase.Repositories;
using ClayDoorsMain.Swagger;
using ClayDoorsModel.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidAudience = builder.Configuration.GetValue<string>("JwtSettings:Audience"),
            ValidIssuer = builder.Configuration.GetValue<string>("JwtSettings:Issuer"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JwtSettings:IssuerKey")!)),
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
        };
    });

AddRepositories();

AddServices();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

void AddRepositories()
{
    builder.Services.AddDbContext<ClayDoorDatabaseContext>();
    builder.Services.AddTransient<IDoorsRepository, DoorsRepository>();
    builder.Services.AddTransient<IDoorUsersRepository, DoorUsersRepository>();
}

void AddServices()
{
    builder.Services.AddTransient<IDoorsReadService, DoorsReadService>();
    builder.Services.AddTransient<IDoorsWriteService, DoorsWriteService>();
    builder.Services.AddTransient<IDoorUserReadService, DoorUserReadService>();
}