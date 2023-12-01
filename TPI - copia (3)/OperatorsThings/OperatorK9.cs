﻿using System.Text.Json.Serialization;
using System.Xml;

namespace TPI
{
    internal class OperatorK9 : Operator // unidades cuadrúpedas
    {
        public OperatorK9()
        {
            UniqueID = Barrack.UniqueID();
            GeneralStatus = EnumOperatorStatus.available;
            Battery = new OperatorBattery(this);  //mAh miliAmperios, 1000mAh = 1 hour use
            Load = new OperatorLoad(this); // kilos
            Speed = new OperatorSpeed(40); // kilometros/hora
            Location = new Location(Map.LandTypeLocation(EnumLandType.barrack).First().Latitud, Map.LandTypeLocation(EnumLandType.barrack).First().Longitud);
            OperatorDamages = new HashSet<EnumOperatorDamage>();
        }
    }
}
