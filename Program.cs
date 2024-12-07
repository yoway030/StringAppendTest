
using System.Diagnostics;

internal class Program
{
    public static string Prefix = "";
    private static void Main(string[] args)
    {
        int count = int.Parse(args[0]); // 시행횟수
        long networkLatency = 1;        // 레이턴시 ms
        Prefix = Environment.MachineName + ":"; // <-- :를 붙일지 말지 연산을 빼기 위해서 Prefix는 Empty이거나 :로 끝나도록 설정

        Console.WriteLine($"Test Count : {count}, Latency : {networkLatency}, MachieName : {Prefix}");

        long normalDelta = 0;
        long normalWork = 0;
        List<string> resultList = new();

        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < count; i++)
            {
                string key = args[0];
                resultList.Add(key);
            }
            stopwatch.Stop();
            long delta = stopwatch.ElapsedMilliseconds;
            long work = delta + networkLatency * count;

            normalDelta = delta;
            normalWork = work;

            Console.WriteLine($"Normal delta | {delta} |" +
                $"delta/normal_delta | {(double)delta / normalDelta * 100.0} |" +
                $"work/normal_work | {(double)work / normalWork * 100.0} |");
        }

        Prefix = Environment.MachineName + ":";
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < count; i++)
            {
                string key = MakeKey(args[0]);
                resultList.Add(key);
            }
            stopwatch.Stop();
            long delta = stopwatch.ElapsedMilliseconds;
            long work = delta + networkLatency * count;

            Console.WriteLine($"Prefix_normal delta | {delta} |" +
                $"delta/normal_delta | {(double)delta / normalDelta * 100.0} |" +
                $"work/normal_work | {(double)work / normalWork * 100.0} |");
        }

        Prefix = String.Empty;
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < count; i++)
            {
                string key = MakeKey(args[0]);
                resultList.Add(key);
            }
            stopwatch.Stop();
            long delta = stopwatch.ElapsedMilliseconds;
            long work = delta + networkLatency * count;

            Console.WriteLine($"Empty_normal delta | {delta} |" +
                $"delta/normal_delta | {(double)delta / normalDelta * 100.0} |" +
                $"work/normal_work | {(double)work / normalWork * 100.0} |");
        }

        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < count; i++)
            {
                string key = Random.Shared.Next(10000).ToString();
                resultList.Add(key);
            }
            stopwatch.Stop();
            long delta = stopwatch.ElapsedMilliseconds;
            long work = delta + networkLatency * count;

            Console.WriteLine($"Number delta | {delta} |" +
                $"delta/normal_delta | {(double)delta / normalDelta * 100.0} |" +
                $"work/normal_work | {(double)work / normalWork * 100.0} |");
        }

        Prefix = Environment.MachineName + ":";
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < count; i++)
            {
                string key = MakeKey(Random.Shared.Next(10000).ToString());
                resultList.Add(key);
            }
            stopwatch.Stop();
            long delta = stopwatch.ElapsedMilliseconds;
            long work = delta + networkLatency * count;

            Console.WriteLine($"Prefix_Number delta | {delta} |" +
                $"delta/normal_delta | {(double)delta / normalDelta * 100.0} |" +
                $"work/normal_work | {(double)work / normalWork * 100.0} |");
        }

        Prefix = String.Empty;
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < count; i++)
            {
                string key = MakeKey(Random.Shared.Next(10000).ToString());
                resultList.Add(key);
            }
            stopwatch.Stop();
            long delta = stopwatch.ElapsedMilliseconds;
            long work = delta + networkLatency * count;

            Console.WriteLine($"Empty_Number delta | {delta} |" +
                $"delta/normal_delta | {(double)delta / normalDelta * 100.0} |" +
                $"work/normal_work | {(double)work / normalWork * 100.0} |");
        }

        using (StreamWriter writer = new StreamWriter("result.txt"))
        {
            foreach (string key in resultList)
            {
                writer.WriteLine(key);
            }
        }
    }

    public static string MakeKey(string key)
    {
        return Prefix + key;
    }
}