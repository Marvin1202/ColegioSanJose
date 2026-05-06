using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ColegioSanJose.Models
{
    public class Alumno
    {
        public int AlumnoId { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }

        public string Grado { get; set; }

        public List<Expediente> Expedientes { get; set; } = new List<Expediente>();
    }
}
