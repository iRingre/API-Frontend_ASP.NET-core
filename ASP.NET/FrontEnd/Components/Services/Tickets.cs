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
        ListOfTickets.Add(new Ticket("Imperatore dei Bug Ancestrali","aperto","0006","Ogni volta che compilo il progetto appare un druido che mi chiede di sacrificare la cartella bin","ORDINE DRUIDICO DEL DEBUG","alta"));
        ListOfTickets.Add(new Ticket("Cavaliere del Mouse Perduto","in lavorazione","0007","Il cursore si teletrasporta negli angoli dello schermo e ride","CONFRATERNITA DEI PERIFERICI SMARRITI","media"));
        ListOfTickets.Add(new Ticket("Archivista del Tempo Cache","aperto","0008","La cache continua a servire pagine del 1998 e rifiuta il presente","BIBLIOTECA CRONOLOGICA DI INTERNET","bassa"));
        ListOfTickets.Add(new Ticket("Oracolo della Stampante Furiosa","aperto","0009","La stampante stampa solo profezie e minacce in Comic Sans","TEMPIO SACRO DEL TONER ESAURITO","alta"));
        ListOfTickets.Add(new Ticket("Sindaco del Database Abissale","in lavorazione","0010","Le query SQL evocano tentacoli dal data center","MUNICIPIO DI CTHULHU DATA SOLUTIONS","media"));
        ListOfTickets.Add(new Ticket("Astronauta del Desktop Remoto","chiuso","0011","La sessione RDP apre un portale verso Windows 95","AGENZIA SPAZIALE DEI SISTEMISTI","bassa"));
        ListOfTickets.Add(new Ticket("Bibliotecario dei Log Eterni","aperto","0012","Il file di log ha raggiunto la coscienza e commenta le mie scelte di vita","ARCHIVIO INFINITO DEI PROCESSI","media"));
        ListOfTickets.Add(new Ticket("Domatore di Container Ribelli","in lavorazione","0013","Docker ha deciso di diventare una religione e chiede fedeli","MONASTERO KUBERNETICO","alta"));
        ListOfTickets.Add(new Ticket("Custode del Firewall Mistico","chiuso","0014","Il firewall blocca i pacchetti perché percepisce cattive vibrazioni","ORDINE ESOTERICO DELLA SICUREZZA","media"));
        ListOfTickets.Add(new Ticket("Viaggiatore della RAM Onirica","aperto","0015","La RAM sogna pecore elettriche e dimentica i processi","ISTITUTO PSICANALITICO DELL'HARDWARE","bassa"));
        ListOfTickets.Add(new Ticket("Strongest soldier","aperto","0016","Ḯ̷̗̱̥̩̯̜̩̙̘͙̙̙̰̐̏͌͂̑̈́̈́̚͜͠͝a̷̢̛͍̯̖̱̝͉̫͙͈̪̞̾͑̿̈́̍̎̈́̅̈́̐́̚͝!̴̰̙̬̳͎̐̇͂͂̐̑̈́̎̾̑̍̔͝ ̶̢̛̥̮͖̗̮̥̫͉͌̑̍͂̑͗͂͐̾́͝C̶̛̤̥͇̦̳̪̮͑͆̍͌̐̿̔͑̚͠ͅt̷̮̩̤̘̙̟͉̮̮̳̜̍͑̋̈́̐̈́̒͊̄̾̎͠h̷̹̱̲͖̙̲̞̩̟͓̓̈́̔͆̇͗̎͊̒͘̚͝u̷̢͉̤̻͎̟̟̱͑̾͂̐̾̐́̈́͋͑̚͝l̷̢͙̙̻̙͉̞͍̫̫̓́̈́͆̓̑̀͆̕͠h̶͙͈͉͎̗͓̗͈̝̖̅̽̿͆́͊̐̄̚̕͜͝ư̶͉̟̜̳̗̥͎̯͉̲̐̓͐̓̾̓̓̒͛͐̚ ̷̛͙̠̰̱͍͇̬̫͚͍̓̐̍̋̎̔̍͋̇f̶̡͔͓̞̲͖͇̲̯̝̾̐̽͆͐́̓̍͆̕͝ͅḧ̶͎̞͓̤̳͙̗̮̯́̍͑̐̑̍̐͂͒̔͘ͅṯ̶̡̢̛̛͔̥̝͓͎̻̘̾̑̽̑͋̄̕͝͝a̵̛͇̙̘̪̳̍̀̑͋̾̀́̓͠͝g̶͓̝̘͓͙̮͉̗̜̳͌͑͌̈́̓͗̄̽͘n̶̡̻̫̱̹͎̈́͋́͂̎̍͑̔͝͠͝","DIO","alta"));

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