using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using RollandiAlejandro;

namespace RollandiAlejandro
{
    internal class Sucursal
    {
        public string codigo;
        public List<Vacuna> vacunas = new List<Vacuna>();
        public List<Virus> viruses = new List<Virus>();

        public Sucursal()
        {
            codigo = CreaCodigoAlfanumericoAleatorio(3);
        }

        static protected string CreaCodigoAlfanumericoAleatorio(int n)
        {
            Random random = new Random();
            string lettersNumbers = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] stringChars = new char[n];

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = lettersNumbers[random.Next(lettersNumbers.Length)];
            }
            return string.Join("", stringChars);
        }

        public override string ToString()
        {
            return codigo;
        }

    }


}

