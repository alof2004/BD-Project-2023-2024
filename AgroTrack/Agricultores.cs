public class Agricultores
{
    public string Id_Trabalhador { get; set; }
    public string Pessoa_N_CartaoCidadao { get; set; }
    public string Quinta_Empresa_Id_Empresa { get; set; }
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