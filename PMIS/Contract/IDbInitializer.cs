using PMIS.Bases;
using PMIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.Contract
{
    internal interface IDbInitializer : IInitializer
    {
        void Create();
        void SetData();
    }
}
