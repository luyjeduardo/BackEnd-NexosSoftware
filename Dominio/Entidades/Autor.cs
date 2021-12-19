using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    public class Autor
    {
        private int _AutorId;
        private string _Nombrescompletos;
        private DateTime _Fechadenacimiento;
        private string _Ciudaddeprocedencia;
        private string _Correoelectronico;
        
        [Key]
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

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Nombrescompletos
        {
            get
            {
                return _Nombrescompletos;
            }
            set
            {
                if (value.Length > 4 && value.Length <= 50)
                {
                    _Nombrescompletos = value;
                }
                else
                {
                    _Nombrescompletos = "value_nullable";
                }
            }
        }

        [Required]
        [Column(TypeName = "date")]
        public DateTime Fechadenacimiento
        {
            get
            {
                return _Fechadenacimiento;
            }
            set
            {
                _Fechadenacimiento = value;
            }
        }

        [Required]
        [Column(TypeName = "varchar(35)")]
        public string Ciudaddeprocedencia
        {
            get
            {
                return _Ciudaddeprocedencia;
            }
            set
            {
                if (value.Length > 1 && value.Length <= 35)
                {
                    _Ciudaddeprocedencia = value;
                }
                else
                {
                    _Ciudaddeprocedencia = "value_nullable";
                }
            }
        }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Correoelectronico
        {
            get
            {
                return _Correoelectronico;
            }
            set
            {
                if (value.Length > 5 && value.Length <= 50)
                {
                    _Correoelectronico = value;
                }
                else
                {
                    _Correoelectronico = "value_nullable";
                }
            }
        }
        public virtual List<Libro> Libros { get; set; }
        public string ValidarPropiedades()
        {
            if (_Nombrescompletos != "value_nullable")
                if (_Ciudaddeprocedencia != "value_nullable")
                    if (_Correoelectronico != "value_nullable")
                        return "success";
                    else
                        return "Verifique la información del correo, éste se comprende entre 6 y 50 caracteres.";
                else
                    return "Verifique la información de la ciudad de procedencia, ésta se comprende entre 2 y 35 caracteres alfabéticos.";          
            else
                return "Verifique la información de los nombres completos, éste comprende entre 5 y 50 caracteres alfabéticos.";
        }
    }
}
