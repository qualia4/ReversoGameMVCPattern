namespace Reverso.Model;

public interface ITwoPlayerGame
{
    public void StartGame(Player firstPlayer, Player secondPlayer);
    public void MakeMove();
    public bool GetEnded();
}