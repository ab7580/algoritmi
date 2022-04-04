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

            List<int> solX = caf.GetX0();
            int solEval = 0;

            (var omegaA, var SA) = Generate(A);
            (var omegaB, var SB) = Generate(B);

            foreach (var Xpair in omegaA)
            {
                var X = Xpair.Value;
                int eval = caf.Eval(X);
                if (omegaB.ContainsKey(X.Hash()) && eval > solEval)
                {
                    solX = X;
                    solEval = eval;
                }
            }

            return (SA[solX.Hash()], SB[solX.Hash()], solEval);
        }

        public static (Dictionary<string, List<int>>, Dictionary<string, List<string>>) Generate(string A)
        {
            List<int> a = new List<int> { 6, 3, 1, 0, 1, 0, 1, 1, 1, 0, 0, 2 };
            List<int> b = new List<int> { 6, 4, 3, 1, 1, 0, 1, 1, 1, 0, 0, 1 };
            int n = A.Length;
            int k = 2;
            CardinalityAssignmentFactory caf = new CardinalityAssignmentFactory(A);
            Dictionary<string, List<string>> S = new();
            Dictionary<string, List<int>> sanity = new();
            List<Dictionary<string, List<int>>> omegas = new();

            Dictionary<string, List<int>> omega0 = new();
            omega0.Add(caf.GetX0().Hash(), caf.GetX0());

            omegas.Add(omega0);

            S.Add(caf.GetX0().Hash(), new());

            for (int i = 1; i <= n; i++)
            {
                Dictionary<string, List<int>> omegai = new();
                omegas.Add(omegai);

                for (int j = 1; j <= Math.Min(i, k); ++j)
                {
                    string w = A.Substring(i - j, j);
                    int positionW = caf.GetPosition(w);
                    foreach (var Xpair in omegas[i - j])
                    {
                        List<int> X = Xpair.Value;
                        List<int> tmpX = CardinalityAssignmentFactory.Increment(X, positionW);
                        string tmpXHash = tmpX.Hash();
                        if (!omegai.ContainsKey(tmpXHash))
                        {
                            omegai.Add(tmpXHash, tmpX);
                            var tmpList = new List<string>();
                            tmpList.AddRange(S[X.Hash()]);
                            tmpList.Add(w);
                            S.Add(tmpXHash, tmpList);
                            sanity.Add(tmpXHash, tmpX);
                        }
                    }
                }
            }
            return (omegas[^1], S);
        }
    }
}
