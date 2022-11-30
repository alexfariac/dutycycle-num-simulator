using Assimetric_RPC;
using Assimetric_RPC.Methods;
using System.Diagnostics;


Stopwatch sw = Stopwatch.StartNew();


List<ScheduleMethod> scheduleMethods = getMethodList(int.Parse(args[1]));
Console.WriteLine(Methods_DC(scheduleMethods));

for (int i = 0; i < scheduleMethods.Count; i++)
{
    for (int j = i; j < scheduleMethods.Count; j++)
    {
        HeterogenousRPC.NDTMetrics(scheduleMethods[i], scheduleMethods[j]);
    }
}

Console.WriteLine($"Simulation executed in {sw.Elapsed.TotalSeconds} seconds.");


string Methods_DC(List<ScheduleMethod> methods)
{
    return String.Join(" | ", methods);
}

List<ScheduleMethod> getMethodList(int dcPerc)
{
    switch (dcPerc)
    {
        case 50:
            ScheduleMethod dc50_s1 = new Searchlight(5);
            ScheduleMethod dc50_s2 = new Disco(5, 7);
            ScheduleMethod dc50_s3 = new Grid(3, 3);
            ScheduleMethod dc50_s4 = new Torus(3, 3);
            ScheduleMethod dc50_s5 = new UConnect(3);
            ScheduleMethod dc50_s6 = new BlockDesign(7);

            return new() { dc50_s1, dc50_s2, dc50_s3, dc50_s4, dc50_s5, dc50_s6 };
        break;

        case 10:
            ScheduleMethod dc10_s1 = new Searchlight(20);
            ScheduleMethod dc10_s2 = new Disco(17, 19);
            ScheduleMethod dc10_s3 = new Grid(19, 19);
            ScheduleMethod dc10_s4 = new Torus(15, 15);
            ScheduleMethod dc10_s5 = new UConnect(13);
            ScheduleMethod dc10_s6 = new BlockDesign(91);

            return new() { dc10_s1, dc10_s2, dc10_s3, dc10_s4, dc10_s5, dc10_s6 };
        break;

        case 5:
            ScheduleMethod dc5_s1 = new Searchlight(40);
            ScheduleMethod dc5_s2 = new Disco(37, 41);
            ScheduleMethod dc5_s3 = new Grid(39, 39);
            ScheduleMethod dc5_s4 = new Torus(30, 30);
            ScheduleMethod dc5_s5 = new UConnect(29);
            ScheduleMethod dc5_s6 = new BlockDesign(381);

            return new() { dc5_s1, dc5_s2, dc5_s3, dc5_s4, dc5_s5, dc5_s6 };
        break;

        case 2:
            ScheduleMethod dc2_s1 = new Searchlight(100);
            ScheduleMethod dc2_s2 = new Disco(97, 101);
            ScheduleMethod dc2_s3 = new Grid(99, 99);
            ScheduleMethod dc2_s4 = new Torus(75, 75);
            ScheduleMethod dc2_s5 = new UConnect(73);
            ScheduleMethod dc2_s6 = new BlockDesign(2451);

            return new() { dc2_s1, dc2_s2, dc2_s3, dc2_s4, dc2_s5, dc2_s6 };
        break;

        case 1:
            ScheduleMethod dc1_s1 = new Searchlight(200);
            ScheduleMethod dc1_s2 = new Disco(197, 199);
            ScheduleMethod dc1_s3 = new Grid(199, 199);
            ScheduleMethod dc1_s4 = new Torus(150, 150);
            ScheduleMethod dc1_s5 = new UConnect(149);
            ScheduleMethod dc1_s6 = new BlockDesign(10303);

            return new() { dc1_s1, dc1_s2, dc1_s3, dc1_s4, dc1_s5, dc1_s6 };
        break;

        default: 
            throw new NotImplementedException("Invalid DC percentage");
        break;
    }
}