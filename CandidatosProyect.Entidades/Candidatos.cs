namespace CandidatosProyect.Entidades
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Candidatos
    {
        [Key]
        public int can_Id { get; set; }

        public string can_Nombre { get; set; }

        public string can_Apellido { get; set; }

        public DateTime can_FechaNacimiento { get; set; }

        public string can_Email { get; set; }

        public long can_Telefono { get; set; }

        public string can_PathCV { get; set; }

        public virtual ICollection<Empleos> Empleos { get; set; }
    }
}