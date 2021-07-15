using System;
using AnimalSimulation.Interfaces;

namespace AnimalSimulation.Models
{
    public enum Gender
    {
        Male,
        Female
    }

    public abstract class Animal
    {
        Random random = new Random();

        public Animal(Position position)
        {
            CurrentPosition = position ?? throw new ArgumentNullException(nameof(position));
            Gender = GetRandomGender();
        }

        protected Animal(Animal parent1, Animal parent2)
        {
            CurrentPosition = parent1.CurrentPosition;
            Gender = GetRandomGender();
        }

        public Position CurrentPosition { get; protected set; }

        public bool IsAlive { get; protected set; } = true;

        public Gender Gender { get; private set; }

        public int Age { get; private set; } = 0;

        public virtual Position Move(IPositionValidator positionValidator)
        {
            if (IsAlive == false)
                return CurrentPosition;

            Position newPosition = CurrentPosition;
            var direction = random.Next(0, 5);
            switch (direction)
            {
                case 0:
                    break;
                case 1:
                    newPosition = CurrentPosition.MoveByX(-1);
                    break;
                case 2:
                    newPosition = CurrentPosition.MoveByY(-1);
                    break;
                case 3:
                    newPosition = CurrentPosition.MoveByX(1);
                    break;
                case 4:
                    newPosition = CurrentPosition.MoveByY(1);
                    break;
                default:
                    throw new NotSupportedException($"Direction of '{direction}' is not supported.");
            }

            if (positionValidator.IsValid(newPosition))
                CurrentPosition = newPosition;

            return CurrentPosition;
        }

        public void Grow() => Age++;

        protected virtual bool CanMate(Animal animal)
        {
            if (animal is null == false && Age >= 5 && animal.Age >= 5 && Gender != animal.Gender)
                return true;

            return false;
        }

        public abstract Animal Mate(Animal animal);

        public abstract bool CanEat(Animal animal);

        public abstract void Eat(IEatable eatable, IAttackModifier attackModifier);

        private Gender GetRandomGender() => random.Next(0, 2) == 0 ? Gender.Male : Gender.Female;
    }
}
