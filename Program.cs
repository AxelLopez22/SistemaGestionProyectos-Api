using Api_ProjectManagement.Common.Exceptions;
using Api_ProjectManagement.Common.Util;
using Api_ProjectManagement.Configurations;
using Api_ProjectManagement.Database;
using Api_ProjectManagement.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.SignalR;
using Api_ProjectManagement.Services.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

builder.Services.AddControllers()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSignalR();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ITokenServices, TokenServices>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IFilesServices, FilesServices>();
builder.Services.AddScoped<IProyectoServices, ProyectoServices>();
builder.Services.AddScoped<IProyectoUsuariosServices, ProyectoUsuariosServices>();
builder.Services.AddScoped<ITareasServices, TareasServices>();
builder.Services.AddScoped<IComentariosServices, ComentariosServices>();
builder.Services.AddScoped<IEstadoServices, EstadoServices>();
builder.Services.AddScoped<IPrioridadServices, PrioridadServices>();
builder.Services.AddTransient<HubCommentNotify>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["LlaveJwt"])),
        ClockSkew = TimeSpan.Zero
    });


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

app.MapHub<HubGroup>("hub/group");

app.MapHub<HubCommentNotify>("hub/comment");

app.Run();
