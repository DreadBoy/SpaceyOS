using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ISpaceShip
    {
        List<IShipSystem> ShipSystem { get; set; }
    }
}
