using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Simulation.Creatures;


namespace Simulation
{


    

    public abstract class Cell
    {
       List<Cell>cells= new List<Cell>();
        public Cell(int x, int y)
        {         

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    
                    
                    
                }
            }

        }
    }

    public class World:Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
      
        public World(int x, int y)
            :base(x,y)

        {
            this.X = x;
            this.Y = y;
            
        }

        public static void Visualizing(char[,] world)
        {

            for (var i = 0; i < 1; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    if (world[i, j] == (char)0)
                        Console.Write('*');
                    else
                        Console.Write(world[i, j]);
                }
                Console.WriteLine();
            }
        }
    }

    public abstract class Position
    {
        public int X { get; set; }
        public int Y { get;  set; }


        public Position(int x, int y)
        {
            if (x < 0) { throw new ArgumentOutOfRangeException(); }
            if (y < 0) { throw new ArgumentOutOfRangeException(); }

            this.X = x;
            this.Y = y;

        }
    }

    public class CurrentPosition : Position
    {
        public CurrentPosition(int x, int y)
             : base(x, y)
        {
            base.X = x;
            base.Y = y;
        }

    }



    public abstract class Animal : Position
    {
        

        public Animal(Position position)
            : base(position.X, position.Y)

        {
           
            this.X = position.X;
            this.Y = position.Y;
        }
     
        public abstract bool Eat();
        public abstract void Move();
        

        public override bool Equals(object obj)
        {

            if ((obj == null) || (!(obj is Animal)))
            {
                return false;
            }

            return (this.X == ((Animal)obj).X)
                && (this.Y == ((Animal)obj).Y);
        }


        public override int GetHashCode()
        {
            return X * 17 + Y * 23;
        }

    }



    public class Herbivore:Animal
    {
       
        public Herbivore(Position position)
            : base(position)
           
        {
            
        }

        public override void Move()
        {
           
            Random rnd = new Random();           
            this.X += rnd.Next(-X, 11);
            this.Y += rnd.Next(-Y, 11);

        }
        public override bool Eat()
        {
            return false;
        }

       
    }

  

   


    public class Carnivore : Animal
    {
        public Carnivore(Position position)
            : base(position)
        {

        }
 

        public override bool Eat()
        {
            Random rnd = new Random();

            //herbivore survives the attack
            if ((rnd.Next(5) == 1) || (rnd.Next(5) == 2))
            {
                return true;
            }

            //herbivore is eaten
            else
            {
                return false;
            }
        }


        public override void Move()
        {
            Random rnd = new Random();          
            this.X += rnd.Next(-X, 11);
            this.Y += rnd.Next(-Y, 11);

        }
    }

    public enum Event
    {
        Eat,
        Move,
        Die
    }
    
    public class Simulation: Position
    {
        public List<Animal> Animals { get; set; }
        public int Days { get; set; }

       // private readonly Dictionary<Event, IDistribution> _distributions;

        public Simulation(IEnumerable<Animal> animals,Position position)
            :base(position.X, position.Y)
        {
            Animals = new List<Animal>(animals);
           
        }
    }

  


    




    class Program
    {
       // private static char[,] world = new char[10, 10];
        static void Main(string[] args)
        {
           
            

            CurrentPosition current = new CurrentPosition(1, 2);
            CurrentPosition current2 = new CurrentPosition(1, 3);
            Carnivore carnivore1 = new Carnivore(current);
            Carnivore carnivore2 = new Carnivore(current2);
            Herbivore herbivore = new Herbivore(current2);
            Herbivore herbirove2 = new Herbivore(current2);

            herbirove2.GetType();
            

            List<Herbivore> test = new List<Herbivore>();
            test.Add(herbivore);
            test.Add(herbirove2);

           if( object.Equals(carnivore1, carnivore2))
            {
                Console.WriteLine("da");
            }

            herbivore.Move();
          
               

            var animals = new List<Animal>
            {
                new Herbivore(current),
                new Herbivore(current2),
                new Carnivore(current),
                new Carnivore(current2)
            };


            foreach(var animal in animals)
            {
                animal.Move();
            }



            for(int i=0; i<animals.Count; i++)
            {
                animals[i].Move();     
                for(int j=1; j<animals.Count; j++)
                {
                    if((object.Equals(animals[i], animals[j]) && animals[i].GetType()!=animals[j].GetType()))
                    {
                        if (animals[i].Equals(typeof(Carnivore)))
                        {
                            animals[i].Eat();
                        }
                        else
                        {
                            animals[j].Eat();
                        }
                    }
                    else if(animals[i].GetType() == animals[j].GetType())
                    {
                        animals[i].Move();
                    }
                            
                }           
            }

           

        }


        //visualizing a 2d world with each animal at its' place
       


        
    }
}
