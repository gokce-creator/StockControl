using Microsoft.EntityFrameworkCore;
using StockControl.Data;
using StockControl.Models;

var builder = WebApplication.CreateBuilder(args);

// Portu her ortamda 4209 olarak ayarla
builder.WebHost.UseUrls("http://+:4209");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection(); // HTTPS yönlendirmeyi devre dýþý býrakmak istersen yorum satýrýna al
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Machines}/{action=Index}/{id?}");

// Seed data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (!context.Machines.Any())
    {
        context.Machines.AddRange(
            new Machine
            {
                Name = "Balya Makinesi ",
                Parts = new List<Part>
                {
                    new Part { Name = "Çelik gövde", StockQuantity = 50 },
                    new Part { Name = "Mil", StockQuantity = 50 }
                }
            },
            new Machine
            {
                Name = "Balya",
                Parts = new List<Part>
                {
                    new Part { Name = "Tekerlek", StockQuantity = 100 },
                    new Part { Name = "Toplama yayý 6 mm *5190*", StockQuantity = 200 }
                }
            }
        );
        context.SaveChanges();
    }
}

app.Run();
