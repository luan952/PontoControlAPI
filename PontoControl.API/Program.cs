using PontoControl.API.Filters;
using PontoControl.Application;
using PontoControl.Application.Services.AutoMapper;
using PontoControl.Infra;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRouting(opt => opt.LowercaseUrls = true);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();


builder.Services.AddRepository(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddMvc(option => option.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddScoped(provider => new AutoMapper.MapperConfiguration(config =>
{
    config.AddProfile(new AutoMapperConfig());
}).CreateMapper());

builder.Services.AddScoped<UserAuthenticatedAttribute>();

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
