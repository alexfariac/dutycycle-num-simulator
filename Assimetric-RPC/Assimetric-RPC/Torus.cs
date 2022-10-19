using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimetric_RPC
{
    public class Torus
    {
        public List<int> Schedule { get; }

        public double DutyCyclePerc { get; }

        public Torus(int n, int m)
        {
            List<int> s = new();

            int activeSlots = 0;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    int r = (j == 0 || (i == j && i < m/2+1) ) ? 1 : 0;
                    s.Add(r);
                    activeSlots++;
                }
            }

            Schedule = s;
            DutyCyclePerc = 100 * (double)activeSlots/(n*m);
        }
    }
}
