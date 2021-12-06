using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Console;

var ages = File.ReadAllText(@"..\..\..\input\input.txt").Split(',').Select(n => long.Parse(n)).ToList();

for (int day = 1; day <= 80; day++)
{
    int count = ages.Count;

    for (int i = 0; i < count; i++)
    {
        if (--ages[i] == -1)
        {
            ages[i] = 6;
            ages.Add(8);
        }
    }

    //Write($"Day {day}: ");

    //foreach (var age in ages)
    //{
    //    Write($"{age}, ");
    //}

    //WriteLine();
}

WriteLine(ages.Count);