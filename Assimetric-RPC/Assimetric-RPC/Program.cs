using Assimetric_RPC;
using System.Text;

var s1 = new UConnect(107);
var s2 = new UConnect(107);

List<int> schedule1 = s1.Schedule; 
List<int> schedule2 = s2.Schedule;

for (int i = 0; i < schedule1.Count; i++)
{
    for (int j = i; j < schedule2.Count; j++)
    {
        int encounterIndex = hasEncounter(schedule1, schedule2, i, j);

        if (encounterIndex == -1)
        {
            Console.WriteLine($"No encounter oportunity at schedule1 offset:{i} and schedule2 offset:{j}.");
            Console.WriteLine(String.Empty);
        }
    }
}


static int hasEncounter(List<int> schedule1, List<int> schedule2, int offset1 = 0, int offset2 = 0)
{
    for(int i = 0; i < lcm(schedule1.Count, schedule2.Count); i++)
    {
        int index1 = offsetIndex(schedule1.Count, i + offset1);
        int index2 = offsetIndex(schedule2.Count, i + offset2);

        if(schedule1[index1] == 1 && schedule2[index2] == 1)
        {
            return i;
        }
    }

    return -1;
}

static int gcf(int a, int b)
{
    while (b != 0)
    {
        int temp = b;
        b = a % b;
        a = temp;
    }
    return a;
}

static int lcm(int a, int b)
{
    return (a / gcf(a, b)) * b;
}

static int offsetIndex(int size, int offset = 0)
{
    return offset < size? offset : offset % size;
}

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