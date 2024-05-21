using System;
namespace AgroTrack
{
    public class Produto
    {
        public int Id_Origem { get; set; }
        public string Nome { get; set; }
        public double Preco{ get; set; }

        public double Taxa_de_iva { get; set; }

        public string Unidade_medida { get; set; }

        public string Tipo_de_Produto { get; set; }

        public Quinta()
        {

        }
        public Produto(int idOrigem, string nome, double preco, double iva, string medida , string tipo)
        {
            Id_Origem = idOrigem;
            Nome = nome;
            Preco = preco;
            Taxa_de_iva = iva;
            Unidade_medida = tipo;
            Tipo_de_Produto = tipo;
        }
        

        public override string ToString()
        {
            return Id_Origem + "     -     " + Nome;
        }
    }
}