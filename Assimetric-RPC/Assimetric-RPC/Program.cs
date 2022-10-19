// See https://aka.ms/new-console-template for more information
using Assimetric_RPC;
using System.Text;

//List<int> bd7 = new List<int> { 1, 1, 0, 1, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0 };
//List<int> bd21 = new List<int> { 1, 0, 0, 1, 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

var s1 = new Grid(120, 120);
var s2 = new Disco(109, 113);

List<int> schedule1 = s1.Schedule; 
List<int> schedule2 = s2.Schedule; 

Console.WriteLine(s1.DutyCyclePerc);
Console.WriteLine(s2.DutyCyclePerc);

for (int i = 0; i < schedule1.Count; i++)
{
    for (int j = i; j < schedule2.Count; j++)
    {
        bool b_hasEncounter = hasEncounter(schedule1, schedule2, i, j);

        if (!b_hasEncounter)
        {
            Console.WriteLine($"No encounter oportunity at schedule1 offset:{i} and schedule2 offset:{j}.");
            //print(schedule1, i);
            //print(schedule2, j);
            Console.WriteLine(String.Empty);
        }
    }
}


static bool hasEncounter(List<int> schedule1, List<int> schedule2, int offset1 = 0, int offset2 = 0)
{
    int i_lcm = lcm(schedule1.Count, schedule2.Count);
    for(int i = 0; i < i_lcm; i++)
    {
        int index1 = offsetIndex(schedule1.Count, i + offset1);
        int index2 = offsetIndex(schedule2.Count, i + offset2);

        if(schedule1[index1] == 1 && schedule2[index2] == 1)
        {
            return true;
        }
    }

    return false;
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