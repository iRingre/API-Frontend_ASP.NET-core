using FirebirdSql.Data.FirebirdClient;
using System.Security.Claims; 
using Microsoft.AspNetCore.Authentication; 
using Microsoft.AspNetCore.Authentication.Cookies;

public class Authentication
{

    private readonly IHttpContextAccessor _http; 
    public Authentication(IHttpContextAccessor http) 
    { 
        _http = http; 
    }
    //Per usare questa tipologia di servizio va Installato Firebird server 4.0
    string connectionString = new FbConnectionStringBuilder
    {
        Database = @"C:\Users\Utente\Desktop\Nuova cartella\API-Frontend_ASP.NET-core\ASP.NET\FrontEnd\Components\DB\GESTIONALE.FDB",
        UserID = "SYSDBA",
        Password = "masterkey",
        ServerType = FbServerType.Default,
        DataSource = "localhost", 
        Port = 3050
    }.ToString();

    public async Task<bool> LoginAsync(string username, string password)
    {
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