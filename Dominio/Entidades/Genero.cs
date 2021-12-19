using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    public class Genero
    {
        private int _GeneroId;
        private string _Nombre;
        private string _Descripcion;

        [Key]
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
        [Column(TypeName = "varchar(25)")]
        public string Nombre
        {
            get
            {
                return _Nombre;
            }
            set
            {
                if (value.Length > 1 && value.Length <= 25)
                {
                    _Nombre = value;
                }
                else
                {
                    _Nombre = "value_nullable";
                }
            }
        }

        [Column(TypeName = "varchar(50)")]
        public string Descripcion
        {
            get
            {
                return _Descripcion;
            }
            set
            {
                _Descripcion = value;
            }
        }
        public virtual List<Libro> Libros { get; set; }
        public string ValidarPropiedades()
        {
            if (_Nombre != "value_nullable")
                if (_Descripcion.Length > 0)
                    if (_Descripcion.Length <= 50)
                        return "success";
                    else
                        return "La descripción del género es opcional, sin embargo, si desea dar una descripción, " +
                               "ésta no debe pasar de 50 caracteres.";
                else
                    return "success";
            else
                return "Verifique el nombre del género, éste se comprende entre 2 y 25 caracteres..";
        }
    }
}
