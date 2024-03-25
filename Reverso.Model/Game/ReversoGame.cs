namespace Reverso.Model;

public class ReversoGame : ITwoPlayerGame
{
    private Player? FirstPlayer;
    private Player? SecondPlayer;
    private Field GameField { get; } = new Field();
    private bool Ended { get; set; }
    private Player? CurrentPlayer { get; set; }
    protected Cell[,]? GetField() => GameField.GetCells();

    public void MakeMove()
    {
        CheckGameStarted();
        ChangeField();
        CheckGameEnd();
    }

    protected virtual void ChangeField()
    {
        CheckGameStarted();
        int pointsToChange = CurrentPlayer.MakeMoveOnField(GameField);
        RedistributePoints(pointsToChange);
        SwitchPlayer();
        GameField.ChangeValid(CurrentPlayer);
    }

    public virtual void StartGame(Player firstPlayer, Player secondPlayer)
    {
        Ended = false;
        FirstPlayer = firstPlayer;
        SecondPlayer = secondPlayer;
        firstPlayer.ResetPoints();
        secondPlayer.ResetPoints();
        CurrentPlayer = firstPlayer;
        GameField.Initialize(firstPlayer, secondPlayer);
    }

    private void CheckGameEnd()
    {
        if (GameField.HasValidMoves())
        {
            return;
        }

        EndGame();
    }

    public Dictionary<string, int>? GetPoints()
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