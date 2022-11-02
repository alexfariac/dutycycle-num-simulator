using Assimetric_RPC.Methods;
using Assimetric_RPC.Utils;

namespace Assimetric_RPC
{
    public static class HeterogenousRPC
    {
        public static void NDTMetrics(ScheduleMethod schedule1, ScheduleMethod schedule2)
        {
            int discoveryTimeSum = 0;
            int maxDiscoveryTime = 0;

            int lcm = MathUtils.Lcm(schedule1.ScheduleSize, schedule2.ScheduleSize);

            for (int relativeOffset = 0; relativeOffset < lcm; relativeOffset++)
            {
                int discoveryTime = DiscoveryTime(schedule1, schedule2, relativeOffset);
                discoveryTimeSum += discoveryTime;
                maxDiscoveryTime = Math.Max(maxDiscoveryTime, discoveryTime);
            }

            var averageNdt = discoveryTimeSum / lcm;
            Console.WriteLine($"DC1: {schedule1.DutyCyclePerc}, DC2: {schedule2.DutyCyclePerc}, AVG-NDT: {averageNdt}, MAX-NDT: {maxDiscoveryTime}");
        }

        public static int DiscoveryTime(ScheduleMethod schedule1, ScheduleMethod schedule2, int relativeOffset = 0)
        {
            for(int i = 0; i < MathUtils.Lcm(schedule1.ScheduleSize, schedule2.ScheduleSize); i++)
            {
                int index1 = i % schedule1.ScheduleSize;
                int index2 = (i + relativeOffset) % schedule2.ScheduleSize;

                if(schedule1.Schedule[index1] && schedule2.Schedule[index2])
                {
                    return i;
                }
            }

            return -1;
        }

    }
}
