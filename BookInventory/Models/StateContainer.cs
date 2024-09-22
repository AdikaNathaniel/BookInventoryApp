using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookInventoryApp
{
    public class StateContainer
    {
         public readonly Dictionary<int,object> ObjectTunnel = new();
    }
}