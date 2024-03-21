namespace Reverso.Model;

using System;

public class ReversoGameWithEvents : ReversoGame
{
    public event Action? GameStarted;

    public event Action<Cell[,]?>? FieldUpdated;

    public event Action<Player?>? GameEnded;

    public event Action<Dictionary<string, int>>? PointsUpdated;

    public ReversoGameWithEvents(Player firstPlayer, Player secondPlayer) : base(firstPlayer, secondPlayer)
    {
    }

    protected override void InitializeField()
    {
        base.InitializeField();
        FieldUpdated?.Invoke(GetField());
        PointsUpdated?.Invoke(GetPoints());
        GameStarted?.Invoke();
    }

    protected override void ChangeField(int x, int y)
    {
        base.ChangeField(x, y);
        FieldUpdated?.Invoke(GetField());
        PointsUpdated?.Invoke(GetPoints());
    }

    protected override void EndGame()
    {
        base.EndGame();
        GameEnded?.Invoke(GetWinner());
    }


}