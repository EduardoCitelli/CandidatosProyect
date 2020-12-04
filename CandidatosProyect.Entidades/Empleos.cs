namespace CandidatosProyect.Entidades
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class Empleos
    {
        [Key]
        public int emp_Id { get; set; }

        public int emp_can_Id { get; set; }

        public string emp_NombreEmpresa { get; set; }

        public DateTime emp_FechaInicio { get; set; }

        public DateTime emp_FechaFinal { get; set; }

        [ForeignKey(nameof(emp_can_Id))]
        public virtual Candidatos Candidato { get; set; }
    }
}