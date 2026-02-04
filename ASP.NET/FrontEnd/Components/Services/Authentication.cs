using FirebirdSql.Data.FirebirdClient;

public class Authentication
{

    string connectionString = new FbConnectionStringBuilder
    {
        Database = @"C:\Database\miofile.fdb",
        UserID = "SYSDBA",
        Password = "masterkey",
        ServerType = FbServerType.Embedded
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
        if (result!=null)return true;
        return false;
    }


}