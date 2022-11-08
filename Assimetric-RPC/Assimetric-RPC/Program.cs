using Assimetric_RPC;
using Assimetric_RPC.Methods;
using Assimetric_RPC.Utils;
using System.Diagnostics;
using System.Text;

Stopwatch sw = Stopwatch.StartNew();

List<int> primesList = MathUtils.getPrimes(15);
Console.WriteLine($"Primes generated in {sw.ElapsedMilliseconds} ms.");

/*
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
*/

/*
int scheduleLimit = 50;
for(int i = 0; i < primesList.Count; i++)
{
    for(int j = i; j < primesList.Count; j++)
    {
        if (j == i) continue;
        ScheduleMethod s1 = new Disco(primesList[i], primesList[j]);

        for(int k = 2; k <= scheduleLimit; k++)
        {
            ScheduleMethod s2 = new Grid(k, k);

            HeterogenousRPC.NDTMetrics(s1, s2);
        }
    }
}
*/

for(int i = 0; i < primesList.Count; i++)
{
    ScheduleMethod s1 = new UConnect(primesList[i]);

    for(int j = 4; j <= 20; j++)
    {
        ScheduleMethod s2 = new Searchlight(j);

        HeterogenousRPC.NDTMetrics(s1, s2);
    }
}


Console.WriteLine($"Simulation executed in {sw.Elapsed.TotalSeconds} seconds.");


static void print(List<bool> bd, int offset = 0)
{
    StringBuilder sb = new();
    for (int i = 0; i < bd.Count; i++)
    {
        int index = (offset + i < bd.Count) ? offset + i : (offset + i) % bd.Count;
        sb.Append(bd[index] ? 1 : 0);
    }
    Console.WriteLine(sb.ToString());
}