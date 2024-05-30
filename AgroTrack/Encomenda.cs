using System;

namespace AgroTrack
{
    public class Encomenda
    {
        public int Codigo { get; set; }
        public int PrazoEntrega { get; set; }
        public string MoradaEntrega { get; set; }
        public int RetalhistaEmpresaId { get; set; }
        public DateTime Entrega { get; set; }
        public int EmpresaDeTransportesId { get; set; }
        public int QuintaEmpresaId { get; set; }
        public string QuintaNome { get; set; }
        public string RetalhistaNome { get; set; }

        public string TransportesNome { get; set; }

        public double Preco { get; set; }



        public Encomenda()
        {
        }

        public Encomenda(int codigo, int prazoEntrega, string moradaEntrega,DateTime entrega, int retalhistaEmpresaId, int empresaDeTransportesId, int quintaEmpresaId, double preco)
        {
            Codigo = codigo;
            PrazoEntrega = prazoEntrega;
            MoradaEntrega = moradaEntrega;
            RetalhistaEmpresaId = retalhistaEmpresaId;
            EmpresaDeTransportesId = empresaDeTransportesId;
            QuintaEmpresaId = quintaEmpresaId;
            Preco = preco;
            Entrega = entrega;
        }


        public override string ToString()
        {
            return $"ID: {Codigo}, Retalhista: {RetalhistaNome}, {QuintaNome}, Prazo de Entrega: {PrazoEntrega} dias, Morada de Entrega: {MoradaEntrega}, Preço: {Preco}";
        }
    }
}
