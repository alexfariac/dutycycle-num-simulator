using Assimetric_RPC;
using Assimetric_RPC.Methods;
using Assimetric_RPC.Utils;
using System.Diagnostics;
using System.Text;

// ScheduleMethod s1 = new UConnect(21);
// ScheduleMethod s2 = new Disco(137,199);

Stopwatch sw = Stopwatch.StartNew();

List<int> primesList = MathUtils.getPrimes(50);
Console.WriteLine($"Primes generated in {sw.ElapsedMilliseconds} ms.");

for (int i = 0; i < primesList.Count; i++)
{
    ScheduleMethod s1 = new UConnect(primesList[i]);
    for (int j = 0; j < primesList.Count; j++)
    {
        for (int k = j; k < primesList.Count; k++)
        {
            ScheduleMethod s2 = new Disco(primesList[j], primesList[k]);

            HeterogenousRPC.NDTMetrics(s1, s2);
        }
    }
}

Console.WriteLine($"Simulation executed in {sw.Elapsed.TotalSeconds} seconds.");


static void print(List<int> bd, int offset = 0)
{
    StringBuilder sb = new ();
    for (int i = 0; i < bd.Count; i++)
    {
        int index = (offset + i < bd.Count) ? offset + i : (offset + i) % bd.Count;
        sb.Append(bd[index]);
    }
    Console.WriteLine(sb.ToString());
}