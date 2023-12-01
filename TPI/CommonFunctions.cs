using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;


// PARA PROBAR TENER UNA CLASE CON FUNCIONES ÚTILES QUE SE REPITEN
namespace TPI
{
    static internal class CommonFunctions
    {
        public static string UserInput(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine().Trim().ToLower();
        }
        public static void IfErrorMessageShowIt(string message)
        {
            if (message != "")
                Console.WriteLine(message + "\nComo consecuencia de ello, se retorna al menú\n\n"); 
        }
        public static void PrintList<T>(List<T> set)
        {
            foreach (T item in set) Console.WriteLine(item);
        }

        public static bool UserInputYesOrNo(string message)
        {
            return UserInput(message) == "s";
        }



        ///Métodos para guardar y salir del juego
        public static void ExitMatch(Barrack Barrack)
        {
            if (UserInputYesOrNo("Si desea guardar la partida oprima s."))
                try
                {
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
        }

        ///Metodo para Abrir el juego
        public static (Location[,], Barrack) GetMatchStarted(string mapFileName, string barrackFileName)
        {
            Map.CreateMapRandomly();
            Barrack barrack = new Barrack();

            if (UserInputYesOrNo("Si desea cargar una partida guardada oprima s."))
            {
                try
                {
                    Map.LatitudLongitud = Map.MapListToArray(JsonSerializer.Deserialize<List<Land[]>>(GetMatchFile(mapFileName)));
                    barrack = JsonConvert.DeserializeObject<Barrack>(GetMatchFile(barrackFileName), new JsonSerializerSettings{TypeNameHandling = TypeNameHandling.Auto});
                }
                catch (IOException x)
                {
                    Console.WriteLine(x.Message);
                    Console.WriteLine("No se ha encontrado ninguna partida guardada. Comienza el juego desde su inicio");
                     barrack.CreateAnOperatorRandomly(10);
                }
            }
            else
            {
                barrack.CreateAnOperatorRandomly(10);
            }
 
            return (Map.LatitudLongitud, barrack);
        }

        private static string GetMatchFile(string savedMatchFile)
        {
            string dynamicPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string folder = "\\data";
            string file = $"\\{savedMatchFile}Data.json";
            string data = File.ReadAllText(dynamicPath + folder + file);
            return data;
        }
    }
}
