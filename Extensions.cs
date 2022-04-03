using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem2
{
    static class Extensions
    {
        private static Random rng = new Random();
        public static string ListToString(this List<string> list)
        {
            string s = "";
            foreach (string item in list)
            {
                s += item + ", ";
            }
            return s;
        }
        public static string Shuffle(this string str)
        {
            char[] array = str.ToCharArray();
            int n = array.Length;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                var value = array[k];
                array[k] = array[n];
                array[n] = value;
            }
            return new string(array);
        }
    }
}
