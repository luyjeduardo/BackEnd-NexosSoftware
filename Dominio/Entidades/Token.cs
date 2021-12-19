
namespace Dominio.Entidades
{
    public class Token
    {
        private string _Usuario;
        private string _Contrasenia;
        public string Usuario
        {
            get
            {
                return _Usuario;
            }
            set
            {
                _Usuario = value;
            }
        }
        public string Contrasenia
        {
            get
            {
                return _Contrasenia;
            }
            set
            {
                _Contrasenia = value;
            }
        }
    }
}
