using System.IO;
using System.Linq;
using static System.Console;

var lines = File.ReadAllLines(@"..\..\..\input\input.txt");

long? previous = null;
long current;
long count = 0;

foreach (var line in lines)
{
    current = long.Parse(line);

    try
    {
        if (previous == null)
        {
            WriteLine($"{line} N/A");
            continue;
        }

        if (current > previous)
        {
            WriteLine($"{line} +");
            ++count;
        }

        WriteLine($"{line} -");
    }
    finally
    {
        previous = current;
    }
}

WriteLine(count);
