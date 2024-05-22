using System;
using Microsoft.VisualBasic;
namespace AgroTrack
{
    public class Contrato
    {
        public int ID { get; set; }
        public int Id_Trabalhador { get; set; }
        public int Pessoa_N_CartaoCidadao { get; set; }

        public DateTime Date_str { get; set; }

        public DateTime Date_end { get; set; }

        public double Salario { get; set; }

        public string Descricao { get; set; }
        public Contrato()
        {

        }
        public Contrato(int id, int id_Trabalhador, int pessoa_N_CartaoCidadao, DateTime date_str, DateTime date_end, float salario, string descricao)
        {
            ID = id;
            Id_Trabalhador = id_Trabalhador;
            Pessoa_N_CartaoCidadao = pessoa_N_CartaoCidadao;
            Date_str = date_str;
            Date_end = date_end;
            Salario = salario;
            Descricao = descricao;
        } 
        public override string ToString()
        {
            return Pessoa_N_CartaoCidadao + "     -     " + Salario;
        }
    }
}