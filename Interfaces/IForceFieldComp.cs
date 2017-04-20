using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IForceFieldComp : IComp
    {
        void SetFrequency(int frequency);

        void GotHit(int frequency);
    }
}
