
using System.Diagnostics;

internal class Program
{
    public static string Prefix = "AAAAAAAAAA:";    // <-- :를 붙일지 말지 연산을 빼기 위해서 Prefix는 Empty이거나 :로 끝나도록 설정
    private static void Main(string[] args)
    {
        int count = int.Parse(args[0]);
        Console.WriteLine($"Test Count : {count}");
        long standardDelta = 0;
        long standardWork = 0;
        long networkLatency = 5;

        using (StreamWriter writer = new StreamWriter("keys_just.txt"))
        {
            Stopwatch stopwatch1 = new Stopwatch();
            stopwatch1.Start();
            for (int i = 0; i < count; i++)
            {
                string key = args[0];
                writer.WriteLine(key);
            }
            stopwatch1.Stop();
            long work = stopwatch1.ElapsedMilliseconds + networkLatency * count;

            standardDelta = stopwatch1.ElapsedMilliseconds;
            standardWork = work;

            Console.WriteLine($"Key Just delta:{stopwatch1.ElapsedMilliseconds}, " +
                $"delta_per:{(double) stopwatch1.ElapsedMilliseconds / standardDelta * 100.0}, " +
                $"work_per:{(double) work / standardWork * 100.0}");
        }

        using (StreamWriter writer = new StreamWriter("keys_number.txt"))
        {
            Stopwatch stopwatch1 = new Stopwatch();
            stopwatch1.Start();
            for (int i = 0; i < count; i++)
            {
                string key = Random.Shared.Next(10000).ToString();
                writer.WriteLine(key);
            }
            stopwatch1.Stop();
            long work = stopwatch1.ElapsedMilliseconds + networkLatency * count;

            Console.WriteLine($"Number delta:{stopwatch1.ElapsedMilliseconds}, " +
                $"delta_per:{(double)stopwatch1.ElapsedMilliseconds / standardDelta * 100.0}, " +
                $"work_per:{(double)work / standardWork * 100.0}");
        }

        using (StreamWriter writer = new StreamWriter("keys_Prefix.txt"))
        {
            Stopwatch stopwatch1 = new Stopwatch();
            stopwatch1.Start();
            for (int i = 0; i < count; i++)
            {
                string key = MakeKey(Random.Shared.Next(10000).ToString());
                writer.WriteLine(key);
            }
            stopwatch1.Stop();
            long work = stopwatch1.ElapsedMilliseconds + networkLatency * count;

            Console.WriteLine($"Prefix delta:{stopwatch1.ElapsedMilliseconds}, " +
                $"delta_per:{(double)stopwatch1.ElapsedMilliseconds / standardDelta * 100.0}, " +
                $"work_per:{(double)work / standardWork * 100.0}");
        }

        Prefix = String.Empty;
        using (StreamWriter writer = new StreamWriter("keys_empty.txt"))
        {
            Stopwatch stopwatch1 = new Stopwatch();
            stopwatch1.Start();
            for (int i = 0; i < count; i++)
            {
                string key = MakeKey(Random.Shared.Next(10000).ToString());
                writer.WriteLine(key);
            }
            stopwatch1.Stop();
            long work = stopwatch1.ElapsedMilliseconds + networkLatency * count;

            Console.WriteLine($"Empty delta:{stopwatch1.ElapsedMilliseconds}, " +
                $"delta_per:{(double)stopwatch1.ElapsedMilliseconds / standardDelta * 100.0}, " +
                $"work_per:{(double)work / standardWork * 100.0}");
        }
    }

    public static string MakeKey(string key)
    {
        return Prefix + key;
    }
}