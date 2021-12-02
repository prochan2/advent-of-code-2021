using System;
using System.IO;
using System.Linq;
using static System.Console;

var lines = File.ReadAllLines(@"..\..\..\input\input.txt");

var v = 0L;
var h = 0L;

foreach (var line in lines)
{
    var instruction = line.Split(' ');

    var offset = long.Parse(instruction[1]);

    switch (instruction[0])
    {
        case "forward":
            v += offset;
            break;

        case "down":
            h += offset;
            break;

        case "up":
            h -= offset;
            break;

        default:
            throw new InvalidOperationException();
    }
}

WriteLine(v * h);
