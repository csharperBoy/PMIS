using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSM.WindowsServices.FileManager.Interfaces
{
    internal interface FileWriteInterface
    {
        [TestMethod]
        public static abstract bool Write<T>(string fileName, T fileContent) where T : class;
    }
}
