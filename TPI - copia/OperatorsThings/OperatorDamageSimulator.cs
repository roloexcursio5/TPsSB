namespace TPI
{
    abstract class DamageCreator
    {
        public abstract Damage FactoryMethod();
        public bool Damage()
        {
            Damage damage = FactoryMethod();

            return damage.DamageOccurs();
        }
    }

    class DamageCreatorCompromisedEngine : DamageCreator
    {
        public override Damage FactoryMethod()
        {
            return new DamageCompromisedEngine();
        }
    }

    class DamageCreatorStuckServo : DamageCreator
    {
        public override Damage FactoryMethod()
        {
            return new DamageStuckServo();
        }
    }
    class DamageCreatorPerforatedBattery : DamageCreator
    {
        public override Damage FactoryMethod()
        {
            return new DamagePerforatedBattery();
        }
    }
    class DamageCreatorDisconnectedBatteryPort : DamageCreator
    {
        public override Damage FactoryMethod()
        {
            return new DamageDisconnectedBatteryPort();
        }
    }
    class DamageCreatorScratchedPaint : DamageCreator
    {
        public override Damage FactoryMethod()
        {
            return new DamageScratchedPaint();
        }
    }
    class DamageCreatorReducedMaximumBatteryCapacity : DamageCreator
    {
        public override Damage FactoryMethod()
        {
            return new DamageReducedMaximumBatteryCapacity();
        }
    }


    /////////////////////
    public abstract class Damage
    {
        protected Random random = new Random();
        public abstract bool DamageOccurs();
    }

    class DamageCompromisedEngine : Damage
    {
        public override bool DamageOccurs()
        {
            return random.NextDouble() < 0.03;
        }
    }

    class DamageStuckServo : Damage
    {
        public override bool DamageOccurs()
        {
            return random.NextDouble() < 0.03;
        }
    }
    class DamagePerforatedBattery : Damage
    {
        public override bool DamageOccurs()
        {
            return random.NextDouble() < 0.03;
        }
    }
    class DamageDisconnectedBatteryPort : Damage
    {
        public override bool DamageOccurs()
        {
            return random.NextDouble() < 0.03;
        }
    }
    class DamageScratchedPaint : Damage
    {
        public override bool DamageOccurs()
        {
            return random.NextDouble() < 0.1;
        }
    }
    class DamageReducedMaximumBatteryCapacity : Damage
    {
        public override bool DamageOccurs()
        {
            return random.NextDouble() < 0.03;
        }
    }


    class Damages
    {
        public HashSet<EnumOperatorDamage> DamageRandomCreator()
        {
            HashSet<EnumOperatorDamage> damages = new HashSet<EnumOperatorDamage>();

            if (DamageSimulator(new DamageCreatorCompromisedEngine()))
                damages.Add(EnumOperatorDamage.compromisedEngine);
            if (DamageSimulator(new DamageCreatorStuckServo()))
                damages.Add(EnumOperatorDamage.stuckServo);
            if (DamageSimulator(new DamageCreatorPerforatedBattery()))
                damages.Add(EnumOperatorDamage.perforatedBattery);
            if (DamageSimulator(new DamageCreatorDisconnectedBatteryPort()))
                damages.Add(EnumOperatorDamage.disconnectedBatteryPort);
            if (DamageSimulator(new DamageCreatorScratchedPaint()))
                damages.Add(EnumOperatorDamage.scratchedPaint);
            if (DamageSimulator(new DamageCreatorReducedMaximumBatteryCapacity()))
                damages.Add(EnumOperatorDamage.reducedMaximumBatteryCapacity);

            if(damages.Count() > 0)
            {
                Console.WriteLine("Durante la acciòn se produjeron los siguientes daños:\n");
                foreach (EnumOperatorDamage damage in damages)
                {
                    Console.WriteLine(damage);
                }
            }
            

            return damages;
        }

        public bool DamageSimulator(DamageCreator creator)
        {
            return creator.Damage();
        }
    }
}
