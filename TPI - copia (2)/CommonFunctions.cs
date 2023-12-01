using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Formatting = Newtonsoft.Json.Formatting;
using JsonSerializer = System.Text.Json.JsonSerializer;

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
            foreach(T item in set) Console.WriteLine(item);
        }

        
        public static void SaveMatch(Barrack Barrack)
        {
            try
            {
                //List<Location[]> mapAsAList = Map.MapArrayToList();
                //Location[,] map = Map.MapListToArray(mapAsAList);


                //for (int i = 0; i < Map.LatitudLongitud.GetLength(0); i++)
                //{
                //    Console.Write("\n");
                //    for (int j = 0; j < Map.LatitudLongitud.GetLength(1); j++)
                //    {
                //        Console.Write(((Land)Map.LatitudLongitud[i, j]).LandType.ToString()[0] + "");
                //    }
                //}
                //Console.WriteLine("\n\n\n\n");
                //for (int i = 0; i < map.GetLength(0); i++)
                //{
                //    Console.Write("\n");
                //    for (int j = 0; j < map.GetLength(1); j++)
                //    {
                //        Console.Write(((Land)map[i, j]).LandType.ToString()[0] + "");
                //    }
                //}

                /*
                string dynamicPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
                string folder = "\\data";
                string file = "\\operadorData.json";
                if (!Directory.Exists(dynamicPath + folder))
                    Directory.CreateDirectory(dynamicPath + folder);
                //string data = JsonSerializer.Serialize(Barrack.Operators);
                string jsonTypeNameAuto = JsonConvert.SerializeObject(Barrack,  new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });
                File.WriteAllText(dynamicPath + folder + file, jsonTypeNameAuto);
                */
                SaveMacthFile(Barrack);
                SaveMacthFile(Map.MapArrayToList());
                //string dynamicPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
                //string folder = "\\data";
                //string file = "\\operatorsData.json";
                //if (!Directory.Exists(dynamicPath + folder))
                //    Directory.CreateDirectory(dynamicPath + folder);
                //string data = JsonSerializer.Serialize(barrack);
                //File.WriteAllText(dynamicPath + folder + file, data);
            }
            catch (IOException x)
            {
                Console.WriteLine("No se ha encontrado ninguna partida guardada. Comienza el juego desde su inicio");
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


        public static (Location[,], Barrack) GetMatchStarted(string mapFileName, string barrackFileName)
        {
            Map.CreateMapRandomly();
            Barrack barrack = new Barrack();

            if (UserInputYesOrNo("Si desea cargar una partida guardada oprima s."))
            {
                try
                {
                    Map.LatitudLongitud = Map.MapListToArray(JsonSerializer.Deserialize<List<Land[]>>(GetMatchFile(mapFileName)));
                    //Barrack = JsonSerializer.Deserialize<Barrack>(File.ReadAllText("C:\\Users\\Ale\\Documents\\rollandia\\Beca\\NeorisUTNBA\\GitTrabajosPracticosProfesorSB\\TPsSB\\TPI\\data\\BarrackData.json"));
                    //operators = JsonSerializer.Deserialize<Operator>(File.ReadAllText("C:\\Users\\Ale\\Documents\\rollandia\\Beca\\NeorisUTNBA\\GitTrabajosPracticosProfesorSB\\TPsSB\\TPI\\data\\operadorData.json"));

                    barrack = JsonConvert.DeserializeObject<Barrack>(GetMatchFile(barrackFileName), new JsonSerializerSettings{TypeNameHandling = TypeNameHandling.Auto});
                    /*
                    barrack = JsonConvert.DeserializeObject<Barrack>(File.ReadAllText("C:\\Users\\Ale\\Documents\\rollandia\\Beca\\NeorisUTNBA\\GitTrabajosPracticosProfesorSB\\TPsSB\\TPI\\data\\operadorData.json"), new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    });
                    */
                }
                catch (IOException x)
                {
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

        public static bool UserInputYesOrNo(string message)
        {
            return UserInput(message) == "s";
        }
    }
}
