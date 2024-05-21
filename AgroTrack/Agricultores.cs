public class Agricultores
{
    public int Id_Trabalhador { get; set; }
    public int Pessoa_N_CartaoCidadao { get; set; }
    public int Quinta_Empresa_Id_Empresa { get; set; }
    public string Nome { get; set; }
    public int Contacto { get; set; }
    public Agricultores()
    {
    }

    public override string ToString()
    {
        return $"{Nome} - {Contacto}";
    }
}