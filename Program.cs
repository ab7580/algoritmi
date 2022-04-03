using Sem2;
using static Sem2.Algorithms;


Console.WriteLine("Starting...");
string A = "abbabbaab";
string B = "aaaabbbbb";

var eval = new Evaluation("output.txt", 30, 5, 1);
eval.Start();
Console.ReadLine();

