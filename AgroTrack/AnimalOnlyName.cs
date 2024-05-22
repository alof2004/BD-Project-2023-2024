namespace AgroTrack{
public class AnimalOnlyName
{
    public int Id { get; set; }
    public string Tipo { get; set; }
    public string Idade { get; set; }
    public string Brinco { get; set; }

public override string ToString()
{
    return $"{Tipo}";
}

}
}