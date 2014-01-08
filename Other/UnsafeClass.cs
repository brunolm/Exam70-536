using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Other
{
    public class UnsafeClass
    {
        public void PretendToBeANiceMethod(string text)
        {
            File.WriteAllText("evil.txt", text);
            
            // Some evil stuff you could do:
            // write on c:\windows
            // delete files
            // stop services
            // change registry
        }
    }
}
