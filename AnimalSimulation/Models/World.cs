using AnimalSimulation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AnimalSimulation.Models
{
    public class World
    {
        private readonly int maxX;
        private readonly int maxY;
        private readonly TimeSpan cycle;
        private readonly List<Cell> cells = new List<Cell>();

        //add all cells from world coordinats in a list of cells
        public World(int maxX, int maxY, TimeSpan cycle)
        {
            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    cells.Add(new Cell(new Position(x, y)));
                }
            }

            //randomly populate every cell with new animal or leave it empty
            var rnd = new Random();
            foreach (var cell in cells)
            {
                var populateChance = rnd.Next(0, 3);
                if (populateChance == 0)
                    continue;

                if (populateChance == 1)
                    cell.Visit(new Herbivore(cell.Position));

                if (populateChance == 2)
                    cell.Visit(new Carnivore(cell.Position));
            }

            this.maxX = maxX;
            this.maxY = maxY;
            this.cycle = cycle;
        }

        public void Simulate()
        {
            var aliveAnimals = cells.SelectMany(x => x.GetAliveAnimals());

            do
            {
                foreach (var animal in aliveAnimals.ToList())
                {
                    var oldCell = cells.FirstOrDefault(x => x.Position == animal.CurrentPosition);

                    var newPosition = animal.Move(new WorldLimits(new Position(0, 0), new Position(maxX, maxY)));
                    if (oldCell.Position == newPosition)
                        continue;

                    var newCell = cells.FirstOrDefault(x => x.Position == newPosition);
                    if (newCell is null)
                        continue;

                    oldCell.Leave(animal);
                    newCell.Visit(animal);
                }

                foreach (var animal in aliveAnimals)
                {
                    animal.Grow();
                }

                Draw();
                Thread.Sleep(cycle);
            } while (aliveAnimals.OfType<Herbivore>().Any());
        }

        private void Draw()
        {
            Console.Clear();
            var defaultColor = Console.ForegroundColor;

            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    var cell = cells.At(x, y);
                    var firstAlive = cell.GetAliveAnimals().FirstOrDefault();
                    if (firstAlive is null)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(" ");
                    }
                    else if (firstAlive.GetType() == typeof(Herbivore))
                    {
                        if (firstAlive.Age < 5)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("h");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write("H");
                        }
                    }
                    else if (firstAlive.GetType() == typeof(Carnivore))
                    {
                        if (firstAlive.Age < 5)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("c");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write("C");
                        }
                    }
                }

                Console.Write(Environment.NewLine);
                Console.ForegroundColor = defaultColor;
            }
        }
    }
}
