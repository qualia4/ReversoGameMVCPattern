namespace Reverso.Model.GameInterfaces;

public interface ITwoPlayerGame: ITwoPlayerStartable
{
    public void MakeMove();
    public bool GetEnded();
}