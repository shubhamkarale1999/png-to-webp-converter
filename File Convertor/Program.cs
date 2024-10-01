using File_Convertor.Data;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MovieCoreDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MvcCoreMovieContext") ?? throw new InvalidOperationException("Connection string 'MvcCoreMovieContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600; // 100 MB limit
});


var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
