﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using mvcProyectoWeb.AccesoDatos.Data.Repository;
using mvcProyectoWeb.AccesoDatos.Data.Repository.IRepository;
using mvcProyectoWeb.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString
    ("ConexionSQL") ?? throw new InvalidOperationException
    ("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllersWithViews(); //(después de esta línea de comando)
//agregar contenedor de trabajo al contenedos IoC de inyeccion de dependencias
builder.Services.AddScoped<IContenedorTrabajo, ContenedorTrabajo>();


builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Cliente}/{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();