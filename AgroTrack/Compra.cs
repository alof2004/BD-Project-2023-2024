namespace AgroTrack{
public class Compra
{
    public int Codigo { get; set; }
    public DateTime DataCompra { get; set; }
    public int Quantidade { get; set; }
    public double Preco { get; set; }
    public int Pessoa_N_CartaoCidadao { get; set; }
    public string Tipo_de_Produto { get; set; }
    public int ID_Quinta { get; set; }

    public int Produto_codigo { get; set; }

    public string Metodo_de_pagamento { get; set; }

    public string Nome { get; set; }


    public Compra(){

    }

    public Compra(int codigo, DateTime dataCompra, int quantidade, double preco, int pessoa_N_CartaoCidadao, string tipo_de_Produto, int iD_Quinta, int produto_codigo, string metodo_de_pagamento, string nome)
        {
            Codigo = codigo;
            DataCompra = dataCompra;
            Quantidade = quantidade;
            Preco = preco;
            Pessoa_N_CartaoCidadao = pessoa_N_CartaoCidadao;
            Tipo_de_Produto = tipo_de_Produto;
            ID_Quinta = iD_Quinta;
            Produto_codigo = produto_codigo;
            Metodo_de_pagamento = metodo_de_pagamento;
            Nome = nome;
        }

        public override string ToString()
        {
            return Quantidade + " "  + Nome + " - " + DataCompra.Day +"/" + DataCompra.Month +"/" + DataCompra.Year + " - " + Preco + "€ - QuintaID " + ID_Quinta + " - Pagamento - " + Metodo_de_pagamento;
        }


    }
}