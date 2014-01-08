using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Other
{
    class Program
    {
        static void Main(string[] args)
        {
            string testAppDomain = AppDomain.CurrentDomain.GetData("Exam70536") as string;

            Console.WriteLine(testAppDomain);

            AppDomain.CurrentDomain.SetData("Exam70536", "OK");
        }
    }
}
