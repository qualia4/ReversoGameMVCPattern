using System.Diagnostics;

namespace Reverso.Controller;
using Reverso.Model;

public class GameStarter
{
    private ITwoPlayerGame game;

    public GameStarter(ITwoPlayerGame game)
    {
        this.game = game;
    }

    public void StartGame()
    {
        Console.WriteLine("PvP, PvE or EvE?");
        while (true)
        {
            string responce = Console.ReadLine().ToLower();
            switch (responce)
            {
                case "pvp":
                    StartPvPGame(new MoveInputHandler());
                    return;
                case "pve":
                    StartPvEGame(new MoveInputHandler());
                    return;
                case "eve":
                    StartEveGame();
                    return;
                default:
                    Console.WriteLine("Invalid answer. PvP, PvE or EvE?");
                    break;
            }
        }
    }


    private void StartPvEGame(IInputHandler playerInputHandler)
    {
        bool delay = AskDelay();
        HumanPlayer firstPlayer = new HumanPlayer("A", playerInputHandler);
        AIPlayer secondPlayer = new AIPlayer("B", delay);
        game.StartGame(firstPlayer, secondPlayer);
    }

    private void StartPvPGame(IInputHandler playerInputHandler)
    {
        HumanPlayer firstPlayer = new HumanPlayer("A", playerInputHandler);
        HumanPlayer secondPlayer = new HumanPlayer("B", playerInputHandler);
        game.StartGame(firstPlayer, secondPlayer);
    }

    private void StartEveGame()
    {
        bool delay = AskDelay();
        AIPlayer firstPlayer = new AIPlayer("A", delay);
        AIPlayer secondPlayer = new AIPlayer("B", delay);
        game.StartGame(firstPlayer, secondPlayer);
    }

    private bool AskDelay()
    {
        Console.WriteLine("Do you want bot with delay? y/n");
        while (true)
        {
            switch (Console.ReadLine()?.ToLower())
            {
                case "y":
                    return true;
                case "n":
                    return false;
            }
        }
    }
}