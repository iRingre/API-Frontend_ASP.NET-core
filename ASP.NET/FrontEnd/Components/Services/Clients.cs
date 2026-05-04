using System.Text.Json.Serialization;
using FirebirdSql.Data.FirebirdClient;
public class Clients
{
    private readonly FirebirdConnectionProvider _provider;

    public Clients(FirebirdConnectionProvider provider)
    {
        _provider = provider;
    }

    public async Task<List<Client>> GetAllClients()
    {
        string connString = _provider.GetConnectionString();
        var con = new FbConnection(connString);
        string sql = @"SELECT * FROM CLIENTI ORDER BY ID_CLIENTE";

        var cmd = new FbCommand(sql, con);
        List<Client> ListOfClient = new List<Client>();
        con.Open();


        using (FbDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                Client t = new Client
                (
                    reader.GetInt32(0),
                    reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                    reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                    reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                    reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                    reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                    reader.IsDBNull(6) ? string.Empty : reader.GetString(6),
                    reader.GetInt32(7),
                    reader.GetDateTime(8),
                    reader.GetBoolean(9)
                );
                ListOfClient.Add(t);
            }

        }

        await con.CloseAsync();
        return ListOfClient;
    }

}


public class Client
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Cognome { get; set; }
    public string Email { get; set; }
    public string Telefono { get; set; }
    public string Indirizzo { get; set; }
    public string Citta { get; set; }
    public int Cap {  get; set; }
    public DateTime Registrazione { get; set; }
    public bool Enabled { get; set; }


    [JsonConstructor]
    public Client(int id, string nome, string cognome, string email, string telefono, string indirizzo, string citta, int cap, DateTime registrazione, bool enabled)
    {
        Id = id;
        Nome = nome;
        Cognome = cognome;
        Email = email;
        Telefono = telefono;
        Indirizzo = indirizzo;
        Citta = citta;
        Cap = cap;
        Registrazione = registrazione;
        Enabled = enabled;
    }
}