using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RollandiAlejandro
{
    internal class Auto
    {
        Random random = new Random();
        public int autoAncho;
        public int autoLargo;
        public string autoTamaño;
        public bool dueñoVip;
        public string dni;
        public int modelo;
        public string matricula;

        public Auto()
        {
            this.DefineTamaño();
            this.DefineAnchoYLargo();
            this.DefineExclusividad();
            this.DefineDNI();
            this.DefineModelo();
            this.DefineMatricula();
        }

        private void DefineTamaño()
        {
            int autoTamañoIndex = random.Next(Enum.GetValues(typeof(TamañoCocheraAuto)).Length);
            this.autoTamaño = Enum.GetValues(typeof(TamañoCocheraAuto)).GetValue(autoTamañoIndex).ToString();
        }

        private void DefineAnchoYLargo()
        {
            if (this.autoTamaño == TamañoCocheraAuto.mini.ToString())
            {
                int anchoMinimo = (int)MedidaCocheraAuto.miniAnchoMin;
                int anchoMaximo = (int)MedidaCocheraAuto.miniAnchoMax;
                this.autoAncho = random.Next(anchoMinimo, anchoMaximo);

                int largoMinimo = (int)MedidaCocheraAuto.miniLargoMin;
                int largoMaximo = (int)MedidaCocheraAuto.miniLargoMax;
                this.autoLargo = random.Next(largoMinimo, largoMaximo);
            }
            else if (this.autoTamaño == TamañoCocheraAuto.standar.ToString())
            {
                int anchoMinimo = (int)MedidaCocheraAuto.standarAnchoMin;
                int anchoMaximo = (int)MedidaCocheraAuto.standarAnchoMax;
                this.autoAncho = random.Next(anchoMinimo, anchoMaximo);

                int largoMinimo = (int)MedidaCocheraAuto.standarLargoMin;
                int largoMaximo = (int)MedidaCocheraAuto.standarLargoMax;
                this.autoLargo = random.Next(largoMinimo, largoMaximo);
            }
            else
            {
                int anchoMinimo = (int)MedidaCocheraAuto.maxAnchoMin;
                int anchoMaximo = (int)MedidaCocheraAuto.maxAnchoMax;
                this.autoAncho = random.Next(anchoMinimo, anchoMaximo);

                int largoMinimo = (int)MedidaCocheraAuto.maxLargoMin;
                int largoMaximo = (int)MedidaCocheraAuto.maxLargoMax;
                this.autoLargo = random.Next(largoMinimo, largoMaximo);
            }
        }

        private void DefineExclusividad()
        {
            dueñoVip = (float)random.NextDouble() < 0.1 ? true : false;
        }
        private void DefineDNI()
        {
            string numbers = "0123456789";
            char[] dniArray = new char[8];

            for (int i = 0; i < dniArray.Length; i++)
            {
                dniArray[i] = numbers[random.Next(numbers.Length)];
            }
            
            this.dni = string.Join("", dniArray);
    }
        private void DefineModelo()
        {
            this.modelo = random.Next(1900, DateTime.Now.Year);
        }
        private void DefineMatricula()
        {
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string numbers = "0123456789";
            char[] stringChars = new char[7];

            for (int i = 0; i < 2; i++)
            {
                stringChars[i] = letters[random.Next(letters.Length)];
            }
            for (int i = 2; i < 5; i++)
            {
                stringChars[i] = numbers[random.Next(numbers.Length)];
            }
            for (int i = 5; i < stringChars.Length; i++)
            {
                stringChars[i] = letters[random.Next(letters.Length)];
            }

            this.matricula = string.Join("",stringChars);
        }

    }
}
