using System;

namespace AgroTrack
{
    public class EncomendaRRetalhista
    {
        public int Codigo { get; set; }
        public int PrazoEntrega { get; set; }
        public string MoradaEntrega { get; set; }
        public DateTime Entrega { get; set; }
        public int RetalhistaEmpresaId { get; set; }

        public string NomeRetalhista { get; set; }


        public EncomendaRRetalhista()
        {
        }

        public EncomendaRRetalhista(int codigo, int prazoEntrega, string nome, string moradaEntrega, DateTime entrega, int retalhistaEmpresaId, int empresaDeTransportesId, int quintaEmpresaId)
        {
            Codigo = codigo;
            PrazoEntrega = prazoEntrega;
            MoradaEntrega = moradaEntrega;
            Entrega = entrega;
            RetalhistaEmpresaId = retalhistaEmpresaId;
            NomeRetalhista = nome;

        }

        public override string ToString()
        {
            return $"ID: {Codigo}, Morada: {MoradaEntrega}, Prazo de Entrega: {PrazoEntrega} dias ";
        }
    }
}
