using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimetric_RPC
{
    public class Disco
    {
        public List<int> Schedule { get; }

        public double DutyCyclePerc { get; }

        public Disco(int p, int q)
        {
            Schedule = new();

            int activeSlots = 0;

            for (int i = 0; i < p * q; i++)
            {
                if (i % p == 0 || i% q == 0)
                {
                    Schedule.Add(1);
                    activeSlots++;
                }
                else
                {
                    Schedule.Add(0);
                }
            }

            DutyCyclePerc = 100 * (double)activeSlots / (p * q);
        }
    }
}
