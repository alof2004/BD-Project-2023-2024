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

    public Agricultores(int id_Trabalhador, int pessoa_N_CartaoCidadao, int quinta_Empresa_Id_Empresa, string nome, int contacto)
    {
        Id_Trabalhador = id_Trabalhador;
        Pessoa_N_CartaoCidadao = pessoa_N_CartaoCidadao;
        Quinta_Empresa_Id_Empresa = quinta_Empresa_Id_Empresa;
        Nome = nome;
        Contacto = contacto;
    }

    public override string ToString()
    {
        return $"{Nome}  {Contacto,20}";
    }
}