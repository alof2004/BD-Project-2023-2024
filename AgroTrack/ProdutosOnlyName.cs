using System;
namespace AgroTrack
{
    public class ProdutosOnlyName
    {
        public int Id_Quinta { get; set; }
        public int Id_Produto { get; set; }
        public int Quantidade { get; set; }

        public string Produto { get; set; }

        public ProdutosOnlyName()
        {

        }
        public ProdutosOnlyName(int id_quinta, int id_produto, int quantidade, string nome)
        {
            Id_Quinta = id_quinta;
            Id_Produto = id_produto;
            Quantidade = quantidade;
            Produto = nome;
        }

        public override string ToString()
        {
            return Produto;
        }
    }
}