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
                game.StartGame(ChooseGameMove());
                Console.WriteLine("Commands:");
                Console.WriteLine("move Y X - make a move");
                Console.WriteLine("exit - close the game");
                Console.WriteLine("restart - restart the game");
                break;
            default:
                return;
        }
        while (true)
        {
            string command = Console.ReadLine();
            var splitCommand = command.Split(new char[0]);
            switch (splitCommand[0].ToLower())
            {
                case "restart":
                    game.StartGame(ChooseGameMove());
                    break;
                case "exit":
                    return;
                case "move":
                    var x = int.Parse(splitCommand[1]);
                    var y = int.Parse(splitCommand[2]);
                    game.MakeMove(x, y);
                    break;
                default:
                    Console.WriteLine($"{splitCommand[0].ToLower()} is not a valid command");
                    break;
            }
        }
    }

    private bool ChooseGameMove()
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
}