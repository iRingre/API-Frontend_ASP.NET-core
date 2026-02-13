using FrontEnd.Components;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);//crea la web application

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddScoped<Authentication>();

builder.Services.AddHttpClient();//metti client in forma di http così da fare delle chiamate api tramite http


//servizi per autentication tramite cookie e management delle sessioni
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    options.Cookie.Name = "authToken";
    options.LoginPath = "/login";
    options.Cookie.MaxAge = TimeSpan.FromMinutes(30);
    options.AccessDeniedPath = "/login";
    options.Cookie.SameSite = SameSiteMode.Lax; 
    options.Cookie.SecurePolicy = CookieSecurePolicy.None;
    options.SlidingExpiration = true;
});
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddHttpContextAccessor();
/*------------------------------------------------------------------------------------------*/


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseCookiePolicy();


app.UseAuthentication();
app.UseAuthorization();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

//Creazione di un record e di un api basata su di esso per il login 
app.MapPost("/api/login", async (HttpContext ctx, Authentication auth, LoginModel model) =>
{
    var ok = await auth.LoginAsync(model.Username, model.Password);
    if (ok==false)
        return Results.Unauthorized();

    await auth.CreateSession(model.Username);
    return Results.Ok();
});


app.Run();

public record LoginModel(string Username, string Password);
