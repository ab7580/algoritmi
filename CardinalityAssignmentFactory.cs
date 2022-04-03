using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem2
{
    public class CardinalityAssignmentFactory
    {
        private readonly int l;
        private readonly int k;
        private readonly string x0;
        private readonly Dictionary<string, int> positions;

        public CardinalityAssignmentFactory(int l, int k)
        {
            this.l = l;
            this.k = k;

            var alphabet = "abcdefghijklmnopqrstuvwxyz"[..l];
            IEnumerable<string>? q = alphabet.Select(x => x.ToString());
            int size = k;
            int ix = 0;
            positions = new Dictionary<string, int>();

            for (int i = 0; i < size - 1; i++)
            {
                foreach (var item in q)
                {
                    positions.Add(item, ix);
                    ++ix;
                }
                q = q.SelectMany(x => alphabet, (x, y) => x + y);
            }
            foreach (var item in q)
            {
                positions.Add(item, ix);
                ++ix;
            }


            x0 = "";
            for (int i = 1; i <= GetSize(); i++)
            {
                x0 += "0";
            }
        }

        public int GetSize()
        {
            return positions.Count;
        }

        public string GetX0()
        {
            return x0;
        }

        public int GetPosition(string w)
        {
            //int pos = positions[w];
            //string ca = "";
            //for (int i=0; i< GetSize(); ++i)
            //{
            //    if (pos == i)
            //    {
            //        ca += "1";
            //        continue;
            //    }
            //    ca += "0";
            //}
            //return ca;
            return positions[w];
        }

        public static string Increment(string x, int positionW)
        {
            int value = Int32.Parse(x.Substring(positionW, 1)) + 1;
            return x[..positionW] + value.ToString() + x.Substring(positionW + 1, x.Length - positionW - 1);
        }
    }
}
