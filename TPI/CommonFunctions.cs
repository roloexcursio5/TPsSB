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
    }
}
