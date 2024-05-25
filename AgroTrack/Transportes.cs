using System;

public class Transportes
{
    public string Nome { get; set; }
    public string Morada { get; set; }

    public int Empresa_Id_Empresa { get; set; }

    public int Contacto { get; set; }

    public Transportes()
    {
    }

    public Transportes(int id, string nome, string localizacao, int id_empresa, int contacto)
    {
        Nome = nome;
        Morada = localizacao;
        Empresa_Id_Empresa = id_empresa;
        Contacto = contacto;
    }

    public override string ToString()
    {
        return Empresa_Id_Empresa + " - " + Nome;
    }
}
