namespace DiskSpaceAnalysis;

using System;
using System.Collections.Generic;
using System.Linq;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Number of segment!");
        var segmentAmount = Convert.ToInt32(Console.ReadLine());
        var line = new List<int>() { 1, 2, 3, 1, 2 };
        int segmentMax = Segment(segmentAmount, line);
        Console.WriteLine(segmentMax);
    }

    private static int Segment(int x, List<int> space)
    {
        if (x == 0 || x > space.Count)
        {
            return 0;
        }

        if (space.Count == 0 || space.Count > 1_000_000)
        {
            return 0;
        }

        if (space.Any(s => s > 1_000_000_000))
        {
            return 0;
        }
        space = space.Distinct().ToList();
        if (space.Count == 1) return space[0];
        var segmentMinimas = new List<int>();
        var segmentsAmount = space.Count - (x - 1);
        var maxListValue = space.Max();
        var maxValue = 0;

        var chunks = space.Select((element, i) => new { Index = i, Value = element })
            .GroupBy(s => segmentsAmount)
            .Select(s => s.Select(v => v.Value).ToList().Min())
            .Max();

        for (int i = 0; i < segmentsAmount; i++)
        {
            var currentSegmentMin = space.Skip(i).Take(x).Min();
            if (currentSegmentMin == maxListValue) return maxListValue;

            if (maxValue < currentSegmentMin)
                maxValue = currentSegmentMin;
        }

        return maxValue;
    }
}