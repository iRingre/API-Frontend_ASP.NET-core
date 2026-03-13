using System.Numerics;
using System.Text.Json.Serialization;
using FirebirdSql.Data.FirebirdClient;

public class Tickets
{
    private readonly FirebirdConnectionProvider _provider;

    public Tickets(FirebirdConnectionProvider provider)
    {
     _provider = provider;   
    }

    public async Task<List<Ticket>> GetAllTickets()//dovranno essere passati dei criteri di filtraggio alla funzione 
    {
        string connString = _provider.GetConnectionString();
        var con = new FbConnection(connString);

        var cmd = new FbCommand();
        List<Ticket> ListOfTickets = new List<Ticket>();
        ListOfTickets.Add(new Ticket("boia","deh",",","1","si va a letto ?","dehh"));

        return ListOfTickets;
    }
}


public class Ticket
{
    public string Assegnatario { get; set; }
    public string Categoria { get; set; }
    public string DatiCliente { get; set; }
    public string DescrizioneDettagliata { get; set; }
    public string Richiedente { get; set; }
    public string Urgenza { get; set; }

    [JsonConstructor]
    public Ticket(
        string assegnatario,
        string categoria,
        string datiCliente,
        string descrizioneDettagliata,
        string richiedente,
        string urgenza)
    {
        Assegnatario = assegnatario;
        Categoria = categoria;
        DatiCliente = datiCliente;
        DescrizioneDettagliata = descrizioneDettagliata;
        Richiedente = richiedente;
        Urgenza = urgenza;
    }
}