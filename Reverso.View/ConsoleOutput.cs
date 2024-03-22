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
        Console.WriteLine("Type RESTART to play again or EXIT to finish!");
    }

    private void OnFieldUpdated(Cell[,]? field)
    {
        Console.Write("   ");
        for (int cord = 0; cord < 8; cord++)
        {
            Console.Write($" {cord} ");
        }
        Console.WriteLine();
        for (int i = 0; i < 8; i++)
        {
            Console.Write($" {i} ");
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

    private void OnPointsUpdated(Dictionary<string, int> players)
    {
        foreach (var player in players)
        {
            Console.WriteLine($"{player.Key}:{player.Value}");
        }
    }

    private void OnGameStarted()
    {
        Console.WriteLine("Game started!");
    }
}