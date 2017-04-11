using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IForceFieldAPI
    {
        ISpaceShip SpaceShip { get; set; }
        IForceField ForceField { get; set; }

        void Init(ISpaceShip spaceShip, IForceField forceField);

        void OnHit(int frequency);
    }
}
