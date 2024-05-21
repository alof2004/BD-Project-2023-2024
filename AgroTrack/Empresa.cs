using System;

namespace AgroTrack
{
    public class Empresa
    {
        public int Id_Empresa { get; set; }
        public string Nome { get; set; }
        public string Morada { get; set; }
        public int Contacto { get; set; }
        public string TipoEmpresa { get; set; }

        public Empresa()
        {
        }

        public Empresa(int id_Empresa, string nome, string morada, int contacto, int quintaEmpresaId,string tipoemp)
        {
            Id_Empresa = id_Empresa;
            Nome = nome;
            Morada = morada;
            Contacto = contacto;
            TipoEmpresa = tipoemp;


        }

        public override string ToString()
        {
            return Id_Empresa + "     -     " + Nome;
        }
    }
}
