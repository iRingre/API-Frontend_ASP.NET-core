using System.Numerics;
using FirebirdSql.Data.FirebirdClient;

public class Tickets
{
    private readonly FirebirdConnectionProvider _provider;

    public Tickets(FirebirdConnectionProvider provider)
    {
     _provider = provider;   
    }

    public async Task<Vector<Ticket>> GetAllTickets()//dovranno essere passati dei criteri di filtraggio alla funzione 
    {
        string connString = _provider.GetConnectionString();
        var con = new FbConnection(connString);

        var cmd = new FbCommand();
        Vector<Ticket> ListOfTickets = new Vector<Ticket>();

        return ListOfTickets;
    }
}


public class Ticket
{
    //inserire i datiche rappresentano i singoli ticket
    private string datiCLiente;
    private string assegnatario;
    private string richiedente;
    private int Lurgenza;
    private string categoria;
    private string descrizioneDettagliata;

    public Ticket(string dati, string ass, string ric, int urg, string categ, string desc)
    {
        datiCLiente = dati;
        assegnatario = ass;
        richiedente = ric;
        Lurgenza = urg;
        categoria = categ;
        descrizioneDettagliata = desc;
    }

    
}