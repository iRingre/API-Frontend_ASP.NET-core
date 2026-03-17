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
        ListOfClient.Add(new Client(1,"Ωmega","Pollo Primordiale","cliente1@email.com","+39 000000001"));
        ListOfClient.Add(new Client(2,"Arciduca","Spatola Quantistica","cliente2@email.com","+39 000000002"));
        ListOfClient.Add(new Client(3,"Entità 404","Senza Forma","cliente3@email.com","+39 000000003"));
        ListOfClient.Add(new Client(4,"Nonna","Antimateria","cliente4@email.com","+39 000000004"));
        ListOfClient.Add(new Client(5,"Profeta","del Router Eterno","cliente5@email.com","+39 000000005"));
        ListOfClient.Add(new Client(6,"Imperatore","dei Bug Ancestrali","cliente6@email.com","+39 000000006"));
        ListOfClient.Add(new Client(7,"Cavaliere","del Mouse Perduto","cliente7@email.com","+39 000000007"));
        ListOfClient.Add(new Client(8,"Archivista","del Tempo Cache","cliente8@email.com","+39 000000008"));
        ListOfClient.Add(new Client(9,"Oracolo","della Stampante Furiosa","cliente9@email.com","+39 000000009"));
        ListOfClient.Add(new Client(10,"Sindaco","del Database Abissale","cliente10@email.com","+39 000000010"));
        ListOfClient.Add(new Client(11,"Astronauta","del Desktop Remoto","cliente11@email.com","+39 000000011"));
        ListOfClient.Add(new Client(12,"Bibliotecario","dei Log Eterni","cliente12@email.com","+39 000000012"));
        ListOfClient.Add(new Client(13,"Domatore","di Container Ribelli","cliente13@email.com","+39 000000013"));
        ListOfClient.Add(new Client(14,"Custode","del Firewall Mistico","cliente14@email.com","+39 000000014"));
        ListOfClient.Add(new Client(15,"Viaggiatore","della RAM Onirica","cliente15@email.com","+39 000000015"));
        ListOfClient.Add(new Client(16,"Strongest","soldier","cliente16@email.com","+39 000000016"));


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