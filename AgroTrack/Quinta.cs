using System;
namespace AgroTrack
{
    public class Quinta
    {
        public int Id_Quinta { get; set; }
        public string Nome { get; set; }
        public string Morada { get; set; }

        public int Empresa_Id_Empresa { get; set; }

        public Quinta()
        {

        }
        public Quinta(int id, string nome, string localizacao, int id_empresa)
        {
            Id_Quinta = id;
            Nome = nome;
            Morada = localizacao;
            Empresa_Id_Empresa = id_empresa;
        }

        public override string ToString()
        {
            return Id_Quinta + "     -     " + Nome;
        }
    }
}