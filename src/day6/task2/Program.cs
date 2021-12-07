using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Console;

const long firstInitialAge = 8;
const long nextInitialAge = 6;
const long initialLifespan = 18;

long[,] offspringCache = new long[firstInitialAge + 1, initialLifespan + 1];

for (int i = 0; i < offspringCache.GetLength(0); i++)
{
    for (int j = 0; j < offspringCache.GetLength(1); j++)
    {
        offspringCache[i, j] = -1;
    }
}

long GetNumberOfOffspring(long age, long lifespan)
{
    long offspring = offspringCache[age, lifespan];

    if (offspring >= 0)
    {
        return offspring;
    }

    offspring = 0;

    for (long day = 1; day <= lifespan; day++)
    {
        if (--age == -1)
        {
            offspring += GetNumberOfOffspring(firstInitialAge, lifespan - day) + 1;
            age = nextInitialAge;
        }
    }

    offspringCache[age, lifespan] = offspring;

    return offspring;
}

var result =
    File.ReadAllText(@"..\..\..\input\sinput.txt")
    //"3"
    .Split(',')
    .Select(n => long.Parse(n))
    .Sum(age =>
    {
        return GetNumberOfOffspring(age, initialLifespan) + 1;
    });

WriteLine(result);

//class Lanternfish
//{
//    public long Age { get; }

//    public long Lifespan { get; }

//    public IEnumerable<Lanternfish> Reproduce()
//    {
//        for (int i = 0; i < length; i++)
//        {

//        }
//    }
//};