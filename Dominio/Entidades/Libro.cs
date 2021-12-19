using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    public class Libro
    {
        private int _LibroId;
        private string _Titulo;
        private int _Anio;
        private int _GeneroId;
        private int _Numerodepaginas;
        private int _AutorId;

        [Key]
        public int LibroId
        {
            get
            {
                return _LibroId;
            }
            set
            {
                _LibroId = value;
            }
        }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Titulo
        {
            get
            {
                return _Titulo;
            }
            set
            {
                if (value.Length > 2 && value.Length <= 50)
                {
                    _Titulo = value;
                }
                else
                {
                    _Titulo = "value_nullable";
                }
            }
        }

        [Required]
        [Column(TypeName = "int")]
        public int Anio
        {
            get
            {
                return _Anio;
            }
            set
            {
                _Anio = value;
            }
        }

        [Required]
        [Column(TypeName = "int")]
        public int GeneroId
        {
            get
            {
                return _GeneroId;
            }
            set
            {
                _GeneroId = value;
            }
        }

        [Required]
        [Column(TypeName = "int")]
        public int Numerodepaginas
        {
            get
            {
                return _Numerodepaginas;
            }
            set
            {
                if (value > 0)
                {
                    _Numerodepaginas = value;
                }
                else
                {
                    _Numerodepaginas = 0;
                }
            }
        }

        [Required]
        [Column(TypeName = "int")]
        public int AutorId
        {
            get
            {
                return _AutorId;
            }
            set
            {
                _AutorId = value;
            }
        }
        public virtual Genero Genero { get; set; }
        public virtual Autor Autor { get; set; }
        public string ValidarPropiedades()
        {
            if (_Titulo != "value_nullable")
                if (_GeneroId > 0)
                    if (_Numerodepaginas > 0)
                        if (_AutorId > 0)
                            return "success";
                        else
                            return "Debe seleccionar un Autor válido para el libro.";
                    else
                        return "El número de páginas debe ser un entero positivo mayor a 0.";
                else
                    return "Debe seleccionar un Género válido para el libro.";
            else
                return "Verifique la información del título, éste se comprende entre 3 y 50 caracteres alfabéticos.";
        }
    }
}
