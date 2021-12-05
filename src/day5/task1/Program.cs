using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Console;

var lines = File.ReadAllLines(@"..\..\..\input\input.txt");

Counter counter = new();

foreach (var line in lines)
{
    var ponits = line.Split(" -> ");
    var a = Coordinate2.Parse(ponits[0]);
    var b = Coordinate2.Parse(ponits[1]);

    // Not a vertical or horizontal line
    if (a.X != b.X && a.Y != b.Y)
    {
        continue;
    }

    var v = Coordinate2.GetVector(a, b);
    var c = a;

    while (true)
    {
        counter.Increment(c);

        if (c == b)
        {
            break;
        }

        c = c.Add(v);
    }
}

Write(counter.MoreThan2Count);

public class Counter
{
    private readonly Dictionary<Coordinate2, long> _counts = new(); // Could store just bools

    public long MoreThan2Count { get; private set; }

    public void Increment(Coordinate2 coordinate)
    {
        if (_counts.TryGetValue(coordinate, out long count))
        {
            _counts[coordinate] = ++count;

            if (count == 2)
            {
                MoreThan2Count++;
            }
        }
        else
        {
            _counts.Add(coordinate, 1);
        }
    }
}

public struct Coordinate2 : IEquatable<Coordinate2>
{
    public long X { get; }

    public long Y { get; }

    public Coordinate2(long x, long y)
    {
        X = x;
        Y = y;
    }

    public static Coordinate2 Parse(string coordinateString)
    {
        var coordinateParts = coordinateString.Split(',');

        long x = long.Parse(coordinateParts[0]);
        long y = long.Parse(coordinateParts[1]);

        return new Coordinate2(x, y);
    }

    public static Vector2 GetVector(Coordinate2 a, Coordinate2 b)
    {
        long GetVector1(long x, long y)
            => (y - x) switch
            {
                0 => 0,
                > 0 => 1,
                < 0 => -1
            };

        long dX = GetVector1(a.X, b.X);
        long dY = GetVector1(a.Y, b.Y);

        return new Vector2(dX, dY);
    }

    public Coordinate2 Add(Vector2 d)
        => new Coordinate2(X + d.DX, Y + d.DY);

    public override bool Equals(object obj)
    {
        return obj is Coordinate2 coordinates && Equals(coordinates);
    }

    public bool Equals(Coordinate2 other)
    {
        return X == other.X &&
               Y == other.Y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public static bool operator ==(Coordinate2 left, Coordinate2 right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Coordinate2 left, Coordinate2 right)
    {
        return !(left == right);
    }

    public override string ToString()
        => $"{X},{Y}";
}

public struct Vector2
{
    public long DX { get; }

    public long DY { get; }

    public Vector2(long dX, long dY)
    {
        DX = dX;
        DY = dY;
    }

    public override string ToString()
        => $"{DX},{DY}";
}