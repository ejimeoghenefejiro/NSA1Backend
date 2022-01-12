using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSA1.Core.Utilities
{
    public interface IImageProcessor
    {

        byte[] ReadFile(string sPath);
        byte[] ReadAllBytes(Stream source);


    }
}
