using FrontEnd.Components;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);//crea la web application

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddScoped<Authentication>();


builder.Services.AddHttpClient();//metti client in forma di http così da fare delle chiamate api tramite http
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7257/")
});
/*-------------------------------------------------------------------*/
//Firebird
builder.Services.Configure<FirebirdConfig>(
    builder.Configuration.GetSection("FirebirdConfig"));

builder.Services.AddSingleton<FirebirdConnectionProvider>();

/*-------------------------------------------------------------------*/
//servizi per autentication tramite cookie e management delle sessioni
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    options.Cookie.Name = "authToken";
    options.LoginPath = "/login";
    options.LogoutPath = "/logout";
    options.Cookie.MaxAge = TimeSpan.FromMinutes(30);
    options.AccessDeniedPath = "/login";
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.SlidingExpiration = true;

});
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<Tickets>();
builder.Services.AddScoped<Clients>();
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

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();


app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


/*---------------------------------- LOG IN/OUT ---------------------------------------*/

app.MapPost("/api/login", async (HttpContext ctx, Authentication auth) =>
{
    var form = await ctx.Request.ReadFormAsync();
    var username = form["Username"];
    var password = form["Password"];

    if (await auth.LoginAsync(username!, password!))
    {
        await auth.CreateSession(username!);
        return Results.Redirect("/home");

    }else return Results.Redirect("/login?error=1");
});

app.MapPost("/api/logout", async (HttpContext ctx, Authentication auth) =>
{
    await auth.LogoutAsync();

    return Results.Redirect("/login");
});

/*----------------------------------------------------------------------------------*/

/*---------------------------------- TICKETS ---------------------------------------*/

app.MapGet("/api/tickets", async (Tickets tk) =>
{
    return Results.Ok(await tk.GetAllTickets());
});

app.MapPost("/api/savetickets", async (List<Ticket> tk, Tickets t) =>
{
    if(await t.SaveModifiedTikets(tk))return Results.Ok();
    else return Results.Problem();    
});

app.MapPost("/api/deleteTickets", async(List<Ticket> tk, Tickets t) =>
{
    if(await t.DeleteTickets(tk))return Results.Ok();
    else return Results.Problem();  
});

/*----------------------------------------------------------------------------------*/

/*---------------------------------- CLIENTS ---------------------------------------*/

app.MapGet("/api/clients", async (Clients cli) =>
{
    return Results.Ok(await cli.GetAllClients());
});

/*----------------------------------------------------------------------------------*/

app.Run();

