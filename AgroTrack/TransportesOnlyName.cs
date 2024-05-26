using System;

public class TransportesOnlyName
{
    public string Nome { get; set; }
    public string Morada { get; set; }

    public int Empresa_Id_Empresa { get; set; }

    public int Contacto { get; set; }

    public TransportesOnlyName()
    {
    }

    public TransportesOnlyName(string nome, string localizacao, int id_empresa, int contacto)
    {
        Nome = nome;
        Morada = localizacao;
        Empresa_Id_Empresa = id_empresa;
        Contacto = contacto;
    }

    public override string ToString()
    {
        return Nome;
    }
}
