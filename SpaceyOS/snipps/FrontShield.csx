using Interfaces;
using System;

class FrontShield : ForceFieldSnipp
{
    public override void OnHit(int frequency)
    {
		Comp.SetFrequency(600);
        Console.WriteLine("Setting frequency to 600");
    }
}