using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ISnipp
    {
        ISpaceShip SpaceShip { get; set; }
        IComp BaseComp { get; set; }
    }
}
