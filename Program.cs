using Sem2;

string A = "abbabbaab";
int k = 2;
(var a, var b) = Generate(A, k);
foreach (var x in a)
{
    Console.WriteLine($"ca: {x} - {b[x].ListToString()}");
}
Console.ReadLine();

(SortedSet<string>, Dictionary<string, List<string>>) Generate(string A, int k)
{
    int n = A.Length;
    int l = A.Distinct().Count();
    CardinalityAssignmentFactory caf = new CardinalityAssignmentFactory(l, k);
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

