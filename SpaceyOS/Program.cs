using Interfaces;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SpaceyOS
{
    class Program
    {
        static void Main(string[] args)
        {
            Terminal terminal = new Terminal();
            ResetColour();

            AssemblyCompiler compiler = new AssemblyCompiler(new[] { @"ForceFieldAPI.csx" });


            SpaceShip ship = new SpaceShip();
            var ff = new ForceField(ship);
            var ffa = (IForceFieldAPI)compiler.CreateInstance("ForceFieldAPI");
            ffa.Init(ship, ff);
            ff.API = ffa;

            ship.ShipSystem.Add(new ForceField(ship));

            ff.MockHit();

            while (!terminal.Exit)
            {
                Console.Write($"{terminal.WorkingDirectory}: ");
                var input = Console.ReadLine();
                var ret = terminal.ReadLine(input);
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

    class Api : IForceFieldAPI
    {
        public ISpaceShip SpaceShip { get; set; }
        public IForceField ForceField { get; set; }

        public Api(ISpaceShip spaceShip, IForceField forceField)
        {
            Init(spaceShip, forceField);
        }

        public void OnHit(int frequency)
        {
            Console.WriteLine("I'm hit");
        }

        public void Init(ISpaceShip spaceShip, IForceField forceField)
        {
            SpaceShip = spaceShip;
            ForceField = forceField;
        }
    }
}
