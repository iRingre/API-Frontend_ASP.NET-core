using FirebirdSql.Data.FirebirdClient;
using System.Security.Claims; 
using Microsoft.AspNetCore.Authentication; 
using Microsoft.AspNetCore.Authentication.Cookies;

public class Authentication
{

    private readonly IHttpContextAccessor _http; 
    private readonly FirebirdConnectionProvider _provider;
    public Authentication(IHttpContextAccessor http, FirebirdConnectionProvider provider) 
    { 
        _http = http;
        _provider = provider; 
    }
    //Per usare questa tipologia di servizio va Installato Firebird server 4.0

    public async Task<bool> LoginAsync(string username, string password)
    {
        string connectionString = _provider.GetConnectionString();
        using var con = new FbConnection(connectionString);
        await con.OpenAsync();
    
        var cmd = new FbCommand(
            "SELECT COUNT(*) FROM UTENTI WHERE USERNAME=@u AND PASSWORD=@p",
            con
        );
    
        cmd.Parameters.AddWithValue("@u", username);
        cmd.Parameters.AddWithValue("@p", password);
    
        var result = await cmd.ExecuteScalarAsync();
        if(result!=null && (Int64)result>(Int64)0){
            return true;
        }
        return false;
    }

    public async Task CreateSession(string username) 
    { 
        var claims = new List<Claim> 
        { 
            new Claim(ClaimTypes.Name, username) 
        }; 
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme); 
        var principal = new ClaimsPrincipal(identity); 

        await _http.HttpContext!.SignInAsync( 
            CookieAuthenticationDefaults.AuthenticationScheme, 
            principal, 
            new AuthenticationProperties 
            { 
                IsPersistent = true, 
                ExpiresUtc = DateTime.UtcNow.AddMinutes(30) 
            }); 
    }

    public async Task LogoutAsync() 
    { 
        await _http.HttpContext!.SignOutAsync(); 
    }
}