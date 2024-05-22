using System;
namespace AgroTrack
{
    public class Planta
    {
        public int Id { get; set; }
        public string Tipo { get; set; }

        public string Estacao { get; set; }


        public string Lote { get; set; }

        public Planta()
        {

        }
        public Planta(int id, string estacao, string tipo, string lote)
        {
            Id = id;
            Estacao = estacao;
            Tipo = tipo;
            Lote = lote;
        }

        public override string ToString()
        {
            return Tipo + "  -  " + Lote;
        }
    }
}