using System;
using System.Security.Cryptography.X509Certificates;

public class Cliente
{
    public int Pessoa_N_CartaoCidadao { get; set; }
    public string Nome { get; set; }
    public int Contacto { get; set; }

    public Cliente()
	{
    }

    public Cliente(int pessoa_N_CartaoCidadao, string nome, int contacto)
    {
        Pessoa_N_CartaoCidadao = pessoa_N_CartaoCidadao;
        Nome = nome;
        Contacto = contacto;
    }


    public override string ToString()
    {
        return Nome + " - " + Contacto;
    }


}
