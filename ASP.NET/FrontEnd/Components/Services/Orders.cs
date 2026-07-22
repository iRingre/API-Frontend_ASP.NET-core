

using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using FirebirdSql.Data.FirebirdClient;
public class Orders
{
    private readonly FirebirdConnectionProvider _provider;

    public Orders(FirebirdConnectionProvider provider)
    {
     _provider = provider;   
    }

    public async Task<List<Order>> GetOrders()
    {
        List<Order> ListOfOrders = new List<Order>();
        var con = new FbConnection(_provider.GetConnectionString());
        string sql = @"SELECT ID, NUMEROORDINE, DATAORDINE, CLIENTEID, STATO, TOTALE, NOTE, CREATOIL, MODIFICATOIL
                        ORDER BY ID ";
        var cmd = new FbCommand(sql, con);
        con.Open();


        using (FbDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                ListOfOrders.Add(new Order(
                    reader.GetInt32(0),
                    reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                    reader.IsDBNull(2) ? DateTime.Now : reader.GetDateTime(2),
                    reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                    reader.IsDBNull(4) ? 0 : reader.GetInt32(4),
                    reader.IsDBNull(5) ? 0 : reader.GetInt32(5),
                    reader.IsDBNull(6) ? string.Empty : reader.GetString(6),
                    reader.IsDBNull(7) ? DateTime.Now : reader.GetDateTime(7),
                    reader.IsDBNull(8) ? DateTime.Now : reader.GetDateTime(8)
                )
                );
            }
            
        }

        await con.CloseAsync();
        return ListOfOrders;
    }
}



public class Order
{
    public int Id {get; set;}
    public string Numeroordine {get; set;}
    public DateTime Dataordine {get; set;}
    public int Clienteid {get; set;}
    public int Stato {get; set;}
    public float Totale {get; set;}
    public string Note {get; set;}
    public DateTime Creatoil {get; set;}
    public DateTime Modificatoil {get; set;}


    [JsonConstructor]
    public Order(
        int id,
        string numeroordine,
        DateTime dataordine,
        int clienteid,
        int stato,
        float totale,
        string note,
        DateTime creatoil,
        DateTime modificatoil
    )
    {
        Id = id;
        Numeroordine = numeroordine;
        Dataordine = dataordine;
        Clienteid = clienteid;
        Stato = stato;
        Totale = totale;
        Note = note;
        Creatoil = creatoil;
        Modificatoil = modificatoil;
    }
}