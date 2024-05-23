﻿using System;

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

        public Encomenda()
        {
        }

        public Encomenda(int codigo, int prazoEntrega, string moradaEntrega, DateTime entrega, int retalhistaEmpresaId, int empresaDeTransportesId, int quintaEmpresaId)
        {
            Codigo = codigo;
            PrazoEntrega = prazoEntrega;
            MoradaEntrega = moradaEntrega;
            Entrega = entrega;
            RetalhistaEmpresaId = retalhistaEmpresaId;
            EmpresaDeTransportesId = empresaDeTransportesId;
            QuintaEmpresaId = quintaEmpresaId;
        }

        public override string ToString()
        {
            return $"Codigo: {Codigo}, Prazo Entrega: {PrazoEntrega}, Morada Entrega: {MoradaEntrega}, Data Entrega: {Entrega}, Retalhista Empresa ID: {RetalhistaEmpresaId}, Empresa de Transportes ID: {EmpresaDeTransportesId}, Quinta Empresa ID: {QuintaEmpresaId}";
        }
    }
}