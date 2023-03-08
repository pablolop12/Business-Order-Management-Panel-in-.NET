using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CsPharma_V4.Areas.Identity.Data;
using CsPharma_V4.Core.Repositories;
using Pages.GestionUsuario.Repositories;

var builder = WebApplication.CreateBuilder(args); // se crea el objeto "builder" para configurar los servicios de la aplicación

AddScope(); // se llama a la función "AddScope()" que configura la inyección de dependencia para las clases relacionadas con el CRUD de usuarios

// Add services to the container.
builder.Services.AddRazorPages(); // se agregan las páginas Razor
builder.Services.AddControllersWithViews(); // se agregan los controladores con vistas
builder.Services.AddEntityFrameworkNpgsql() // se agregan los servicios del framework Entity Framework Core para PostgreSQL
    .AddDbContext<CsPharmaSprintFinalContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("EFCConexion")); // se configura la cadena de conexión a la base de datos PostgreSQL
    });

builder.Services.AddEntityFrameworkNpgsql() // se agregan los servicios del framework Entity Framework Core para PostgreSQL
    .AddDbContext<LoginContexto>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("EFCConexion")); // se configura la cadena de conexión a la base de datos PostgreSQL
    });

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false) // se agregan los servicios de autenticación y se configura la opción para requerir una cuenta confirmada
  .AddRoles<IdentityRole>() // se agregan los roles de usuario
  .AddEntityFrameworkStores<LoginContexto>(); // se agregan los servicios del framework Entity Framework Core para PostgreSQL

var app = builder.Build(); // se construye la aplicación a partir del objeto "builder"

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); // se configura la aplicación para habilitar el comportamiento de marcas de tiempo heredado

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) // se verifica si la aplicación está en modo de desarrollo
{
    app.UseExceptionHandler("/Error"); // se configura el manejo de excepciones
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts(); // se habilita la política de seguridad HSTS
}

app.UseHttpsRedirection(); // se redirige el tráfico HTTP a HTTPS
app.UseStaticFiles(); // se habilitan los archivos estáticos

app.UseRouting(); // se habilita el enrutamiento
app.UseAuthentication(); ; // se habilita la autenticación
app.UseAuthorization(); // se habilita la autorización

app.MapRazorPages(); // se mapean las páginas Razor

app.Run(); // se inicia la aplicación


void AddScope()//Inyeccion de dependencias declarada arriba
{
    builder.Services.AddScoped<IUserRepository, UserRepository>(); // Se agrega el servicio del repositorio de usuarios
    builder.Services.AddScoped<IIdentityRoleRepository, RoleRepository>(); // Se agrega el servicio del repositorio de roles
    builder.Services.AddScoped<IUnionRepository, Union>();//Servicio de repositorio Union
}