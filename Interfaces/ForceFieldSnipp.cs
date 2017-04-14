using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public class ForceFieldSnipp : IForceFieldSnipp
    {
        public ISpaceShip SpaceShip { get; set; }
        public IComp Comp { get; set; }

        public virtual void OnHit(int frequency)
        {

        }
    }
}
