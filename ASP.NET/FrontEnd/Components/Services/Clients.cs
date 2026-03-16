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

        var cmd = new FbCommand();
        List<Client> ListOfClient = new List<Client>();
        ListOfClient.Add(new Client(1,"IO","ZIO","pippo@gmail.com","123456789"));

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

    [JsonConstructor]
    public Client(int id, string nome, string cognome, string email, string telefono)
    {
        Id = id;
        Nome = nome;
        Cognome = cognome;
        Email = email;
        Telefono = telefono;
    }
}