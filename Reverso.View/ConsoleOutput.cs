using Reverso.Model;

namespace Reverso.View;

using System;

public class ConsoleOutput
{
    private ReversoGameWithEvents game;

    public void ListenTo(ReversoGameWithEvents game)
    {
        this.game = game;
        game.GameStarted += OnGameStarted;
        game.FieldUpdated += OnFieldUpdated;
        game.PointsUpdated += OnPointsUpdated;
        game.GameEnded += OnGameEnded;
    }

    private void OnGameStarted()
    {
        Console.WriteLine("Game started!");
    }

    private void OnFieldUpdated(Cell[,] field)
    {
        Console.Clear();
        DrawXCoords(field.GetLength(0));
        DrawBorder(field.GetLength(0));
        DrawField(field);
        DrawBorder(field.GetLength(0));
    }

    private void OnPointsUpdated(Dictionary<string, int> players)
    {
        Console.Write("            ");
        foreach (var player in players)
        {
            Console.Write($"{player.Key}:{player.Value}   ");
        }
        Console.WriteLine(" ");
        Console.WriteLine(" ");
    }

    private void OnGameEnded(Player? winner)
    {
        Console.WriteLine("Game is over!");
        if (winner == null)
        {
            Console.WriteLine("No winner found!");
            return;
        }
        else
        {
            Console.WriteLine($"Player {winner.GetName()} won with {winner.GetPoints()} points!");
        }
    }

    private void DrawXCoords(int fieldLength)
    {
        Console.Write("     ");
        for (int cord = 0; cord < fieldLength; cord++)
        {
            Console.Write($" {cord} ");
        }
        Console.WriteLine();
    }

    private void DrawBorder(int fieldLength)
    {
        Console.Write("     ");
        for (int cord = 0; cord < fieldLength; cord++)
        {
            Console.Write($"===");
        }
        Console.WriteLine();
    }

    private void DrawField(Cell[,] field)
    {
        for (int i = 0; i < field.GetLength(1); i++)
        {
            Console.Write($" {i} | ");
            for (int j = 0; j < 8; j++)
            {
                if (field[i, j].IfEmpty)
                {
                    if (field[i, j].IfValid)
                    {
                        Console.Write(" * ");
                    }
                    else
                    {
                        Console.Write(" - ");
                    }
                }
                else
                {
                    Console.Write($" {field[i,j].GetHost()?.GetName().Substring(0,1)} ");
                }
            }
            Console.WriteLine(" ");
        }
    }
}