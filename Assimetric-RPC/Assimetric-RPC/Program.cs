using Assimetric_RPC;
using Assimetric_RPC.Methods;
using System.Text;

ScheduleMethod s1 = new BlockDesign(21);
ScheduleMethod s2 = new BlockDesign(7);

HeterogenousRPC.NDTMetrics(s1, s2);


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