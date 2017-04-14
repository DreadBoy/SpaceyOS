using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace SpaceyOS
{
    class ForceField : IForceFieldComp
    {
        public Dictionary<string, ISnipp> Snipps { get; set; } = new Dictionary<string, ISnipp>();
        public string Id { get; set; }

        ISpaceShip spaceShip;

        public ForceField(ISpaceShip spaceShip)
        {
            this.spaceShip = spaceShip;
            Id = StaticRandom.Rand().ToString("X").Substring(0, 4).ToLower();
        }

        public void MockHit()
        {
            foreach (var snipp in Snipps.Values)
            {
                //TODO what happenes if cast fails?
                ((IForceFieldSnipp)snipp).OnHit(5);
            }
        }
    }
}
