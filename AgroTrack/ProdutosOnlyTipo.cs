using System;
namespace AgroTrack
{
    public class ProdutosOnlyTipo
    {
        public int Id_Quinta { get; set; }
        public int Id_Produto { get; set; }
        public int Quantidade { get; set; }

        public string Tipo_de_Produto { get; set; }

        public string Produto { get; set; }

        public ProdutosOnlyTipo()
        {

        }
        public ProdutosOnlyTipo(int id_quinta, int id_produto,string tipo, int quantidade, string nome)
        {
            Id_Quinta = id_quinta;
            Id_Produto = id_produto;
            Quantidade = quantidade;
            Produto = nome;
            Tipo_de_Produto = tipo;
        }

        public override string ToString()
        {
            return Tipo_de_Produto;
        }
    }
}
