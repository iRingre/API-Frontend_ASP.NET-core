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
        List<Ticket> ListOfTickets = new List<Ticket>();
        string connString = _provider.GetConnectionString();
        var con = new FbConnection(connString);
        string sql = "SELECT ID, ASSEGNATARIO, CATEGORIA, DATICLIENTE, DESCRIZIONEDETTAGLIATA, RICHIEDENTE, URGENZA, DATACREAZIONE FROM TICKETS";

        var cmd = new FbCommand(sql, con);
        con.Open();


        using (FbDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                Ticket t = new Ticket
                (
                    reader.GetInt32(0),
                    reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                    reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                    reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                    reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                    reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                    reader.IsDBNull(6) ? string.Empty : reader.GetString(6),
                    reader.GetDateTime(7)
                );
                ListOfTickets.Add(t);
            }
            
        }

        await con.CloseAsync();
        return ListOfTickets;
    }

    public async Task<bool> SaveModifiedTikets(List<Ticket> saveTickets)
    {
        

        
        return true;
    }
}


public class Ticket
{
    public int Id {get; set;}
    public string Assegnatario { get; set; }
    public string Categoria { get; set; }
    public string DatiCliente { get; set; }
    public string DescrizioneDettagliata { get; set; }
    public string Richiedente { get; set; }
    public string Urgenza { get; set; }

    public DateTime DataCreazione {get; set;}

    [JsonConstructor]
    public Ticket(
        int id,
        string assegnatario,
        string categoria,
        string datiCliente,
        string descrizioneDettagliata,
        string richiedente,
        string urgenza,
        DateTime dataCreazione)
    {
        Id = id;
        Assegnatario = assegnatario;
        Categoria = categoria;
        DatiCliente = datiCliente;
        DescrizioneDettagliata = descrizioneDettagliata;
        Richiedente = richiedente;
        Urgenza = urgenza;
        DataCreazione = dataCreazione;
    }
}