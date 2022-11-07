using Assimetric_RPC;
using Assimetric_RPC.Methods;
using Assimetric_RPC.Utils;
using System.Diagnostics;

Stopwatch sw = Stopwatch.StartNew();

List<int> primesList = MathUtils.getPrimes(15);
Console.WriteLine($"Primes generated in {sw.ElapsedMilliseconds} ms.");

for (int i = 0; i < primesList.Count; i++)
{
    ScheduleMethod s1 = new UConnect(primesList[i]);
    for (int j = 0; j < primesList.Count; j++)
    {
        for (int k = j; k < primesList.Count; k++)
        {
            if (j == k) continue;
            ScheduleMethod s2 = new Disco(primesList[j], primesList[k]);

            HeterogenousRPC.NDTMetrics(s1, s2);
        }
    }
}


Console.WriteLine($"Simulation executed in {sw.Elapsed.TotalSeconds} seconds.");