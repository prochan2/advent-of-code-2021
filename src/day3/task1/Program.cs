using System;
using System.IO;
using static System.Console;

var lines = File.ReadAllLines(@"..\..\..\input\input.txt");

int length = lines[0].Length;
long[] ones = new long[length];
long[] zeros = new long[length];

foreach (var line in lines)
{
    for (int i = 0; i < length; i++)
    {
        if (line[i] == '1')
        {
            ones[i]++;
        }
        else
        {
            zeros[i]++;
        }
    }
}

long x = 0;
long y = 0;

long m = 1;

for (int i = length - 1; i >= 0; i--)
{
    if (ones[i] > zeros[i])
    {
        Write("1");
        x += m;
    }
    else
    {
        Write("0");
        y += m;
    }

    m *= 2;
}

WriteLine();
WriteLine(x);
WriteLine(y);
WriteLine(x * y);
