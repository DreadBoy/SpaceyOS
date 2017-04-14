using Interfaces;
using System;

namespace SpaceyOS
{
    class Program
    {
        static void Main(string[] args)
        {
            SpaceShip ship = new SpaceShip();
            ship.ShipComps.Add(new ForceField(ship));
            //ship.ShipComps.Add(new ForceField(ship));

            SpaceyOS spaceyOS = new SpaceyOS(ship);
            ResetColour();

            //AssemblyCompiler compiler = new AssemblyCompiler(new[] { @"ForceFieldAPI.csx" });
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


/*
 * TODO 
 *
 * 'system' pokaže informacije o OS in ladji
 * 'system comps' pokaže vse komponente na ladji
 * 'comp <id>' pokaže infomacije o komponenti, tudi pripete snippe
 * 'comp <id> attach <id_snipp>
 * 'comp <id> deattach <id_snipp>
 */
