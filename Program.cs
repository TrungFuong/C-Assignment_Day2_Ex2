using System.Diagnostics;

internal class Ex2
{
    static async Task Main(string[] args)
    {
        var watch = Stopwatch.StartNew();

        Console.WriteLine("Enter a range!");
        Console.WriteLine("Enter the lower limit: ");
        int lowerLim = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter the upper limit: ");
        int upperLim = Convert.ToInt32(Console.ReadLine());

        // Launch asynchronous tasks to find prime numbers in different ranges
        Task<List<long>> primeTask1 = FindPrimesAsync(lowerLim, upperLim / 2);
        Task<List<long>> primeTask2 = FindPrimesAsync(upperLim / 2, upperLim);

        // Wait for both tasks to complete
        await Task.WhenAll(primeTask1, primeTask2);

        // Combine and print prime numbers
        List<long> allPrimes = new List<long>(primeTask1.Result);
        allPrimes.AddRange(primeTask2.Result);
        allPrimes.Sort();
        Console.WriteLine("Prime numbers within the range from {0} to {1}:", lowerLim, upperLim);
        foreach (var prime in allPrimes)
        {
            Console.Write($"{prime} ");
        }

        watch.Stop();
        Console.WriteLine($"\nExecution Time: {watch.ElapsedMilliseconds} ms");
    }

    public static bool IsPrime(long number)
    {
        if (number < 2) return false;
        for (long j = 2; j <= Math.Sqrt(number); j++)
        {
            if (number % j == 0) return false;
        }
        return true;
    }

    static async Task<List<long>> FindPrimesAsync(long lowerLim, long upperLim)
    {
        var primeNumbers = new List<long>();

        // Find prime numbers asynchronously
        await Task.Run(() =>
        {
            for (long number = lowerLim; number <= upperLim; number++)
            {
                if (IsPrime(number))
                {
                    primeNumbers.Add(number);
                }
            }
        });

        return primeNumbers;
    }
}