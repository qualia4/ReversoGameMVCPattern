namespace Reverso.Model;

public class Cell
{
    private bool IsEmpty { get; set; } = true;
    private bool IsValid { get; set; } = false;
    private IHasName? Host { get; set; }

    public bool IfEmpty => IsEmpty == true;
    public bool IfValid => IsValid == true;

    public void Rehost(IHasName? newHost)
    {
        Host = newHost;
        IsEmpty = false;
    }

    public void SetValid(bool isValid)
    {
        this.IsValid = isValid;
    }


    public IHasName? GetHost()
    {
        return Host;
    }
}