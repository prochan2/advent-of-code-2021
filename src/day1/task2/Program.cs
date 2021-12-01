using System.IO;
using System.Linq;
using static System.Console;

var lines = File.ReadAllLines(@"..\..\..\input\input.txt");
var measurements = lines.Select(line => long.Parse(line.Split(new[] { ' ' }).First())).ToArray();

long? previous = null;
long current;
long count = 0;

for( int i = 2; i < measurements.Length; i++ )
{
    current = measurements[i] + measurements[i - 1] + measurements[i - 2];

    try
    {
        if (previous == null)
        {
            WriteLine($"{current} N/A");
            continue;
        }

        if (current > previous)
        {
            WriteLine($"{current} +");
            ++count;
        }

        WriteLine($"{current} -");
    }
    finally
    {
        previous = current;
    }
}

WriteLine(count);
