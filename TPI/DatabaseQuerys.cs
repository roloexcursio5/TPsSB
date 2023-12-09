using System.Data.SqlClient;
namespace TPI
{
    static internal class DatabaseQuerys
    {
        static public List<(int OperatorUniqueID, int Kilometers, int BatteryConsumption, int TransportedCargo, int Damages, int LocationLatitud, int LocationLongitud)> data = new List<(int, int, int, int, int, int, int)>();

        static public void GetAllData()     // incorrecto, luego debo traer sòlo la info necesaria segùn la consulta que se desee realizar FALTA
        {
            SqlConnection cnn = DatabaseConection.GetSQLBaseConection();
            SqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "SELECT * FROM OperatorsActivityLog";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                data.Add(((int)reader[1], (int)reader[2], (int)reader[3], (int)reader[4], (int)reader[5], (int)reader[6], (int)reader[7]));
            }
            reader.Close();
        }

        static public void KilometersTotal()
        {
            Console.WriteLine($"La cantidad total de kilómetros recorridos por todos los operadores es de: {(from i in data select i.Kilometers).Sum()}");
        }
        static public void KilometersOperator(int n)
        {
            Console.WriteLine($"La cantidad total de kilómetros recorridos por el operador {n} es de: {(from i in data where i.OperatorUniqueID == n select i.Kilometers).Sum()}");
        }
    }
}


