using System.Text.Json.Serialization;

namespace TPI
{
    internal class OperatorM8 : Operator //semi-humanoides de carga
    {
        public OperatorM8()
        {
            UniqueID = Barrack.UniqueID();
            GeneralStatus = EnumOperatorStatus.available;
            Battery = new OperatorBattery(this);  //mAh miliAmperios, 1000mAh = 1 hour use
            Load = new OperatorLoad(this); // kilos
            Speed = new OperatorSpeed(30); // kilometros/hora
            Location = new Location(Map.LandTypeLocation(EnumLandType.barrack).First().Latitud, Map.LandTypeLocation(EnumLandType.barrack).First().Longitud);
            OperatorDamages = new HashSet<EnumOperatorDamage>();
        }
}
}
