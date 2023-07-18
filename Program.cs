using Api_ProjectManagement.Common.Exceptions;
using Api_ProjectManagement.Common.Util;
using Api_ProjectManagement.Configurations;
using Api_ProjectManagement.Database;
using Api_ProjectManagement.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
string TitleSwagger = "API PROJECT MANAGEMENT";
string MyAllowOrigins = "MyAllowOrigins";

// Add services to the container.

builder.Services.AddDbContext<ProjectManagementDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionDb"));
});

//Configuracion de servicio automapper
var mapperConfigure = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MapperConfig());
});
IMapper mapper = mapperConfigure.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ITokenServices, TokenServices>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IFilesServices, FilesServices>();
builder.Services.AddScoped<IProyectoServices, ProyectoServices>();
builder.Services.AddScoped<IProyectoUsuariosServices, ProyectoUsuariosServices>();
builder.Services.AddScoped<ITareasServices, TareasServices>();
builder.Services.AddScoped<IComentariosServices, ComentariosServices>();

builder.Services.AddSwaggerConfig(TitleSwagger);
builder.Services.AddConfigureCORS(MyAllowOrigins);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors(MyAllowOrigins);

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
