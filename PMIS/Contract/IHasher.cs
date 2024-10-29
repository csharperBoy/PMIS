using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.Contract
{
    internal interface IHasher
    {
        void Hash(string text);
    }
}
