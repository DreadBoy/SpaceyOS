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
        public List<IComp> ShipComps { get; set; } = new List<IComp>();

        public SpaceShip()
        {

        }
    }
}
