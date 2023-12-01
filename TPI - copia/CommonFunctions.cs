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
        public static void PrintList<T>(List<T> list)
        {
            foreach(T item in list) Console.WriteLine(item);
        }

    }
}
