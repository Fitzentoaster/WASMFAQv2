using WASMFAQv2.Shared.Interfaces;
using WASMFAQv2.Client.Services;
using MudBlazor.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();

builder.Services.AddScoped<IFAQApiService, FAQApiService>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
