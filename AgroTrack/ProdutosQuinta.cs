using System;

namespace AgroTrack
{
    public class ProdutosQuinta
    {
        public int Id_Quinta { get; set; }
        public int Id_Produto { get; set; }
        public int Quantidade { get; set; }
        public string Produto { get; set; }

        public DateTime Data { get; set; }

        public ProdutosQuinta()
        {

        }
        public ProdutosQuinta(int id_quinta, int id_produto, int quantidade, string nome, DateTime data)
        {
            Id_Quinta = id_quinta;
            Id_Produto = id_produto;
            Quantidade = quantidade;
            Produto = nome;
            Data = data;
        }
        public override string ToString()
        {
            return $"{Produto} - {Quantidade} - {Data.ToString("dd/MM/yyyy")}";
        }
    }
}
