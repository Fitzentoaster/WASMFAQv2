using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using WASMFAQv2.Components;
using WASMFAQv2.Server.Contexts;
using WASMFAQv2.Server.Repository;
using WASMFAQv2.Shared.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddMudServices();

builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddCors(options =>
{
    options.AddPolicy("ClientAppPolicy", policy =>
        policy.WithOrigins("https://localhost:5001") 
              .AllowAnyHeader()
              .AllowAnyMethod());
});


builder.Services.AddScoped<IQnASetRepository, QnASetRepository>();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<FAQContext>(options =>
    options.UseNpgsql(connectionString), ServiceLifetime.Transient);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5001, listenOptions =>
    {
        listenOptions.UseHttps(); 
    });

    options.ListenLocalhost(5000); 
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseBlazorFrameworkFiles();
app.UseRouting();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(WASMFAQv2.Client._Imports).Assembly);

app.MapControllers();
app.MapFallbackToFile("index.html");


app.Run();
