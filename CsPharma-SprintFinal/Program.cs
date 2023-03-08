using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CsPharma_V4.Areas.Identity.Data;
using CsPharma_V4.Core.Repositories;
using Pages.GestionUsuario.Repositories;

var builder = WebApplication.CreateBuilder(args); // se crea el objeto "builder" para configurar los servicios de la aplicaci�n

AddScope(); // se llama a la funci�n "AddScope()" que configura la inyecci�n de dependencia para las clases relacionadas con el CRUD de usuarios

// Add services to the container.
builder.Services.AddRazorPages(); // se agregan las p�ginas Razor
builder.Services.AddControllersWithViews(); // se agregan los controladores con vistas
builder.Services.AddEntityFrameworkNpgsql() // se agregan los servicios del framework Entity Framework Core para PostgreSQL
    .AddDbContext<CsPharmaSprintFinalContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("EFCConexion")); // se configura la cadena de conexi�n a la base de datos PostgreSQL
    });

builder.Services.AddEntityFrameworkNpgsql() // se agregan los servicios del framework Entity Framework Core para PostgreSQL
    .AddDbContext<LoginContexto>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("EFCConexion")); // se configura la cadena de conexi�n a la base de datos PostgreSQL
    });

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false) // se agregan los servicios de autenticaci�n y se configura la opci�n para requerir una cuenta confirmada
  .AddRoles<IdentityRole>() // se agregan los roles de usuario
  .AddEntityFrameworkStores<LoginContexto>(); // se agregan los servicios del framework Entity Framework Core para PostgreSQL

var app = builder.Build(); // se construye la aplicaci�n a partir del objeto "builder"

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); // se configura la aplicaci�n para habilitar el comportamiento de marcas de tiempo heredado

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) // se verifica si la aplicaci�n est� en modo de desarrollo
{
    app.UseExceptionHandler("/Error"); // se configura el manejo de excepciones
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts(); // se habilita la pol�tica de seguridad HSTS
}

app.UseHttpsRedirection(); // se redirige el tr�fico HTTP a HTTPS
app.UseStaticFiles(); // se habilitan los archivos est�ticos

app.UseRouting(); // se habilita el enrutamiento
app.UseAuthentication(); ; // se habilita la autenticaci�n
app.UseAuthorization(); // se habilita la autorizaci�n

app.MapRazorPages(); // se mapean las p�ginas Razor

app.Run(); // se inicia la aplicaci�n


void AddScope()//Inyeccion de dependencias declarada arriba
{
    builder.Services.AddScoped<IUserRepository, UserRepository>(); // Se agrega el servicio del repositorio de usuarios
    builder.Services.AddScoped<IIdentityRoleRepository, RoleRepository>(); // Se agrega el servicio del repositorio de roles
    builder.Services.AddScoped<IUnionRepository, Union>();//Servicio de repositorio Union
}