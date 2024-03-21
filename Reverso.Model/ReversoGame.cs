namespace Reverso.Model;

public class ReversoGame(Player firstPlayer, Player secondPlayer)
{
    private const int FieldSize = 8;
    private Cell[,]? Field { get; set; }
    private Player? CurrentPlayer { get; set; }
    private bool Ended { get; set; }
    private bool PvE { get; set; }
    protected Cell[,]? GetField() => Field?.Clone() as Cell[,];

    Random rand = new Random();

    public void MakeMove(int x, int y)
    {
        if (x < 0 || x > FieldSize - 1 || y < 0 || y > FieldSize - 1)
        {
            Console.WriteLine("Coordinates out of field. Please choose another cell.");
            return;
        }
        if (!Field[x,y].IfValid)
        {
            Console.WriteLine("Invalid move. Please choose another cell.");
            return;
        }

        ChangeField(x, y);
        CheckGameEnd();

        if (PvE && !Ended && CurrentPlayer == secondPlayer)
        {
            int delay = rand.Next(1, 4);
            Thread.Sleep((int)TimeSpan.FromSeconds(delay).TotalMilliseconds);
            MakeRandomMove();
        }
    }

    public void MakeRandomMove()
    {
        while (true)
        {
            int x = rand.Next(0, 8);
            int y = rand.Next(0, 8);
            if (Field[x, y].IfValid)
            {
                MakeMove(x, y);
                return;
            }
        }
    }

    public bool IsValidMove(int x, int y)
    {
        int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
        int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

        if (Field == null || Ended)
        {
            throw new Exception("Game hasn't been started yet.");
        }

        if (!Field[x, y].IfEmpty || !IsInBounds(x, y))
        {
            return false;
        }

        for (int dir = 0; dir < 8; dir++)
        {
            if (IsValidDirection(x, y, dx[dir], dy[dir]))
            {
                return true;
            }
        }
        return false;
    }

    private void ChangeValid()
    {
        for (int x = 0; x < FieldSize; x++)
        {
            for (int y = 0; y < FieldSize; y++)
            {
                if (IsValidMove(x, y))
                {
                    Field[x, y].SetValid(true);
                }
                else
                {
                    Field[x, y].SetValid(false);
                }
            }
        }
    }

    public bool IsValidDirection(int x, int y, int dirX, int dirY)
    {
        bool foundOpponent = false;
        while (true)
        {
            x += dirX;
            y += dirY;
            if (!IsInBounds(x, y) || Field[x, y].IfEmpty)
            {
                break;
            }

            if (Field[x, y].GetPlayer() == CurrentPlayer)
            {
                if (foundOpponent)
                {
                    return true;
                }
                break;
            }
            foundOpponent = true;
        }
        return false;
    }

    public void StartGame(bool pve)
    {
        Ended = false;
        PvE = pve;
        firstPlayer.Reset();
        secondPlayer.Reset();
        CurrentPlayer = firstPlayer;
        InitializeField();
    }

    protected virtual void InitializeField()
    {
        Field = new Cell[FieldSize, FieldSize];
        for (var x = 0; x < Field.GetLength(0); x++)
        {
            for (var y = 0; y < Field.GetLength(1); y++)
            {
                Field[x, y] = new Cell();
            }
        }
        Field[3,4].Rehost(firstPlayer);
        Field[4,3].Rehost(firstPlayer);
        firstPlayer.AddPoints(2);
        Field[3,3].Rehost(secondPlayer);
        Field[4,4].Rehost(secondPlayer);
        secondPlayer.AddPoints(2);
        ChangeValid();
    }

    protected virtual void ChangeField(int x, int y)
    {
        MarkCell(x, y);
        CurrentPlayer?.AddPoints(1);
        int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
        int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

        for (int dir = 0; dir < 8; dir++)
        {
            if (IsValidDirection(x, y, dx[dir], dy[dir]))
            {
                int pointToAdd = FlipPiecesInDirection(x, y, dx[dir], dy[dir]);
                CurrentPlayer?.AddPoints(pointToAdd);
            }
        }
        SwitchPlayer();
        ChangeValid();
    }

    private bool IsInBounds(int x, int y)
    {
        return x is >= 0 and < FieldSize && y is >= 0 and < FieldSize;
    }

    private int FlipPiecesInDirection(int startX, int startY, int dx, int dy)
    {
        int x = startX + dx;
        int y = startY + dy;
        int pointsToAdd = 0;

        while (IsInBounds(x, y) && Field?[x, y].IfEmpty == false &&
               Field[x, y].GetPlayer() != CurrentPlayer)
        {
            Field[x, y].Rehost(CurrentPlayer);
            pointsToAdd++;
            if (CurrentPlayer == firstPlayer)
            {
                secondPlayer.RemovePoint();
            }
            else
            {
                firstPlayer.RemovePoint();
            }
            x += dx;
            y += dy;
        }
        return pointsToAdd;
    }

    private void MarkCell(int x, int y)
    {
        Field?[x, y].Rehost(CurrentPlayer);
    }

    private void CheckGameEnd()
    {
        var hasValidMoves = false;
        for (var x = 0; x < Field?.GetLength(0); x++)
        {
            for (var y = 0; y < Field.GetLength(1); y++)
            {
                if (Field[x, y].IfValid)
                {
                    hasValidMoves = true;
                }
            }
        }

        if (!hasValidMoves)
        {
            EndGame();
        }
    }

    public Dictionary<string, int> GetPoints()
    {
        var players = new Dictionary<string, int>()
        {
            {firstPlayer.GetName(), firstPlayer.GetPoints()},
            {secondPlayer.GetName(), secondPlayer.GetPoints()}
        };
        return players;
    }

    protected virtual void EndGame()
    {
        Ended = true;
    }

    public Player? GetWinner()
    {
        return (firstPlayer.GetPoints() > secondPlayer.GetPoints()) ?
            firstPlayer : (secondPlayer.GetPoints() > firstPlayer.GetPoints()) ?
                secondPlayer : null;
    }

    private void SwitchPlayer()
    {
        CurrentPlayer = CurrentPlayer == firstPlayer ? secondPlayer : firstPlayer;
    }

}