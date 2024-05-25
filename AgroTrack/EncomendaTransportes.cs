using System;

namespace AgroTrack
{
    public class EncomendaEmpresaTransporte
    {
        public int Codigo { get; set; }
        public int PrazoEntrega { get; set; }
        public string MoradaEntrega { get; set; }
        public DateTime Entrega { get; set; }
        public int TransporteEmpresaId { get; set; }

        public string NomeEmpresa { get; set; }


        public EncomendaEmpresaTransporte()
        {
        }

        public EncomendaEmpresaTransporte(int codigo,string nome, int prazoEntrega, string moradaEntrega, DateTime entrega, int retalhistaEmpresaId, int empresaDeTransportesId, int quintaEmpresaId)
        {
            Codigo = codigo;
            PrazoEntrega = prazoEntrega;
            MoradaEntrega = moradaEntrega;
            Entrega = entrega;
            TransporteEmpresaId = empresaDeTransportesId;
            NomeEmpresa = nome;

        }

        public override string ToString()
        {
            return $"ID: {Codigo}, Entrega: {NomeEmpresa}";
        }
    }
}
