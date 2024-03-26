namespace Reverso.Model.Game;
using GameInterfaces;
using Abstractions;

public class ReversoGame : ITwoPlayerGame
{
    private Player? FirstPlayer;
    private Player? SecondPlayer;
    private Player? CurrentPlayer { get; set; }
    private ReversoField ReversoGameField { get; } = new ReversoField();
    private bool Ended { get; set; }
    protected Cell[,] GetField() => ReversoGameField.GetCells();

    public void MakeMove()
    {
        CheckGameStarted();
        ChangeField();
        CheckGameEnd();
    }

    protected virtual void ChangeField()
    {
        CheckGameStarted();
        int pointsToChange = CurrentPlayer.MakeMoveOnField(ReversoGameField);
        RedistributePoints(pointsToChange);
        SwitchPlayer();
        ReversoGameField.ChangeValid(CurrentPlayer);
    }

    public virtual void StartGame(Player firstPlayer, Player secondPlayer)
    {
        Ended = false;
        FirstPlayer = firstPlayer;
        SecondPlayer = secondPlayer;
        firstPlayer.ResetPoints();
        secondPlayer.ResetPoints();
        CurrentPlayer = firstPlayer;
        ReversoGameField.Initialize(firstPlayer, secondPlayer);
    }

    private void CheckGameEnd()
    {
        if (ReversoGameField.HasValidMoves())
        {
            return;
        }

        EndGame();
    }

    protected Dictionary<string, int> GetPoints()
    {
        CheckGameStarted();
        var players = new Dictionary<string, int>()
        {
            {FirstPlayer.GetName(), FirstPlayer.GetPoints()},
            {SecondPlayer.GetName(), SecondPlayer.GetPoints()}
        };
        return players;
    }

    protected virtual void EndGame()
    {
        CheckGameStarted();
        Ended = true;
    }

    private void RedistributePoints(int points)
    {
        if (CurrentPlayer == FirstPlayer)
        {
            SecondPlayer?.RemovePoints(points);
            FirstPlayer?.AddPoints(points);
            return;
        }

        FirstPlayer?.RemovePoints(points);
        SecondPlayer?.AddPoints(points);
    }

    public Player? GetWinner()
    {
        CheckGameStarted();
        return (FirstPlayer?.GetPoints() > SecondPlayer?.GetPoints()) ? FirstPlayer :
            (SecondPlayer?.GetPoints() > FirstPlayer?.GetPoints()) ? SecondPlayer : null;
    }

    private void SwitchPlayer()
    {
        CurrentPlayer = CurrentPlayer == FirstPlayer ? SecondPlayer : FirstPlayer;
    }

    private void CheckGameStarted()
    {
        if (FirstPlayer == null || SecondPlayer == null)
        {
            throw new Exception("Game has not been started");
        }
    }

    public bool GetEnded()
    {
        return Ended;
    }

}