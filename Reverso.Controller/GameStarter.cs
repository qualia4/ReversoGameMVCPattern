namespace Reverso.Controller;
using Reverso.Model;

public class GameStarter
{
    private ReversoGameWithEvents game;

    public GameStarter(ReversoGameWithEvents game)
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
        HumanPlayer firstPlayer = new HumanPlayer("A");
        AIPlayer secondPlayer = new AIPlayer("B");
        firstPlayer.SetCommandHandler(playerInputHandler);
        game.StartGame(firstPlayer, secondPlayer);
    }

    private void StartPvPGame(IInputHandler playerInputHandler)
    {
        HumanPlayer firstPlayer = new HumanPlayer("A");
        HumanPlayer secondPlayer = new HumanPlayer("B");
        firstPlayer.SetCommandHandler(playerInputHandler);
        secondPlayer.SetCommandHandler(playerInputHandler);
        game.StartGame(firstPlayer, secondPlayer);
    }

    private void StartEveGame()
    {
        AIPlayer firstPlayer = new AIPlayer("A");
        AIPlayer secondPlayer = new AIPlayer("B");
        game.StartGame(firstPlayer, secondPlayer);
    }
}