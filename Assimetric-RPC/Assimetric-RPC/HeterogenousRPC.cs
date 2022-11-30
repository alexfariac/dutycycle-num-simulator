using Assimetric_RPC.Methods;
using Assimetric_RPC.Utils;
using System.Collections.Generic;
using System.Numerics;

namespace Assimetric_RPC
{
    public static class HeterogenousRPC
    {
        private const int ROUND_PRECISION = 3;
        private static int lcm;
        private static int gcf;

        public static void NDTMetrics(ScheduleMethod schedule1, ScheduleMethod schedule2)
        {
            ulong discoveryTimeAverage = 0;
            int maxDiscoveryTime = 0;
            uint iterations = 0;

            lcm = MathUtils.Lcm(schedule1.ScheduleSize, schedule2.ScheduleSize);
            gcf = MathUtils.gcf(schedule1.ScheduleSize, schedule2.ScheduleSize);

            for (int startIndex1 = 0; startIndex1 < lcm; startIndex1++)
            {
                ulong discoveryTimeSum = 0;

                for (int startIndex2 = startIndex1; startIndex2 < lcm; startIndex2++)
                {
                    int discoveryTime = DiscoveryTimeActiveSlots(schedule1, schedule2, startIndex1, startIndex2);
                    if (discoveryTime == -1)
                    {
                        Console.WriteLine($"S1: {schedule1.Name}; S2: {schedule2.Name}; gcf: {gcf}; DC1: {Math.Round(schedule1.DutyCyclePerc, ROUND_PRECISION)}; DC2: {Math.Round(schedule2.DutyCyclePerc, ROUND_PRECISION)}; AVG-NDT: inf; MAX-NDT: inf");
                        return;
                    }

                    discoveryTimeSum += (uint)discoveryTime;
                    maxDiscoveryTime = Math.Max(maxDiscoveryTime, discoveryTime);
                    iterations++;
                }

                discoveryTimeAverage += discoveryTimeSum;

            }

            // Console.WriteLine($"DISCOVERY SUM: {discoveryTimeSum}, ITERATIONS: {iterations}");
            double averageNdt = discoveryTimeAverage / iterations;
            Console.WriteLine($"S1: {schedule1.Name}; S2: {schedule2.Name}; lcm: {lcm}; gcf: {gcf}; DC1: {Math.Round(schedule1.DutyCyclePerc, ROUND_PRECISION)}; DC2: {Math.Round(schedule2.DutyCyclePerc, ROUND_PRECISION)}; AVG-NDT: {averageNdt}; MAX-NDT: {maxDiscoveryTime}; ITERATIONS: {iterations}");
        }

        public static int DiscoveryTimeActiveSlots(ScheduleMethod schedule1, ScheduleMethod schedule2, int startIndex1 = 0, int startIndex2 = 0)
        {
            int t = 0;
            while (!schedule1.Schedule[startIndex1 % schedule1.ScheduleSize] || !schedule2.Schedule[startIndex2 % schedule2.ScheduleSize])
            {
                int slotsToAdvance = Math.Min(schedule1.NextActiveSlots[startIndex1 % schedule1.ScheduleSize], schedule2.NextActiveSlots[startIndex2 % schedule2.ScheduleSize]);

                t += slotsToAdvance;
                startIndex1 += slotsToAdvance;
                startIndex2 += slotsToAdvance;

                if(t > lcm)
                {
                    return -1;
                }
            }

            return t;
        }
    }
}
