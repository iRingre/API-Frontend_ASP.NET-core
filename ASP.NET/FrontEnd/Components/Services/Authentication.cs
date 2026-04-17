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



    /// <summary>
    /// Funzione che va a creare una sessione tramite Cookies dopo aver effettuato il controllo su database se l'user con `username` e `pasword` esiste
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
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
            await con.CloseAsync();
            return true;
        }
        await con.CloseAsync();
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