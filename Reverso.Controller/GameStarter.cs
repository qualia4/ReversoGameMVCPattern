namespace Reverso.Controller;
using Reverso.Model;

public class GameStarter
{
    private ReversoGameWithEvents game;

    public GameStarter(ReversoGameWithEvents game)
    {
        this.game = game;
    }

    public void StartPvEGame(IInputHandler playerInputHandler)
    {
        HumanPlayer firstPlayer = new HumanPlayer("A");
        AIPlayer secondPlayer = new AIPlayer("B");
        firstPlayer.SetCommandHandler(playerInputHandler);
        game.StartGame(firstPlayer, secondPlayer);
    }

    public void StartPvPGame(IInputHandler playerInputHandler)
    {
        HumanPlayer firstPlayer = new HumanPlayer("A");
        HumanPlayer secondPlayer = new HumanPlayer("B");
        firstPlayer.SetCommandHandler(playerInputHandler);
        secondPlayer.SetCommandHandler(playerInputHandler);
        game.StartGame(firstPlayer, secondPlayer);
    }
}