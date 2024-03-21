namespace Reverso.Model;

public class Player: IHasName
{
    public string Name { get; }
    protected int Points { get; set; }

    public Player(string name)
    {
        Name = name;
        Points = 0;
    }

    public void AddPoints(int pointsToAdd)
    {
        Points += pointsToAdd;
    }

    public void Reset()
    {
        Points = 0;
    }

    public void RemovePoint()
    {
        Points --;
    }

    public int GetPoints()
    {
        return Points;
    }

    public string GetName()
    {
        return Name;
    }
}