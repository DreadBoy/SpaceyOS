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
        public IComp BaseComp { get; set; }

        public IForceFieldComp Comp
        {
            get { return (IForceFieldComp) BaseComp; }
            private set { }
        }

        public virtual void OnHit(int frequency)
        {

        }
    }
}
