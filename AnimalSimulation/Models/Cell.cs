using AnimalSimulation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnimalSimulation.Models
{
    public class Cell
    {
        private List<Animal> animals = new List<Animal>();
        public Position Position { get; }

        public Cell(Position position)
        {
            Position = position ?? throw new ArgumentNullException(nameof(position));
        }

        public void Visit(Animal animal)
        {
            if (animal is null) throw new ArgumentNullException(nameof(animal));
            if (animals.Contains(animal)) throw new InvalidOperationException();

            animals.Add(animal);

            foreach (var anotherAnimal in animals.ToList())
            {
                if (animal.CanEat(anotherAnimal))
                    animal.Eat(anotherAnimal as IEatable, DefaultAttackModifier.Instance);

                var newAnimal = animal.Mate(anotherAnimal);
                if (newAnimal is null == false)
                    animals.Add(newAnimal);

                if (anotherAnimal.IsAlive == false)
                    animals.Remove(anotherAnimal);
            }
        }

        public void Leave(Animal animal)
        {
            if (animal is null) throw new ArgumentNullException(nameof(animal));
            if (animals.Contains(animal) == false) throw new InvalidOperationException();

            animals.Remove(animal);
        }

        public IEnumerable<Animal> GetAliveAnimals()
        {
            return animals.Where(x => x.IsAlive);
        }
    }
}
