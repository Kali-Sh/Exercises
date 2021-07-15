using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalSimulation.Interfaces
{
    public interface IAttackModifier
    {
        bool CalculateChances();
    }

    public class DayNightAttackModifier : IAttackModifier
    {
        public bool CalculateChances()
        {
            return true;
        }
    }

    public class DefaultAttackModifier : IAttackModifier
    {
        Random rnd = new Random();
        static DefaultAttackModifier instance;
        readonly static object mutex = new object();

        private DefaultAttackModifier() { }

        public static DefaultAttackModifier Instance
        {
            get
            {
                if (instance is null)
                {
                    lock (mutex)
                    {
                        if (instance is null)
                            instance = new DefaultAttackModifier();
                    }
                }

                return instance;
            }
        }

        public bool CalculateChances()
        {
            return rnd.Next(0, 100) > 40;
        }
    }
}
