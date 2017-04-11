using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceyOS
{
    class SpaceShip : ISpaceShip
    {
        public List<IShipSystem> ShipSystem { get; set; } = new List<IShipSystem>();

        public SpaceShip()
        {
            ShipSystem.Add(new ForceField(this));
        }
    }
}
