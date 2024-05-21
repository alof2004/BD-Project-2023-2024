public class Animal
{
    public string Id { get; set; }
    public string Tipo { get; set; }
    public string Idade { get; set; }
    public string Brinco { get; set; }

public override string ToString()
{
    // Calculate the maximum lengths for each property to ensure proper alignment
    int maxTipoLength = Math.Max(Tipo.Length, 3); // Assuming a minimum of 3 for "tipo"
    int maxBrincoLength = Math.Max(Brinco.Length, 20); // Assuming a minimum of 20 for "brinco"

    // Calculate the total width needed for the formatted string
    int totalWidth = maxTipoLength + maxBrincoLength; // Adding 3 for "Anos" and space between "idade" and "Anos"

    // Format the string using the calculated widths
    return $"{Tipo.PadRight(maxTipoLength)}{Idade.PadLeft(2)} Anos {Brinco.PadRight(maxBrincoLength)}";
}

}
