namespace TPI
{
    internal class OperatorBattery : OperatorElement
    {
        public OperatorBattery(Operator o) : base(o, 4000, 6500, 12250)
        {
        }
        public override void ElementActualValueDefinition()
        {
            Actual = Maximum/2;   // se pone para iniciar a mitad de baterìa para probar carga y transferencia
        }

        public void BatteryRecharge()
        {
            Actual = Maximum;
            Console.WriteLine("Baterìa al 100%");
        }
    }
}
