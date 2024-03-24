namespace Reverso.Model;

public abstract class Player: IHasName
{
    public string Name { get; }
    public int Points { get; set; }

    public Player(string name)
    {
        Name = name;
        Points = 0;
    }

    public void AddPoints(int pointsToAdd)
    {
        Points += pointsToAdd;
    }

    public void ResetPoints()
    {
        Points = 0;
    }

    public void RemovePoints(int pointsToRemove)
    {
        Points -= pointsToRemove;
    }

    public int GetPoints()
    {
        return Points;
    }

    public string GetName()
    {
        return Name;
    }

    public abstract int MakeMoveOnField(Field GameField);

}