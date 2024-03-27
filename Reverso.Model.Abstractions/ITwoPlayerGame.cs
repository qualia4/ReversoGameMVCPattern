namespace Reverso.Model.Abstractions;

public interface ITwoPlayerGame: ITwoPlayerStartable
{
    public void MakeMove();
    public bool GetEnded();
}