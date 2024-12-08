
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

        long normalDelta = 1;
        long normalWork = 1;
        List<string> totalList = new();

        {
            List<string> resultList = new();
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
            totalList.AddRange(resultList);

            normalDelta = delta;
            normalWork = work;

            Console.WriteLine($"| Normal delta | {delta} | {(double)delta / normalDelta * 100.0} | {(double)work / normalWork * 100.0} |");
        }

        Prefix = Environment.MachineName + ":";
        {
            List<string> resultList = new();
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
            totalList.AddRange(resultList);

            Console.WriteLine($"| Prefix_normal delta | {delta} | {(double)delta / normalDelta * 100.0} | {(double)work / normalWork * 100.0} |");
        }

        Prefix = String.Empty;
        {
            List<string> resultList = new();
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
            totalList.AddRange(resultList);

            Console.WriteLine($"| Empty_normal delta | {delta} | {(double)delta / normalDelta * 100.0} | {(double)work / normalWork * 100.0} |");
        }

        {
            List<string> resultList = new();
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
            totalList.AddRange(resultList);

            Console.WriteLine($"| Number delta | {delta} | {(double)delta / normalDelta * 100.0} | {(double)work / normalWork * 100.0} |");
        }

        Prefix = Environment.MachineName + ":";
        {
            List<string> resultList = new();
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
            totalList.AddRange(resultList);

            Console.WriteLine($"| Prefix_Number delta | {delta} | {(double)delta / normalDelta * 100.0} | {(double)work / normalWork * 100.0} |");
        }

        Prefix = String.Empty;
        {
            List<string> resultList = new();
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
            totalList.AddRange(resultList);

            Console.WriteLine($"| Empty_Number delta | {delta} | {(double)delta / normalDelta * 100.0} | {(double)work / normalWork * 100.0} |");
        }

        using (StreamWriter writer = new StreamWriter("result.txt"))
        {
            foreach (string key in totalList)
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