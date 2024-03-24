namespace Reverso.Controller;

using System;
using Reverso.Model;

public class ConsoleInput
{
    public void StartProcessing(ReversoGameWithEvents game)
    {
        Console.WriteLine("Welcome to the ReversoGame!");
        Console.WriteLine("Type START to play or anything else to close the game!");
        switch (Console.ReadLine().ToLower())
        {
            case "start":
                StartGame(game);
                break;
            default:
                return;
        }
        while (true)
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
                        StartGame(game);
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

    public void StartGame(ReversoGameWithEvents game)
    {
        GameStarter starter = new GameStarter(game);
        MoveCommandHandler moveHandler = new MoveCommandHandler();
        if (ChooseGameMode())
        {
            starter.StartPvEGame(moveHandler);
            return;
        }
        starter.StartPvPGame(moveHandler);
        WriteInstruction();
    }

    private bool ChooseGameMode()
    {
        Console.WriteLine("PvP or PvE?");
        while (true)
        {
            string responce = Console.ReadLine().ToLower();
            if (responce == "pve")
            {
                return true;
            }
            else if (responce == "pvp")
            {
                return false;
            }
            Console.WriteLine("Invalid answer. PvP or PvE?");
        }
    }

    public void WriteInstruction()
    {
        Console.WriteLine("Commands:");
        Console.WriteLine("move Y X - make a move");
        Console.WriteLine("exit - close the game");
        Console.WriteLine("restart - restart the game");
    }
}
