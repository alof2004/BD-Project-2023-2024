using System;
namespace AgroTrack
{
    public class Item
    {
        public int EncomendaCodigo { get; set; }
        public int Quantidade { get; set; }

        public int ProdutoId { get; set; }

        public String ProdutoNome { get; set; }


        public Item()
        {

        }

        public override string ToString()
        {
            return ProdutoNome + "  -  " + Quantidade;
        }
    }
}