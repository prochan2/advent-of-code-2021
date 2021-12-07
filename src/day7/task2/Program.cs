using System;
using System.IO;
using System.Linq;
using System.Threading;
using static System.Console;

var input = File.ReadAllText(@"..\..\..\input\input.txt")
    .Split(',')
    .Select(n => long.Parse(n))
    .ToArray();

Combination best = null;

var minPosition = input.Min();
var maxPosition = input.Max();

for (long commonPosition = minPosition; commonPosition <= maxPosition; commonPosition++)
{
    long sum = 0;

    foreach (var initialPositon in input)
    {
        var length = Math.Abs(initialPositon - commonPosition);
        var fuel = (length * (length + 1)) / 2;
        sum += fuel;
    }

    if (best == null || sum < best.Sum)
    {
        best = new Combination(commonPosition, sum);
    }
}

Console.WriteLine($"{best.CommonPosition} {best.Sum}");

class Combination
{
    public long CommonPosition { get; }

    public long Sum { get; }

    public Combination(long commonPosition, long sum)
    {
        CommonPosition = commonPosition;
        Sum = sum;
    }
}