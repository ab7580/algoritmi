using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem2
{
    public class Evaluation
    {
        private readonly string resultPath;
        private readonly int maxWordLength;
        private readonly int maxAlphabetSize;
        private readonly int repsForAverage;
        private readonly Random random;


        public Evaluation(string resultPath, int maxWordLength, int maxAlphabetSize, int repsForAverage)
        {
            this.resultPath = resultPath;
            this.maxWordLength = maxWordLength;
            this.maxAlphabetSize = maxAlphabetSize;
            this.repsForAverage = repsForAverage;
            random = new Random();
        }

        public void Start()
        {
            File.Delete(resultPath);
            File.AppendAllText(resultPath, "n;l;time;eval;omegaASize;omegaBSize;SASize;SBSize\n");
            Stopwatch stopWatch = new Stopwatch();
            for (int l = 2; l <= maxAlphabetSize; ++l)
            {
                for (int n = 3; n <= maxWordLength; ++n)
                {
                    List<long> ticks = new();
                    List<int> evals = new();
                    List<int> omegaAs = new();
                    List<int> omegaBs = new();
                    List<int> SAs = new();
                    List<int> SBs = new();
                    for (int reps = 0; reps < repsForAverage; ++reps)
                    {
                        string a = GenerateWord(n, l);
                        string b = a.Shuffle();

                        stopWatch.Start();
                        (var _, var _, int eval, int omegaASize, int SASize, int omegaBSize, int SBSize) = Algorithms.Match(a, b);
                        stopWatch.Stop();
                        evals.Add(eval);
                        omegaAs.Add(omegaASize);
                        omegaBs.Add(omegaBSize);
                        SAs.Add(SASize);
                        SBs.Add(SBSize);
                        ticks.Add(stopWatch.ElapsedTicks);
                    }
                    double avg = ticks.Average();
                    double avgEval = evals.Average();
                    double avgOmegaA = omegaAs.Average();
                    double avgOmegaB = omegaBs.Average();
                    double avgSAs = SAs.Average();
                    double avgSBs = SBs.Average();
                    TimeSpan ts = new TimeSpan((long)avg);

                    // Format and display the TimeSpan value.
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                    string e = Math.Round(avgEval, 1).ToString(CultureInfo.InvariantCulture);
                    Console.WriteLine($"n = {n} l = {l}: RunTime " + elapsedTime + $" | evals = {e} | omegaA = {avgOmegaA} | " +
                        $"omegaB = {avgOmegaB} | SAs = {avgSAs} | SBs = {avgSBs}");
                    File.AppendAllText(resultPath, $"{n};{l};{elapsedTime};{e};{avgOmegaA};{avgOmegaB};{avgSAs};{avgSBs}\n");
                    stopWatch.Reset();
                }
            }
        }

        private string GenerateWord(int n, int l)
        {
            var alphabet = "abcdefghijklmnopqrstuvwxyz"[..l];
            string word = "";
            for (int i = 0; i < n; ++i)
            {
                word += alphabet[random.Next(0, alphabet.Length)];
            }
            return word;
        }
    }
}
