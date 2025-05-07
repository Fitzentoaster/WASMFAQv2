using WASMFAQv2.Shared.Interfaces;
using WASMFAQv2.Client.Services;
using MudBlazor.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IFAQApiService, FAQApiService>();
builder.Services.AddScoped<ISortManager, SortManager>();
builder.Services.AddScoped<IQnAEditManager, QnAEditManager>();
builder.Services.AddScoped<IQnASetEditManager, QnASetEditManager>();

builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
