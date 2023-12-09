using System.Data.SqlClient;

namespace TPI
{
    static internal class DatabaseConection
    {
        static string DataSource;
        static string UserID;
        static string Password;
        static string InitialCatalog;

        static public void FillConectionInformation(string DataSourceInfo, string UserIDeInfo, string PasswordeInfo, string InitialCatalogeInfo)
        {
            DataSource = DataSourceInfo;
            UserID = UserIDeInfo;
            Password = PasswordeInfo;
            InitialCatalog = InitialCatalogeInfo;
        }
        public static SqlConnection GetSQLBaseConection()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = DataSource;
            builder.UserID = UserID;
            builder.Password = Password;
            builder.InitialCatalog = InitialCatalog;
            builder.TrustServerCertificate = true;

            SqlConnection cnn = new SqlConnection(builder.ConnectionString);
            Console.WriteLine("Conectando a la base de datos");
            try
            {
                cnn.Open();
                Console.WriteLine("Se conectó");
                return cnn;
            }
            catch (SqlException x)
            {
                Console.WriteLine(x);
                cnn = null;
            }
            return cnn;
        }
    }
}
