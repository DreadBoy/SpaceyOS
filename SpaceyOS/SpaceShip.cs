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

        public void GotHit()
        {
            foreach (var comp in ShipComps)
            {
                if (!(comp is IForceFieldComp)) continue;
                var forceField = (IForceFieldComp) comp;
                forceField.GotHit(500);
            }
        }
    }
}
