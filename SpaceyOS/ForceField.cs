using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace SpaceyOS
{
    class ForceField : IForceField
    {
        public IForceFieldAPI API { get; set; }
        ISpaceShip spaceShip;

        public ForceField (ISpaceShip spaceShip)
        {
            this.spaceShip = spaceShip;
        }

        public void MockHit()
        {
            API.OnHit(5);
        }
    }
}
