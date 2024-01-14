using ClayDoorsProject.Repositories;
using ClayDoorsProject.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
AddRepositories();

AddServices();

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

void AddRepositories()
{
    builder.Services.AddDbContext<ClayDoorDatabaseContext>();
    builder.Services.AddTransient<IDoorsRepository, DoorsRepository>();
}

void AddServices()
{
    builder.Services.AddTransient<IDoorsReadService, DoorsReadService>();
}