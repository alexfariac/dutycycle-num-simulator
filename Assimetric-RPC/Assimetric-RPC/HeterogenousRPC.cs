﻿using Assimetric_RPC.Methods;
using Assimetric_RPC.Utils;
using System.Collections.Generic;
using System.Numerics;

namespace Assimetric_RPC
{
    public static class HeterogenousRPC
    {
        private const int ROUND_PRECISION = 3;

        public static void NDTMetrics(ScheduleMethod schedule1, ScheduleMethod schedule2)
        {
            ulong discoveryTimeAverage = 0;
            int maxDiscoveryTime = 0;
            uint iterations = 0;

            int lcm = MathUtils.Lcm(schedule1.ScheduleSize, schedule2.ScheduleSize);
            int gcf = MathUtils.gcf(schedule1.ScheduleSize, schedule2.ScheduleSize);

            for (int startIndex1 = 0; startIndex1 < lcm; startIndex1++)
            {
                ulong discoveryTimeSum = 0;

                for (int startIndex2 = startIndex1; startIndex2 < lcm; startIndex2++)
                {
                    int discoveryTime = DiscoveryTime(schedule1, schedule2, startIndex1, startIndex2, lcm);
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
            int closestActiveSlotIndex1 = schedule1.FindNextGreatestActiveSlot(startIndex1);
            int closestActiveSlotIndex2 = schedule2.FindNextGreatestActiveSlot(startIndex2);

            int slotsUntilNextActiveSlots1 = (closestActiveSlotIndex1 - startIndex1) >= 0 ? (closestActiveSlotIndex1 - startIndex1) : schedule1.ScheduleSize - startIndex1 + closestActiveSlotIndex1;
            int slotsUntilNextActiveSlots2 = (closestActiveSlotIndex2 - startIndex2) >= 0 ? (closestActiveSlotIndex2 - startIndex2) : schedule2.ScheduleSize - startIndex2 + closestActiveSlotIndex2;

            if (slotsUntilNextActiveSlots1 == slotsUntilNextActiveSlots2)
            {
                return slotsUntilNextActiveSlots1;
            }

            return int.MaxValue;
        }

        public static int DiscoveryTime(ScheduleMethod schedule1, ScheduleMethod schedule2, int startIndex1 = 0, int startIndex2 = 0, int lcm = -1)
        {
            int schedule_lcm = lcm == -1 ? MathUtils.Lcm(schedule1.ScheduleSize, schedule2.ScheduleSize) : lcm;
            for (int i = 0; i < schedule_lcm; i++)
            {
                int index1 = (i + startIndex1) % schedule1.ScheduleSize;
                int index2 = (i + startIndex2) % schedule2.ScheduleSize;

                if (schedule1.Schedule[index1] && schedule2.Schedule[index2])
                {
                    return i;
                }
            }

            return -1;
        }

        public static List<int> CommonActiveSlots(ScheduleMethod s1, ScheduleMethod s2, int s1InitialSlot = 0, int s2InitialSlot = 0, int lcm = -1)
        {
            int schedule_lcm = lcm == -1 ? MathUtils.Lcm(s1.ScheduleSize, s2.ScheduleSize) : lcm;

            List<int> intersectionSlots = new ();

            for(int i = 0; i < schedule_lcm; i++)
            {
                int index1 = (i + s1InitialSlot) % s1.ScheduleSize;
                int index2 = (i + s2InitialSlot) % s2.ScheduleSize;

                if(s1.Schedule[index1] && s2.Schedule[index2])
                {
                    intersectionSlots.Add(1);
                }
                else
                {
                    intersectionSlots.Add(0);
                }
            }

            return intersectionSlots;
        }

    }
}
