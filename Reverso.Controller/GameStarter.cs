namespace Reverso.Controller;
using Reverso.Model;

public class GameStarter
{
    private ReversoGameWithEvents game;

    public GameStarter(ReversoGameWithEvents game)
    {
        this.game = game;
    }

    public void StartPvEGame(ICommandHandler playerCommandHandler)
    {
        HumanPlayer firstPlayer = new HumanPlayer("A");
        AIPlayer secondPlayer = new AIPlayer("B");
        firstPlayer.SetCommandHandler(playerCommandHandler);
        game.StartGame(firstPlayer, secondPlayer);
    }

    public void StartPvPGame(ICommandHandler playerCommandHandler)
    {
        HumanPlayer firstPlayer = new HumanPlayer("A");
        HumanPlayer secondPlayer = new HumanPlayer("B");
        firstPlayer.SetCommandHandler(playerCommandHandler);
        secondPlayer.SetCommandHandler(playerCommandHandler);
        game.StartGame(firstPlayer, secondPlayer);
    }
}