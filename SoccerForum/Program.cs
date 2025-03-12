using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SoccerForum.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<SoccerForumContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DatabaseConnection"))
);

// Add Identity services
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Set to true if you require email confirmation
})
    .AddEntityFrameworkStores<SoccerForumContext>();

// Add controllers with views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();  // Enable authentication
app.UseAuthorization();   // Enable authorization

// Identity routes (Login, Register, etc.)
app.MapRazorPages();

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
