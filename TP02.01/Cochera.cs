using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RollandiAlejandro.TamañoCocheraAuto;
using static RollandiAlejandro.MedidaCocheraAuto;

namespace RollandiAlejandro
{
    internal class Cochera
    {
        Random random = new Random();
        public int box;
        public int cocheraAncho = 0;
        public int cocheraLargo = 0;
        public string cocheraTamaño = "";
        public bool cocheravip = false;
        public bool cocheraOcupada = false;
        public int autoAncho = 0;
        public int autoLargo = 0;
        public string autoTamaño = "";
        public bool dueñoVip = false;
        public string dni = "";
        public int modelo = 0;
        public string matricula = "       \t";
        public bool coincideTamañoCocheraAuto = false;


        public Cochera(int box)
        {
            if (box <= 12)
            {
                this.DefineTamaño();
                this.DefineAnchoYLargo();
                this.DefineExclusividad(box);
            }
            else
            {
                this.DefineTamaño();
                this.DefineAnchoYLargo();
                this.DefineExclusividad(box);
            }
        }

        private void DefineTamaño()
        {
            int cocheraTamañoIndex = random.Next(Enum.GetValues(typeof(TamañoCocheraAuto)).Length);
            this.cocheraTamaño = Enum.GetValues(typeof(TamañoCocheraAuto)).GetValue(cocheraTamañoIndex).ToString();
        }

        private void DefineAnchoYLargo()
        {
            if (this.cocheraTamaño == TamañoCocheraAuto.mini.ToString())
            {
                int anchoMinimo = (int)MedidaCocheraAuto.miniAnchoMin;
                int anchoMaximo = (int)MedidaCocheraAuto.miniAnchoMax;
                this.cocheraAncho = random.Next(anchoMinimo, anchoMaximo);

                int largoMinimo = (int)MedidaCocheraAuto.miniLargoMin;
                int largoMaximo = (int)MedidaCocheraAuto.miniLargoMax;
                this.cocheraLargo = random.Next(largoMinimo, largoMaximo);
            }
            else if (this.cocheraTamaño == TamañoCocheraAuto.standar.ToString())
            {
                int anchoMinimo = (int)MedidaCocheraAuto.standarAnchoMin;
                int anchoMaximo = (int)MedidaCocheraAuto.standarAnchoMax;
                this.cocheraAncho = random.Next(anchoMinimo, anchoMaximo);

                int largoMinimo = (int)MedidaCocheraAuto.standarLargoMin;
                int largoMaximo = (int)MedidaCocheraAuto.standarLargoMax;
                this.cocheraLargo = random.Next(largoMinimo, largoMaximo);
            }
            else
            {
                int anchoMinimo = (int)MedidaCocheraAuto.maxAnchoMin;
                int anchoMaximo = (int)MedidaCocheraAuto.maxAnchoMax;
                this.cocheraAncho = random.Next(anchoMinimo, anchoMaximo);

                int largoMinimo = (int)MedidaCocheraAuto.maxLargoMin;
                int largoMaximo = (int)MedidaCocheraAuto.maxLargoMax;
                this.cocheraLargo = random.Next(largoMinimo, largoMaximo);
            }
        }

        private void DefineExclusividad(int box)
        {
            if (box < 13)
            {
                this.cocheravip = (box == 3 || box == 7 || box == 12) ? true : false;
            }
            else
            {
                this.cocheravip = (float)random.NextDouble() < 0.1 ? true : false;
            }
        }
    }
}
