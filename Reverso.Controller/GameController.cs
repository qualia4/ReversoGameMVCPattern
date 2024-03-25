namespace Reverso.Controller;

using System;
using Reverso.Model;

public class GameController
{
    private ITwoPlayerGame game;
    private GameStarter Starter;

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
                var splitCommand = command.Split(new char[0]);
                switch (splitCommand[0].ToLower())
                {
                    case "move":
                        Console.WriteLine("Invalid coordinates. Please try again");
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
