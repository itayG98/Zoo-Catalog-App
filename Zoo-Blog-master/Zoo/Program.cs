using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Model.DAL;
using Model.Repositories;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Configuration.
    AddJsonFile("appsettings.json").
    AddJsonFile("appsettings.docker.json", true).
    AddEnvironmentVariables();
builder.Services.AddDbContext<ZooDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("data"));
    options.EnableSensitiveDataLogging();
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
builder.Services.AddDbContext<ZooIdentityContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("identity"));
});
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<ZooIdentityContext>();
builder.Services.AddScoped<AnimalRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<CommentRepository>();
builder.Services.AddCors(options =>
{
    options.
    AddPolicy(name: "LocalHosts",
        policy =>
        {
            policy.WithOrigins(
                "https://localhost:5063",
                "https://zoo-catalog-app.azurewebsites.net"
                ).
            AllowAnyHeader().
            AllowAnyMethod();
        });
});
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ZooDBContext>();
     //context.Database.EnsureDeleted(); //For deleting and Re-Seedng the Database
     //context.Database.EnsureCreated();
}
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ZooIdentityContext>();
    //context.Database.EnsureDeleted(); //For deleting and Re-Seedng the Database
    //context.Database.EnsureCreated();
}
var provider = new FileExtensionContentTypeProvider();
// Add new mappings
provider.Mappings[".js"] = "Text/Javascript";
provider.Mappings[".css"] = "text/css";
provider.Mappings[".png"] = "image/png";
provider.Mappings[".jpg"] = "image/jpeg";
provider.Mappings[".jpeg"] = "image/jpeg";
provider.Mappings[".gif"] = "image/gif";
provider.Mappings[".svg"] = "image/svg+xml";
provider.Mappings[".ico"] = "image/x-icon";


app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
    ContentTypeProvider = provider
});
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
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
