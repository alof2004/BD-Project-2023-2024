using System;
namespace AgroTrack
{
    public class Colheita
    {
        public int Id_Trabalhador { get; set; }
        public int Pessoa_N_CartaoCidadao { get; set; }
        public int Quinta_Empresa_Id_Empresa { get; set; }
        public double Duracao_colheita { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataColheita { get; set; }
        public int Produto_codigo { get; set; }
        public string ProdutoNome { get; set; }
        public string Unidade_de_medida { get; set; }

        public Colheita()
        {

        }
        public Colheita(int id_Trabalhador, int pessoa_N_CartaoCidadao, int quinta_Empresa_Id_Empresa, int duracao_colheita, int quantidade, DateTime dataColheita, int produto_codigo, string produtoNome, string unidade_de_medida)
        {
            Id_Trabalhador = id_Trabalhador;
            Pessoa_N_CartaoCidadao = pessoa_N_CartaoCidadao;
            Quinta_Empresa_Id_Empresa = quinta_Empresa_Id_Empresa;
            Duracao_colheita = duracao_colheita;
            Quantidade = quantidade;
            DataColheita = dataColheita;
            Produto_codigo = produto_codigo;
            ProdutoNome = produtoNome;
            Unidade_de_medida = unidade_de_medida;
        }
        public override string ToString()
        {
            return ProdutoNome + " - " + Quantidade + " - " + DataColheita;
        }
    }
}