using System.Text.Json.Serialization;

namespace TPI
{
    internal class OperatorUAV : Operator//drones voladores de varias hélices
    {
        public OperatorUAV() 
        {
            UniqueID = Barrack.UniqueID();
            GeneralStatus = EnumOperatorStatus.available;
            Battery = new OperatorBattery(this);  //mAh miliAmperios, 1000mAh = 1 hour use
            Load = new OperatorLoad(this); // kilos
            Speed = new OperatorSpeed(50); // kilometros/hora
            Location = new Location(Map.LandTypeLocation(EnumLandType.barrack).First().Latitud, Map.LandTypeLocation(EnumLandType.barrack).First().Longitud); 
            OperatorDamages = new HashSet<EnumOperatorDamage>();
        }
    }
}
