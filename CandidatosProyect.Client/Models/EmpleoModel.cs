namespace CandidatosProyect.Client.Models
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class EmpleoModel
    {
        public EmpleoModel()
        {
            this.Id = Guid.NewGuid();
        }

        [Display(Name = "Empleo Id")]
        public Guid Id { get; set; }

        [Display(Name = "Nombre Empresa")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nombre Empresa Requerido")]
        public string NombreEmpresa { get; set;}

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Inicio")]
        [Required(ErrorMessage = "Inicio Requerido")]
        public DateTime FechaInicio { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha Final")]
        [Required(ErrorMessage = "Final Requerido")]
        public DateTime FechaFinal { get; set; }

        public Guid CodigoCandidato { get; set; }
    }
}
