using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem2
{
    static class Extensions
    {
        public static string ListToString(this List<string> list)
        {
            string s = "";
            foreach (string item in list)
            {
                s += item + ", ";
            }
            return s;
        }
    }
}
