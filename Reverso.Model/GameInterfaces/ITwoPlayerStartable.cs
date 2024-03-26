namespace Reverso.Model.GameInterfaces;
using Abstractions;

public interface ITwoPlayerStartable
{
    public void StartGame(Player firstPlayer, Player secondPlayer);
}
