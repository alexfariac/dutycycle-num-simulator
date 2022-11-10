﻿using Assimetric_RPC;
using Assimetric_RPC.Methods;
using System.Diagnostics;

Stopwatch sw = Stopwatch.StartNew();

List<ScheduleMethod> scheduleMethods = getMethodList(10);
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

            return new() { dc50_s1, dc50_s2, dc50_s3, dc50_s4, dc50_s5 };
        break;

        case 10:
            ScheduleMethod dc10_s1 = new Searchlight(20);
            ScheduleMethod dc10_s2 = new Disco(17, 19);
            ScheduleMethod dc10_s3 = new Grid(19, 19);
            ScheduleMethod dc10_s4 = new Torus(14, 14);
            ScheduleMethod dc10_s5 = new UConnect(13);

            return new() { dc10_s1, dc10_s2, dc10_s3, dc10_s4, dc10_s5 };
        break;

        case 5:
            ScheduleMethod dc5_s1 = new Searchlight(40);
            ScheduleMethod dc5_s2 = new Disco(37, 41);
            ScheduleMethod dc5_s3 = new Grid(39, 39);
            ScheduleMethod dc5_s4 = new Torus(30, 30);
            ScheduleMethod dc5_s5 = new UConnect(29);

            return new() { dc5_s1, dc5_s2, dc5_s3, dc5_s4, dc5_s5 };
        break;

        default: 
            throw new NotImplementedException("Invalid DC percentage");
        break;
    }
}