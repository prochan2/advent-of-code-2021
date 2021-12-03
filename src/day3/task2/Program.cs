using System;
using System.Collections.Generic;
using System.IO;
using static System.Console;

var lines = File.ReadAllLines(@"..\..\..\input\input.txt");

Code c1 = new Code(lines);
Code c0 = new Code(lines);

for (int j = 0; j < c1.Width; j++)
{
    char msb1 = c1.GetMsb(j) ? '1' : '0';
    char msb0 = c0.GetMsb(j) ? '0' : '1';

    WriteLine($"{j + 1}:");
    WriteLine($"MSB1: {msb1} Ones: {c1.GetOnes()} Zeros: {c1.GetZeros()}");
    WriteLine($"MSB0: {msb0} Ones: {c0.GetOnes()} Zeros: {c0.GetZeros()}");

    for (int i = 0; i < lines.Length; i++)
    {
        if (c1.RemainingLength > 1 && c1.IsLineValid(i) && lines[i][j] != msb1)
        {
            Write("MSB1: ");
            c1.Remove(i);
        }

        if (c0.RemainingLength > 1 && c0.IsLineValid(i) && lines[i][j] != msb0)
        {
            Write("MSB0: ");
            c0.Remove(i);
        }

        if (c1.RemainingLength == 1 && c0.RemainingLength == 1)
        {
            break;
        }
    }

    WriteLine();

    foreach (var line in c1.GetValidLines())
    {
        WriteLine(line);
    }

    WriteLine();

    foreach (var line in c0.GetValidLines())
    {
        WriteLine(line);
    }

    WriteLine();
    WriteLine();
}

long first1 = Convert.ToInt64(c1.GetFirst(), 2);
long first0 = Convert.ToInt64(c0.GetFirst(), 2);

WriteLine($"{c1.GetFirst()} {first1}");
WriteLine($"{c0.GetFirst()} {first0}");
WriteLine(first1 * first0);

class Code
{
    private readonly string[] _lines;
    private readonly bool[] _inverseMask;

    private readonly long[] _ones;
    private readonly long[] _zeros;

    public int RemainingLength { get; private set; }

    public int Width { get; }

    public Code(string[] lines)
    {
        RemainingLength = lines.Length;
        Width = lines[0].Length;
        _lines = lines;
        _inverseMask = new bool[RemainingLength];
        _ones = new long[Width];
        _zeros = new long[Width];

        foreach (var line in lines)
        {
            for (int i = 0; i < Width; i++)
            {
                if (line[i] == '1')
                {
                    _ones[i]++;
                }
                else
                {
                    _zeros[i]++;
                }
            }
        }
    }

    public bool IsLineValid(int i) => !_inverseMask[i];

    public void Remove(int i)
    {
        Write($"Removing {_lines[i]}: ");

        _inverseMask[i] = true;

        for (int j = 0; j < Width; j++)
        {
            if (_lines[i][j] == '1')
            {
                Write("-1 ");
                _ones[j]--;
            }
            else
            {
                Write("-0 ");
                _zeros[j]--;
            }
        }

        WriteLine();

        RemainingLength--;
    }

    public bool GetMsb(int j) => _ones[j] >= _zeros[j];

    public string GetOnes() => string.Join(' ', _ones);

    public string GetZeros() => string.Join(' ', _zeros);

    public string GetFirst()
    {
        for (int i = 0; i < _inverseMask.Length; i++)
        {
            if (!_inverseMask[i])
            {
                return _lines[i];
            }
        }

        throw new InvalidOperationException();
    }

    public IEnumerable<string> GetValidLines()
    {
        for (int i = 0; i < _lines.Length; i++)
        {
            if (!_inverseMask[i])
            {
                yield return _lines[i];
            }
        }
    }
}