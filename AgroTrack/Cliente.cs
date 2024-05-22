using System;
using System.Security.Cryptography.X509Certificates;

public class Cliente
{
    public int Pessoa_N_CartaoCidadao { get; set; };

    public Cliente()
	{
    }

    public Cliente(int cc)
    {
        Pessoa_N_CartaoCidadao = cc;
    }


    public override string ToString()
    {
        return Pessoa_N_CartaoCidadao.ToString();
    }


}
