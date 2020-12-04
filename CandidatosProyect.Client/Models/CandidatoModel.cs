namespace CandidatosProyect.Client.Models
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Api.Client;
    
    public class CandidatoModel
    {
        public CandidatoModel()
        {
            this.Codigo = Guid.NewGuid();
            this.Empleos = new List<EmpleoModel>();
        }

        public CandidatoModel(Candidatos candidato) : this()
        {
            this.IdEntity = candidato.Can_Id;
            this.Nombre = candidato.Can_Nombre;
            this.Apellido = candidato.Can_Apellido;
            this.FechaNacimiento = candidato.Can_FechaNacimiento;
            this.Email = candidato.Can_Email;
            this.Telefono = candidato.Can_Telefono;
        }

        public Guid Codigo { get; set; }

        public int IdEntity { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Nombre Requerido")]
        public string Nombre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Apellido Requerido")]
        public string Apellido { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTimeOffset FechaNacimiento { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "E-Mail Requerido")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Telefono Requerido")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Debe usar Numero")]
        public long Telefono { get; set; }

        [DataType(DataType.Upload)]
        [Required(ErrorMessage = "Debe Cargar el CV")]
        public IFormFile CV { get; set; }

        public ICollection<EmpleoModel> Empleos { get; set; }
    }
}