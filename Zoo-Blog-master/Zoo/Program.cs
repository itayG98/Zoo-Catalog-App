using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Model.DAL;
using Model.Repositories;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ZooDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
    options.EnableSensitiveDataLogging();
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
builder.Services.AddDbContext<ZooIdentityContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityUsersConnectionString"));
});
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<ZooIdentityContext>();
builder.Services.AddScoped<AnimalRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<CommentRepository>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ZooDBContext>();
    //context.Database.EnsureDeleted(); //For deleting and Re-Seedng the Database
    context.Database.EnsureCreated();
}
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ZooIdentityContext>();
    //context.Database.EnsureDeleted(); //For deleting and Re-Seedng the Database
    context.Database.EnsureCreated();
}


app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseStaticFiles();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{param?}"
    );
}
);
app.Run(async (context) =>
{
    await context.Response.WriteAsync("Sorry cant serve your request");
});

app.Run();
