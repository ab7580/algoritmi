using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem2
{
    public static class Algorithms
    {
        public static (List<string>, List<string>) Match(string A, string B, int k)
        {
            var caf = new CardinalityAssignmentFactory(A, k);

            string solX = caf.GetX0();

            (var omegaA, var SA) = Generate(A, k);
            (var omegaB, var SB) = Generate(B, k);

            foreach (string X in omegaA)
            {
                if (omegaB.Contains(X) && caf.Eval(X) > caf.Eval(solX))
                    solX = X;
            }

            return (SA[solX], SB[solX]);
        }

        public static (SortedSet<string>, Dictionary<string, List<string>>) Generate(string A, int k)
        {
            int n = A.Length;
            CardinalityAssignmentFactory caf = new CardinalityAssignmentFactory(A, k);
            Dictionary<string, List<string>> S = new();
            List<SortedSet<string>> omegas = new();

            SortedSet<string> omega0 = new();
            omega0.Add(caf.GetX0());

            omegas.Add(omega0);

            S.Add(caf.GetX0(), new());

            for (int i = 1; i <= n; i++)
            {
                SortedSet<string> omegai = new();
                omegas.Add(omegai);

                for (int j = 1; j <= Math.Min(i, k); ++j)
                {
                    string w = A.Substring(i - j, j);
                    int positionW = caf.GetPosition(w);
                    foreach (string X in omegas[i - j])
                    {
                        string tmpX = CardinalityAssignmentFactory.Increment(X, positionW);
                        if (!omegai.Contains(tmpX))
                        {
                            omegai.Add(tmpX);
                            var tmpList = new List<string>();
                            tmpList.AddRange(S[X]);
                            tmpList.Add(w);
                            S.Add(tmpX, tmpList);
                        }
                    }
                }
            }
            return (omegas[^1], S);
        }
    }
}
