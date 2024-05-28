using System;

namespace AgroTrack
{
    public class Encomenda
    {
        public int Codigo { get; set; }
        public int PrazoEntrega { get; set; }
        public string MoradaEntrega { get; set; }
        public DateTime Entrega { get; set; }
        public int RetalhistaEmpresaId { get; set; }
        public int EmpresaDeTransportesId { get; set; }
        public int QuintaEmpresaId { get; set; }

        public double Preco { get; set; }



        public Encomenda()
        {
        }

        public Encomenda(int codigo, int prazoEntrega, string moradaEntrega, DateTime entrega, int retalhistaEmpresaId, int empresaDeTransportesId, int quintaEmpresaId, double preco)
        {
            Codigo = codigo;
            PrazoEntrega = prazoEntrega;
            MoradaEntrega = moradaEntrega;
            Entrega = entrega;
            RetalhistaEmpresaId = retalhistaEmpresaId;
            EmpresaDeTransportesId = empresaDeTransportesId;
            QuintaEmpresaId = quintaEmpresaId;
            Preco = preco;
        }


        public override string ToString()
        {
            return $"ID: {Codigo}, Prazo de Entrega: {PrazoEntrega} dias, Morada de Entrega: {MoradaEntrega}, Preço: {Preco}";
        }
    }
}
