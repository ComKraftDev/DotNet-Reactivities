using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.

// Pas besoin de redirection car on utilise uniquement du Https pas de http
//app.UseHttpsRedirection();

// No need for the moment
//app.UseAuthorization();

// fait le lien entre endPoint et controller
app.MapControllers();
// ce code sera exécuté au lancement de l'application
// le scope sera nettoyé par le GC dès qu'on aura fini avec
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<AppDbContext>();
    // créer la DB si elle n'existe pas ou alors met jour avec les dernières migrations non-appliquées
    await context.Database.MigrateAsync();
    // on ajouter les données en dur dans la DB
    await DbInitializer.SeedData(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration.");
    throw;
}

app.Run();
