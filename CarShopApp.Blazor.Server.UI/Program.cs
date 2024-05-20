using Blazored.LocalStorage;
using CarShopApp.Blazor.Server.UI.Configuration;
using CarShopApp.Blazor.Server.UI.Provider;
using CarShopApp.Blazor.Server.UI.Services;
using CarShopApp.Blazor.Server.UI.Services.Authentication;
using CarShopApp.Blazor.Server.UI.Services.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredLocalStorage();
//order matters
builder.Services.AddHttpClient<IClient, Client>(ba => ba.BaseAddress = new Uri("https://localhost:7111"));
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddScoped<IProducerService, ProducerService>();
builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddScoped<ApiAuthenticationStateProvider>();
builder.Services.AddScoped<ApiAuthenticationStateProvider>(p => 
                p.GetRequiredService<ApiAuthenticationStateProvider>());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
