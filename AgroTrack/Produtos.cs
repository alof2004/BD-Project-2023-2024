using System;
namespace AgroTrack
{
    public class Produto
    {
        public int Id_origem { get; set; }
        public string Nome { get; set; }
        public double Preco{ get; set; }

        public double Taxa_de_iva { get; set; }

        public string Unidade_medida { get; set; }

        public string Tipo_de_Produto { get; set; }
        public int Codigo { get; set; }

        public Produto()
        {

        }
        public Produto(int idOrigem, string nome,int codigo, double preco, double iva, string medida , string tipo)
        {
            Id_origem = idOrigem;
            Codigo=codigo;
            Nome = nome;
            Preco = preco;
            Taxa_de_iva = iva;
            Unidade_medida = tipo;
            Tipo_de_Produto = tipo;
        }
        

        public override string ToString()
        {
            return Id_origem + "     -     " + Nome;
        }
    }
}