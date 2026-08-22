using Example.Blazor.Components;
using IncaTechnologies.Recurrence.Radzen;
using Radzen;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Resources;

var builder = WebApplication.CreateBuilder(args);

CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("it");

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<ILocalizer, RecurrenceLocalizer>();
builder.Services.AddRadzenComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
