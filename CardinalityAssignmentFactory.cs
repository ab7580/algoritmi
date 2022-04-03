using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem2
{
    sealed class CardinalityAssignmentFactory
    {
        private readonly KeyList<int> x0;
        private readonly Dictionary<string, int> words2positions;
        private readonly Dictionary<int, bool> positions2bools; // is there a duo (string of length 2) on position pos

        public CardinalityAssignmentFactory(string A)
        {
            //var alphabet = "abcdefghijklmnopqrstuvwxyz"[..l];
            var alphabet = A.Distinct().OrderBy(x => x).ToList();
            IEnumerable<string>? q = alphabet.Select(x => x.ToString());
            int size = 2;
            int ix = 0;

            words2positions = new();
            positions2bools = new();

            for (int i = 0; i < size - 1; i++)
            {
                foreach (var item in q)
                {
                    words2positions.Add(item, ix);
                    positions2bools.Add(ix, item.Length == 2);
                    ++ix;
                }
                q = q.SelectMany(x => alphabet, (x, y) => x + y);
            }
            foreach (var item in q)
            {
                words2positions.Add(item, ix);
                positions2bools.Add(ix, item.Length == 2);
                ++ix;
            }


            x0 = new();
            for (int i = 1; i <= GetSize(); i++)
            {
                x0.Add(0);
            }
        }

        public int GetSize()
        {
            return words2positions.Count;
        }

        public KeyList<int> GetX0()
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
            return words2positions[w];
        }

        public static KeyList<int> Increment(KeyList<int> x, int positionW)
        {
            //int value = Int32.Parse(x[positionW].ToString()) + 1;
            //return x[..positionW] + value.ToString() + x.Substring(positionW + 1, x.Length - positionW - 1);
            KeyList<int> newList = new();
            for (int i=0; i< x.Count; ++i)
            {
                if (i == positionW)
                {
                    newList.Add(x[i] + 1);
                    continue;
                }
                newList.Add(x[i]);
            }
            return newList;
        }

        public int Eval(KeyList<int> X)
        {
            int eval = 0;
            for (int i = 0; i<X.Count; i++)
            {
                if (positions2bools[i])
                {
                    eval += X[i];
                }
            }
            return eval;
        }
    }
}
