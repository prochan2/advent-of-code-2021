using System;
using System.IO;
using System.Linq;
using System.Threading;
using static System.Console;

var lines = File.ReadAllLines(@"..\..\..\input\input.txt");

Point[,] data = new Point[lines.Length, lines[0].Length];

int r = 0;

foreach (var line in lines)
{
    int c = 0;

    foreach (var n in line)
    {
        long height = n - '0';
        data[r, c] = new Point(data, r, c, height);
        c++;
    }

    r++;
}

long sum = 0;

var originalColor = ForegroundColor;

try
{
    for (int i = 0; i < data.GetLength(0); i++)
    {
        for (int j = 0; j < data.GetLength(1); j++)
        {
            if (data[i, j].IsLow)
            {
                sum += data[i, j].Height + 1;

                ForegroundColor = ConsoleColor.White;
            }
            else
            {
                ForegroundColor = ConsoleColor.DarkGray;
            }

            Write(data[i, j].Height);
        }

        WriteLine();
    }
}
finally
{
    ForegroundColor = originalColor;
}

WriteLine(sum);

class Point
{
    private Point[,] _data;

    private int _row;

    private int _column;

    public long Height { get; }

    public Point? Upper => _row > 0 ? _data[_row - 1, _column] : null;

    public Point? Lower => _row < _data.GetLength(0) - 1 ? _data[_row + 1, _column] : null;

    public Point? Left => _column > 0 ? _data[_row, _column - 1] : null;

    public Point? Right => _column < _data.GetLength(1) - 1 ? _data[_row, _column + 1] : null;

    private bool IsLowerThan(Point? other)
    {
        if (other == null)
        {
            return true;
        }
        else
        {
            return Height < other.Height;
        }
    }

    public bool IsLow => 
        IsLowerThan(Upper) &&
        IsLowerThan(Lower) &&
        IsLowerThan(Right) &&
        IsLowerThan(Left);

    public Point(Point[,] data, int row, int column, long height)
    {
        _data = data;
        _row = row;
        _column = column;
        Height = height;
    }
}