using Microsoft.EntityFrameworkCore;
using SoccerForum.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<SoccerForumContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DatabaseConnection"))
);

// Add controllers with views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Use HSTS for enhanced security
}

app.UseHttpsRedirection();  // Redirect HTTP requests to HTTPS
app.UseStaticFiles();  // Serve static files like images, CSS, JS

app.UseRouting();  // Enable routing

app.UseAuthorization();  // Add authorization middleware if you need to protect routes

// Define the default route for controllers and views
app.UseStaticFiles();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();  // Start the application
