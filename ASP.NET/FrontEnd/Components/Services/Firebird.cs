using FirebirdSql.Data.FirebirdClient;
using Microsoft.Extensions.Options;

//Per usare questa tipologia di servizio va Installato Firebird server 4.0

public class FirebirdConfig
{
    public string Server { get; set; }
    public int Port { get; set; }
    public string Database { get; set; }
    public string User { get; set; }
    public string Password { get; set; }
    public string Charset { get; set; }
}

public class FirebirdConnectionProvider
{
    private readonly FirebirdConfig _config;
    private readonly IWebHostEnvironment _env;

    public FirebirdConnectionProvider(IOptions<FirebirdConfig> config, IWebHostEnvironment env)
    {
        _config = config.Value;
        _env = env;
    }

    public string GetConnectionString()
    {
        var absolutePath = Path.Combine(
            _env.ContentRootPath,
            _config.Database);

        return new FbConnectionStringBuilder
        {
            Database = absolutePath,
            UserID = _config.User,
            Password = _config.Password,
            ServerType = FbServerType.Default,
            DataSource = _config.Server,
            Port = _config.Port,
            Charset = _config.Charset
        }.ToString();
    }
}