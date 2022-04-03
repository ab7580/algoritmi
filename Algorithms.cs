using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem2
{
    sealed class Algorithms
    {
        public static (List<string>, List<string>, int) Match(string A, string B)
        {
            var caf = new CardinalityAssignmentFactory(A);

            KeyList<int> solX = caf.GetX0();
            int solEval = 0;

            (var omegaA, var SA) = Generate(A);
            (var omegaB, var SB) = Generate(B);

            foreach (var Xpair in omegaA)
            {
                var X = Xpair.Key;
                int eval = caf.Eval(X);
                if (omegaB.ContainsKey(X) && eval > solEval)
                {
                    solX = X;
                    solEval = eval;
                }
            }

            return (SA[solX], SB[solX], solEval);
        }

        public static (Dictionary<KeyList<int>, bool>, Dictionary<KeyList<int>, List<string>>) Generate(string A)
        {
            int n = A.Length;
            int k = 2;
            CardinalityAssignmentFactory caf = new CardinalityAssignmentFactory(A);
            Dictionary<KeyList<int>, List<string>> S = new();
            List<Dictionary<KeyList<int>, bool>> omegas = new();

            Dictionary<KeyList<int>, bool> omega0 = new();
            omega0.Add(caf.GetX0(), true);

            omegas.Add(omega0);

            S.Add(caf.GetX0(), new());

            for (int i = 1; i <= n; i++)
            {
                Dictionary<KeyList<int>, bool> omegai = new();
                omegas.Add(omegai);

                for (int j = 1; j <= Math.Min(i, k); ++j)
                {
                    string w = A.Substring(i - j, j);
                    int positionW = caf.GetPosition(w);
                    foreach (KeyValuePair<KeyList<int>, bool> Xpair in omegas[i - j])
                    {
                        KeyList<int> X = Xpair.Key;
                        KeyList<int> tmpX = CardinalityAssignmentFactory.Increment(X, positionW);
                        if (!omegai.ContainsKey(tmpX))
                        {
                            omegai.Add(tmpX, true);
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
