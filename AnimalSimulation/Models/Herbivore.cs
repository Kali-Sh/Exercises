using AnimalSimulation.Interfaces;

namespace AnimalSimulation.Models
{
    public class Herbivore : Animal, IEatable
    {
        public Herbivore(Position position) : base(position) { }

        public Herbivore(Herbivore parent1, Herbivore parent2) : base(parent1, parent2) { }

        public override bool CanEat(Animal animal)
        {
            return false;
        }

        public override void Eat(IEatable eatable, IAttackModifier attackModifier) { }

        public void Kill()
        {
            IsAlive = false;
        }

        public override Animal Mate(Animal animal)
        {
            var herbivore = animal as Herbivore;
            if (CanMate(herbivore))
                return new Herbivore(this, herbivore);

            return null;
        }
    }
}
