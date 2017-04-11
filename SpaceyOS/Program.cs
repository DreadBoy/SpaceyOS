using Interfaces;
using System;

namespace SpaceyOS
{
    class Program
    {
        static void Main(string[] args)
        {
            SpaceyOS spaceyOS = new SpaceyOS();
            ResetColour();

            //AssemblyCompiler compiler = new AssemblyCompiler(new[] { @"ForceFieldAPI.csx" });
            //SpaceShip ship = new SpaceShip();
            //var ff = new ForceField(ship);
            //var ffa = (IForceFieldAPI)compiler.CreateInstance("ForceFieldAPI");
            //ffa.Init(ship, ff);
            //ff.API = ffa;

            //ship.ShipSystem.Add(new ForceField(ship));

            //ff.MockHit();

            while (!spaceyOS.Exit)
            {
                Console.Write($"{spaceyOS.WorkingDirectory}: ");
                var input = Console.ReadLine();
                var ret = spaceyOS.ReadLine(input);
                foreach (var line in ret)
                {
                    if (line.OverwriteColour)
                    {
                        Console.ForegroundColor = line.ForegroundColor;
                        Console.BackgroundColor = line.BackgroundColor;
                    }
                    Console.WriteLine(line.Text);
                    if (line.OverwriteColour)
                        ResetColour();
                }
            }
        }

        static void ResetColour()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;
        }


    }
}
