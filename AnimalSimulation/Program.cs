using System;
using AnimalSimulation.Models;

namespace AnimalSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            World world = new World(20, 10, TimeSpan.FromMilliseconds(200));
            world.Simulate();
        }
    }
}
