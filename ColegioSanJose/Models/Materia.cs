using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ColegioSanJose.Models
{
    public class Materia
    {
        public int MateriaId { get; set; }

        [Required]
        public string NombreMateria { get; set; }

        public string Docente { get; set; }

        public List<Expediente> Expedientes { get; set; } = new List<Expediente>();
    }
}
