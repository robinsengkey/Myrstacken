class Ant
{
    private string name;
    private readonly int legsAmount;

    public Ant(string name, int legsAmount)
    {
        name = name.Substring(0, 1).ToUpper() + name.Substring(1).ToLower();
        this.name = name;
        this.legsAmount = legsAmount;
    }

    public string GetName()
    {
        return name;
    }
    public int GetLegs()
    {
        return legsAmount;
    }
    public override string ToString()
    {
        return name + ", " + legsAmount;
    }
}