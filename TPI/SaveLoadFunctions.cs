using Newtonsoft.Json;
using System.Data.SqlClient;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace TPI
{
    static internal class SaveLoadFunctions
    {

        //////////////////////////////////////
        ///Métodos para guardar y salir del juego
        public static void ExitMatch(Barrack Barrack)
        {
            if (CommonFunctions.UserInputYesOrNo("Si desea guardar la partida oprima s."))
                try
                {
                    SqlConnection cnn = DatabaseConection.GetSQLBaseConection();
                    SaveMatchSQLBase(cnn, Barrack);
                    SaveMacthFile(Barrack);
                    SaveMacthFile(Map.MapArrayToList());
                }
                catch (IOException x)
                {
                    Console.WriteLine(x.Message);
                    Console.WriteLine("No se ha podido guardar la partida. Mil Disculpas.");
                }
                finally
                {
                    Console.WriteLine("Has salido del programa");
                    Environment.Exit(0);
                }
            else
            {
                Console.WriteLine("Has salido del programa");
                Environment.Exit(0);
            }
        }

        private static void SaveMacthFile(object gameElement)
        {
            string dynamicPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string folder = "\\data";
            string file = $"\\{gameElement.ToString().Split(".").Last().Replace("[", "").Replace("]", "")}Data.json";
            if (!Directory.Exists(dynamicPath + folder))
                Directory.CreateDirectory(dynamicPath + folder);
            string data = JsonConvert.SerializeObject(gameElement, new JsonSerializerSettings{TypeNameHandling = TypeNameHandling.Auto});
            File.WriteAllText(dynamicPath + folder + file, data);
            Console.WriteLine($"Se guardó en archivo local el objeto {file}");
        }


        private static void SaveMatchSQLBase(SqlConnection cnn, Barrack Barrack)
        {
            if (cnn != null)
            {
                SaveMatchSQLBaseBarrack(cnn, Barrack);
                SaveMatchSQLBaseMap(cnn);
                Console.WriteLine("Consultas ejecutadas. Cerrando servicio");
                cnn.Close();
                Console.WriteLine("Conexión cerrada");
            }
        }

        private static void SaveMatchSQLBaseBarrack(SqlConnection cnn, Barrack Barrack)
        {
            try
            {
                SqlCommand cmd = cnn.CreateCommand();
                cmd.CommandText = "DELETE FROM Barrack;";
                Console.WriteLine($"Se eliminaron {cmd.ExecuteNonQuery()} registros de la partida anterior.");
                int contador = 0;
                for (int i = 0; i < Barrack.Operators.Count; i++)
                {
                    cmd.CommandText = $"INSERT INTO Barrack (AutoIncrementalID,LocationLatitud,LocationLongitud,OperatorType,OperatorUniqueID,OperatorGeneralStatus,OperatorBattery,OperatorLoad,OperatorLocationLatitud,OperatorLocationLongitud,OperatorDamages) VALUES ({Barrack.ToString(i)});";
                    contador += cmd.ExecuteNonQuery();
                }
                Console.WriteLine($"Se insertaron en la base de datos: {contador} operadores");
            }
            catch (SqlException x)
            {
                Console.WriteLine(x);
                Console.WriteLine("No se pudieron guardar los datos del cuartel");
            }
        }

        private static void SaveMatchSQLBaseMap(SqlConnection cnn)
        {
            try
            {
                SqlCommand cmd = cnn.CreateCommand();

                cmd.CommandText = "DELETE FROM Map;";
                Console.WriteLine($"Se eliminaron {cmd.ExecuteNonQuery()} registros de la partida anterior.");
                int contador = 0;
                for (int i = 0; i < Map.LatitudLongitud.GetLength(0); i++)
                {
                    for (int j = 0; j < Map.LatitudLongitud.GetLength(1); j++)
                    {
                        cmd.CommandText = $"INSERT INTO Map (Latitud,Longitud,LandType) VALUES ({Map.ToString(i, j)});";
                        contador += cmd.ExecuteNonQuery();
                    }
                }
                Console.WriteLine($"Se guardaron en la base de datos las {contador} ubicaciones que tiene el mapa");
            }
            catch (SqlException x)
            {
                Console.WriteLine(x);
                Console.WriteLine("No se pudo guardar el mapa");
            }
        }

        ///////////////////////////////
        ///Metodo para Abrir el juego
        public static (Location[,], Barrack) GetMatchStarted(string mapFileName, string barrackFileName)
        {
            Map.CreateMapRandomly();
            Barrack Barrack = new Barrack();

            if (CommonFunctions.UserInputYesOrNo("Si desea cargar una partida guardada oprima s."))
            {
                SqlConnection cnn = DatabaseConection.GetSQLBaseConection();

                bool LoadMatchSuccess = false;
                if (!LoadMatchSuccess)
                    LoadMatchSuccess = GetMatchSQLBase(cnn, Barrack);
                if (!LoadMatchSuccess)
                    LoadMatchSuccess = GetMatchFile(ref Barrack, mapFileName, barrackFileName);
                if (!LoadMatchSuccess)
                {
                    Console.WriteLine("No se ha encontrado ninguna partida guardada. Comienza el juego desde su inicio");
                    Barrack.CreateAnOperatorRandomly(10);
                }
            }
            else
            {
                Barrack.CreateAnOperatorRandomly(10);
            }
 
            return (Map.LatitudLongitud,Barrack);
        }



        private static bool GetMatchFile(ref Barrack Barrack, string mapFileName, string barrackFileName)
        {
            bool success = false;
            
            try
            {
                Map.LatitudLongitud = Map.MapListToArray(JsonSerializer.Deserialize<List<Land[]>>(GetMatchSavedFile(mapFileName)));
                Barrack = JsonConvert.DeserializeObject<Barrack>(GetMatchSavedFile(barrackFileName), new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
                success = true;
            }
            catch (IOException x)
            {
                Console.WriteLine(x.Message);
            }

            return success;
        }



        private static string GetMatchSavedFile(string savedMatchFile)
        {
            string dynamicPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string folder = "\\data";
            string file = $"\\{savedMatchFile}Data.json";
            string data = File.ReadAllText(dynamicPath + folder + file);
            return data;
        }


        private static bool  GetMatchSQLBase(SqlConnection cnn, Barrack Barrack)
        {
            bool successGettingBarrack = false;
            bool successGettingMap = false;

            if (cnn != null)
            {
                successGettingBarrack = GetMatchSQLBaseBarrack(cnn, Barrack);
                successGettingMap = GetMatchSQLBaseMap(cnn);

                if (!(successGettingBarrack && successGettingMap))
                    Console.WriteLine("No se ha encontrado o podido cargar la partida desde la base SQL. Se intentará cargar desde archivo");

                Console.WriteLine("Consultas ejecutadas. Cerrando servicio");
                cnn.Close();
                Console.WriteLine("Conexión cerrada");
            }

            return successGettingBarrack && successGettingMap;
        }


        private static bool GetMatchSQLBaseBarrack(SqlConnection cnn, Barrack Barrack)
        {
            bool success = false;
            
            try
            {
                SqlCommand cmd = cnn.CreateCommand();

                //Para completar el listado de operadores
                cmd.CommandText = "SELECT * FROM Barrack";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Operator operatorx = Operator.CargoOperadorDesdeBaseSQL(reader[3].ToString());

                    operatorx.CompletoOperadorDesdeBaseSql((int)reader[4], (EnumOperatorStatus)Enum.Parse(typeof(EnumOperatorStatus), reader[5].ToString(), true), (int)reader[6], (int)reader[7], (int)reader[8], (int)reader[9], (EnumOperatorDamage)Enum.Parse(typeof(EnumOperatorDamage), reader[10].ToString(), true));

                    Barrack.Operators.Add(operatorx);
                }
                reader.Close();

                //Para completar los datos del cuartel, debe ir luego de operadores ya que al crear operadores nuevos aumenta el contador, de este modo se neutraliza dicho impacto
                cmd.CommandText = "SELECT TOP 1 AutoincrementalID,LocationLatitud,LocationLongitud,count(*) FROM Barrack GROUP BY AutoIncrementalID,LocationLatitud,LocationLongitud ORDER BY AutoIncrementalID,LocationLatitud,LocationLongitud DESC";
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Barrack.AutoIncrementalID = (int)reader[0];
                    Barrack.Location.Latitud = (int)reader[1];
                    Barrack.Location.Longitud = (int)reader[2];
                    success = true; // me aseguro que aunque sea tiene un registro de barraca
                }
                reader.Close();
            }
            catch (SqlException x)
            {
                Console.WriteLine("No se pudieron cargar los datos del cuartel");
                Console.WriteLine(x);
            }

            return success;
        }

        private static bool GetMatchSQLBaseMap(SqlConnection cnn)
        {
            bool success = false;

            try
            {
                SqlCommand cmd = cnn.CreateCommand();
                cmd.CommandText = "SELECT * FROM Map";
                SqlDataReader reader = cmd.ExecuteReader();
                int contador = 0;
                while (reader.Read())
                {
                    ((Land)Map.LatitudLongitud[(int)reader[0], (int)reader[1]]).LandType = (EnumLandType)Enum.Parse(typeof(EnumLandType), reader[2].ToString(), true);
                    contador++;
                }
                success = (contador == Map.LatitudLongitud.GetLength(0) * Map.LatitudLongitud.GetLength(0)) ? true : false; // me aseguro que en la base exista la misma cantidad de tierra que el mapa del juego por comenzar
                reader.Close();
            }
            catch (SqlException x)
            {
                Console.WriteLine("No se pudieron cargar los datos del mapa");
                Console.WriteLine(x);
            }

            return success;
        }
    }
}


