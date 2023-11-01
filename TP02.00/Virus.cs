using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RollandiAlejandro
{
    internal class Virus : Vacuna
    {
        public Virus(List<Virus> viruses) : base (new List<Vacuna>(viruses))
        {
        }

        public Virus(List<Virus> viruses, string identificador, Tipo tipo)
        {
            CargaNombre();
            CargaIdentificadorDesdeVacuna(new List<Vacuna>(viruses), "Agregue un caracter al código de vacuna para generar el código del virus", identificador);
            this.tipo = tipo;
        }

        private void CargaIdentificadorDesdeVacuna(List<Vacuna> viruses, string mensaje, string identificadorDesdeVacuna)
        {
            string identificador = null;
            string caracterAdicional;
            while (identificador == null)
            {
                caracterAdicional = SolicitaInput(mensaje);
                identificador = ValidaLongitudUnitaria(caracterAdicional) ? identificadorDesdeVacuna + caracterAdicional : null;
                identificador = ValidaUnicidad(viruses, identificador) ? identificador : null;
            }
            identificador = this.identificador;
        }

        private bool ValidaLongitudUnitaria(string input)
        {
            return (input.Length == 1);
        }

        public Virus(int a, int b) : base (a, b)
       {
            this.nombre = CreaCodigoAlfanumericoAleatorio(a);
            this.identificador = CreaCodigoAlfanumericoAleatorio(b);
            this.tipo = EligeUnTipoAleatoriamente();
       }

        public override string ToString()
        {
            return $"{codigo}\t\t{identificador}\t\t{tipo}";
        }

    }
}
