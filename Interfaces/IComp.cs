using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IComp
    {
        string Id { get; set; }
        Dictionary<string, ISnipp> Snipps { get; set; }
    }
}
