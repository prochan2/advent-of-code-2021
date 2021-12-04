using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Console;

var lines = File.ReadAllLines(@"..\..\..\input\input.txt");

var drawnNumbers = lines[0].Split(',').Select(n => long.Parse(n));

var boardsDataEnumerator = lines.Skip(1).GetEnumerator();
boardsDataEnumerator.MoveNext();
List<BingoBoard> boards = new();

while (boardsDataEnumerator.MoveNext())
{
    boards.Add(BingoBoard.Load(boardsDataEnumerator));
}

long winningNumber = -1;
long winningSum = -1;

foreach (var drawmNumber in drawnNumbers)
{
    WriteLine(drawmNumber);

    List<BingoBoard> remainingBoards = new();

    foreach (var board in boards)
    {
        if (board.TryWin(drawmNumber))
        {
            winningNumber = drawmNumber;
            winningSum = board.UnmarkedsSum;
        }
        else
        {
            remainingBoards.Add(board);
        }

        WriteLine();
        board.Print();
    }

    WriteLine();

    if (remainingBoards.Count == 0)
    {
        break;
    }

    boards = remainingBoards;
}

WriteLine();
WriteLine(winningNumber);
WriteLine(winningSum);
WriteLine(winningNumber * winningSum);

class BingoBoard
{
    private readonly int _size;
    private readonly long[,] _board; // For printing only
    private readonly bool[,] _marks; // For printing only
    private readonly Dictionary<long, (int RowNumber, int ColumnNumber)> _indices = new();
    private readonly int[] _rowMarksCounts;
    private readonly int[] _columnMarksCounts;

    public long UnmarkedsSum { get; private set; }

    public BingoBoard(int size)
    {
        _size = size;
        _board = new long[size, size];
        _marks = new bool[size, size];
        _rowMarksCounts = new int[size];
        _columnMarksCounts = new int[size];
    }

    public static BingoBoard Load(IEnumerator<string> source)
    {
        BingoBoard board = null;

        int rowNumber = 0;
        do
        {
            var rowData = source.Current.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (board == null)
            {
                board = new BingoBoard(rowData.Length);
            }

            if (rowData.Length != board._size)
            {
                throw new ArgumentException($"Wrong size: {source.Current}", nameof(rowData));
            }

            for (int columnNumber = 0; columnNumber < board._size; columnNumber++)
            {
                var value = long.Parse(rowData[columnNumber]);
                board._board[rowNumber, columnNumber] = value;
                board._indices.Add(value, (rowNumber, columnNumber));
                board.UnmarkedsSum += value;
            }

            source.MoveNext();
            rowNumber++;
        }
        while (rowNumber < board._size);

        return board;
    }

    public bool TryWin(long markedValue)
    {
        if (!_indices.TryGetValue(markedValue, out var index))
        {
            return false;
        }

        _marks[index.RowNumber, index.ColumnNumber] = true;
        _rowMarksCounts[index.RowNumber]++;
        _columnMarksCounts[index.ColumnNumber]++;
        UnmarkedsSum -= markedValue;

        return _rowMarksCounts[index.RowNumber] == _size || _columnMarksCounts[index.ColumnNumber] == _size;
    }

    public void Print()
    {
        var originalColor = ForegroundColor;

        try
        {
            for (int rowNumber = 0; rowNumber < _size; rowNumber++)
            {
                for (int columnNumber = 0; columnNumber < _size; columnNumber++)
                {
                    if (_marks[rowNumber, columnNumber])
                    {
                        ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        ForegroundColor = ConsoleColor.DarkGray;
                    }

                    Write("{0,2} ", _board[rowNumber, columnNumber]);
                }

                WriteLine();
            }
        }
        finally
        {
            ForegroundColor = originalColor;
        }
    }
}