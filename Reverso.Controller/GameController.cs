namespace Reverso.Controller;
using Model.Abstractions;
using System;

public class GameController
{
    private readonly ITwoPlayerGame game;
    private readonly GameStarter Starter;

    public GameController(ITwoPlayerGame game)
    {
        this.game = game;
        Starter = new GameStarter(game);
    }

    public void StartingMenu()
    {
        while (true)
        {
            Console.WriteLine("Type START to play or anything else to close the game!");
            switch (Console.ReadLine()?.ToLower())
            {
                case "start":
                    Starter.StartGame();
                    StartProcessing();
                    break;
                default:
                    return;
            }
        }
    }

    private void StartProcessing()
    {
        WriteInstruction();

        while (!game.GetEnded())
        {
            try
            {
                game.MakeMove();
            }
            catch (Exception ex)
            {
                string command = ex.Message;
                var splitCommand = command.Split(Array.Empty<char>());
                switch (splitCommand[0].ToLower())
                {
                    case "move":
                        Console.WriteLine("Invalid move. Please try again");
                        break;
                    case "restart":
                        Starter.StartGame();
                        WriteInstruction();
                        break;
                    case "exit":
                        return;
                    default:
                        Console.WriteLine($"{splitCommand[0].ToLower()} is not a valid command");
                        break;
                }
            }
        }
    }

    private void WriteInstruction()
    {
        Console.WriteLine("Commands:");
        Console.WriteLine("move Y X - make a move");
        Console.WriteLine("exit - close the game");
        Console.WriteLine("restart - restart the game");
    }
}
