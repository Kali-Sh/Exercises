using System;
using System.Collections.Generic;
using Simulation.Creatures;


namespace Simulation
{
    class Program
    {
        private static char[,] world = new char[10, 10];
        static void Main(string[] args)
        {
            int daysPassed = 0;

            int xCoordinateH;
            int yCoordinateH;

            int xCoordinateC;
            int yCoordinateC;

            List <Carnivores> ListC= new List<Carnivores>();
            List<Herbivores> ListH = new List<Herbivores>();

            // creating a random amount between 10 and 30 of herbivores and carnivores in the world           
            Random rnd = new Random();
            int hCount = rnd.Next(10, 31); 
            int cCount = rnd.Next(10, 31);

            int herbivoreCount = hCount;
           


            //random starting positions for the herbivores
            for (int i = 0; i < hCount; i++)
            {
                xCoordinateH = rnd.Next(0, 10);
                yCoordinateH = rnd.Next(0, 10);
                
                //checking to see if a herbivore is already on these coordinates
                while (world[xCoordinateH, yCoordinateH] == 'h')
                {
                    xCoordinateH = rnd.Next(0, 10);
                    yCoordinateH = rnd.Next(0, 10);
                }

                //creating a new herbivore object and adding it to the list
                Herbivores herbivores = new Herbivores
                {
                   
                    HPositionX=xCoordinateH,
                    HPositionY=yCoordinateH
                };
                ListH.Add(herbivores);

                world[xCoordinateH, yCoordinateH] = 'h';
              
            }

            //random starting positions for the carnivores
            //we can assume that the starting positions of the 2 species never overlap
            for (int i = 0; i < cCount; i++)
            {
                xCoordinateC = rnd.Next(0, 10);
                yCoordinateC = rnd.Next(0, 10);

                while(world[xCoordinateC, yCoordinateC] == 'h' || world[xCoordinateC, yCoordinateC] == 'c')
                {
                    xCoordinateC = rnd.Next(0, 10);
                    yCoordinateC = rnd.Next(0, 10);
                }

                //creating a new carnivore object and adding it to the list
                Carnivores carnivores = new Carnivores
                {
                  
                   CPositionX=xCoordinateC,
                   CPositionY=yCoordinateC
                };

                ListC.Add(carnivores);
                world[xCoordinateC, yCoordinateC] = 'c';

            }
          

            while (true)
            {

                if (hCount == 0)
                {
                    Console.WriteLine($"{herbivoreCount} herbivores were killed by {cCount} carnivores after {daysPassed} days!");
                    break;
                }

                //generating random movement for each day for herbivores
                foreach (var herbivores in ListH)
                {
                    int x = herbivores.HPositionX;
                    int y = herbivores.HPositionY;

                    //if herbivore was attacked previous day and field assigned with S, now it will move and the carnivore is still there bbecause it hasnt moved yet
                    if (world[x, y] == 'S')
                    {
                        world[x, y] = 'c';
                    }
                    else
                    {
                        world[x, y] = (char)0;
                      
                    }

                    x = rnd.Next(0, 10);
                    y = rnd.Next(0, 10);

                    //herbivores wont go near carnivores, because of fear and wont go near other herbivores because of teritory
                    while (world[x, y] == 'c' || world[x, y] == 'h')
                    {
                        x = rnd.Next(0, 10);
                        y = rnd.Next(0, 10);
                    }

                    world[x, y] = 'h';

                    //setting the new position for the herbivore object
                    herbivores.HPositionX=x;
                    herbivores.HPositionY = y;
              

                }

                Visualizing(world);
                System.Threading.Thread.Sleep(200);
                Console.Clear();


                //generating random movement for carnivores
                foreach (var carnivore in ListC)
                {
                    int x = carnivore.CPositionX;
                    int y = carnivore.CPositionY;
                    
                    world[x, y] = (char)0;
                  
                    x = rnd.Next(0, 10);
                    y = rnd.Next(0, 10);


                    //carnivores wont go near other carnivores because of teritory
                    while (world[x, y] == 'c')
                    {
                        x = rnd.Next(0, 10);
                        y = rnd.Next(0, 10);
                    }

                    if (world[x, y] == 'h')
                    {
                        //we have a 40% chance of survival of our herbivore when attacked by the carvinore
                        if ((rnd.Next(5) == 1) || (rnd.Next(5) == 2))
                        {
                            world[x, y] = 'S';

                            //setting the new position for the carnivore object
                            carnivore.CPositionX = x;
                            carnivore.CPositionY = y;
                            Console.WriteLine("The herbivore SURVIVED the attack !");
                        }

                        else
                        {
                            Console.WriteLine("The herbivore was KILLED...");
                            hCount--;
                            var herbivore = ListH.Find(h => (h.HPositionX.Equals(x)) && (h.HPositionY.Equals(y)));
                            ListH.Remove(herbivore);

                           
                            world[x, y] = 'c';
                            carnivore.CPositionX = x;
                            carnivore.CPositionY = y;
                        }


                    }
                    else
                    {
                        world[x, y] = 'c';
                        carnivore.CPositionX = x;
                        carnivore.CPositionY = y;
                    }


                    

                }
                Visualizing(world);
                System.Threading.Thread.Sleep(200);
                Console.Clear();

                daysPassed++;

            }
            
        }



        //visualizing a 2d world with each animal at its' place
        private static void Visualizing(char[,] world)
        {
 
            for (var i = 0; i < 10; i++)
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
}
