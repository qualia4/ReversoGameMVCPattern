namespace Reverso.Model;

public interface ITwoPlayerGame: ITwoPlayerStartable
{
    public void MakeMove();
    public bool GetEnded();
}