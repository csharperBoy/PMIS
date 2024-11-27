using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSM.WindowsServices.FileManager.Interfaces
{
    internal interface FileReadInterface
    {
        [TestMethod]
        public static abstract object Read(string fileName);
    }
}
