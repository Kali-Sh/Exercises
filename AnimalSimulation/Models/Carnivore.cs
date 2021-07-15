using AnimalSimulation.Interfaces;
using System;

namespace AnimalSimulation.Models
{
    public class Carnivore : Animal
    {
        public Carnivore(Position position) : base(position) { }

        public Carnivore(Carnivore parent1, Carnivore parent2) : base(parent1, parent2) { }

        public override bool CanEat(Animal animal)
        {
            return (animal as Herbivore) is null == false;
        }

        public override void Eat(IEatable eatable, IAttackModifier attackModifier)
        {
            if (attackModifier is null)
                eatable.Kill();
            else if (attackModifier.CalculateChances())
            {
                eatable.Kill();
            }
        }

        public override Animal Mate(Animal animal)
        {
            var carnivore = animal as Carnivore;
            if (CanMate(carnivore))
                return new Carnivore(this, carnivore);

            return null;
        }
    }
}
