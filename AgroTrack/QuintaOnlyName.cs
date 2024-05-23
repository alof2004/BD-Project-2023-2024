using System;
namespace AgroTrack
{
    public class QuintaOnlyName
    {
        public int Id_Quinta { get; set; }
        public string Nome { get; set; }
        public string Morada { get; set; }

        public int Empresa_Id_Empresa { get; set; }

        public int Contacto { get; set; }

        public QuintaOnlyName()
        {

        }
        public QuintaOnlyName(int id, string nome, string localizacao, int id_empresa, int contacto)
        {
            Id_Quinta = id;
            Nome = nome;
            Morada = localizacao;
            Empresa_Id_Empresa = id_empresa;
            Contacto = contacto;
        }

        public override string ToString()
        {
            return Nome;
        }
    }
}