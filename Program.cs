using Sem2;
using static Sem2.Algorithms;


Console.WriteLine("Starting...");
string A = "abbabbaab";
string B = "aaaabbbbb";
int k = 2;

(var a, var b) = Match(A, B, k);
//foreach (var x in a)
//{
//    Console.WriteLine($"ca: {x} - {b[x].ListToString()}");
//}
Console.WriteLine($"a: {a.ListToString()}");
Console.WriteLine($"b: {b.ListToString()}");
Console.ReadLine();

