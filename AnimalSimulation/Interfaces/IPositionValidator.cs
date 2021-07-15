using AnimalSimulation.Models;
using System;

namespace AnimalSimulation.Interfaces
{
    public interface IPositionValidator
    {
        bool IsValid(Position position);
    }

    public class WorldLimits : IPositionValidator
    {
        public WorldLimits(Position origin, Position edgeOfTheWorld)
        {
            Origin = origin ?? throw new ArgumentNullException(nameof(origin));
            EdgeOfTheWorld = edgeOfTheWorld ?? throw new ArgumentNullException(nameof(edgeOfTheWorld));
        }

        public Position Origin { get; }

        public Position EdgeOfTheWorld { get; }

        public bool IsValid(Position position)
        {
            if (position.X < Origin.X || position.Y < Origin.Y)
                return false;

            if (position.X >= EdgeOfTheWorld.X || position.Y >= EdgeOfTheWorld.Y)
                return false;

            return true;
        }
    }
}
