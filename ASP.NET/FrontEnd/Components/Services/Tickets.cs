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
        ListOfTickets.Add(new Ticket("Ωmega Pollo Primordiale","aperto","0001","Il PC ha collassato in un buco nero ogni volta che apro il gestionale","CONSORZIO COSMICO DEL POLLAIO","alta"));
        ListOfTickets.Add(new Ticket("Arciduca Spatola Quantistica","in lavorazione","0002","La cache contiene ricordi di vite passate e rifiuta di svuotarsi","MINISTERO DELLE POSATE TEMPORALI","bassa"));
        ListOfTickets.Add(new Ticket("Entità 404 Senza Forma","chiuso","0003","Il server risponde ma sostiene che la realtà non esiste","FONDAZIONE PARADOSSI DIGITALI","media"));
        ListOfTickets.Add(new Ticket("Nonna Antimateria","chiuso","0004","Il microonde ha compilato codice C++ e ora pretende RAM","CUCINA NUCLEARE SPA","media"));
        ListOfTickets.Add(new Ticket("Profeta del Router Eterno","aperto","0005","Il router trasmette solo versetti binari sul destino dell'universo","CHIESA DEL PING INFINITO","alta"));



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