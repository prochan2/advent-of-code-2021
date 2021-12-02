using System;
using System.IO;
using static System.Console;

var lines = File.ReadAllLines(@"..\..\..\input\input.txt");

var aim = 0L;
var h = 0L;
var v = 0L;

foreach (var line in lines)
{
    var instruction = line.Split(' ');

    var offset = long.Parse(instruction[1]);

    switch (instruction[0])
    {
        case "forward":
            h += offset;
            v += aim * offset;
            break;

        case "down":
            aim += offset;
            break;

        case "up":
            aim -= offset;
            break;

        default:
            throw new InvalidOperationException();
    }
}

WriteLine(h * v);
