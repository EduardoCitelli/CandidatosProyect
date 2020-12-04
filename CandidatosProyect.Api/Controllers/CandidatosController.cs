namespace CandidatosProyect.Api.Controllers
{
    using CandidatosProyect.Entidades;
    using CandidatosProyect.Service;
    using Microsoft.AspNetCore.Mvc;
    using Serilog;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatosController : ControllerBase
    {
        private ICandidatosService service;
        private readonly ILogger logger;

        public CandidatosController(ILogger logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Candidatos>> GetCandidatos()
        {
            this.service = new CandidatosService();

            var lista = this.service.ObtenerTodos().ToList();

            return lista;
        }

        [HttpGet("{id}")]
        public ActionResult<Candidatos> GetCandidato(int id)
        {
            this.service = new CandidatosService();

            var candidato = this.service.ObtenerUno(id);

            return candidato;
        }

        [HttpPost]
        public ActionResult NuevoCandidato(Candidatos candidato)
        {
            this.service = new CandidatosService();

            try
            {
                this.service.Alta(candidato);

                this.logger.Information($"Agregado Nuevo Candidato - {candidato.can_Apellido}, {candidato.can_Nombre}");

                return this.Ok();
            }
            catch (Exception ex)
            {
                throw new Exception (ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult ModificarCandidato(int id, Candidatos candidatoModificado)
        {
            this.service = new CandidatosService();

            try
            {
                var candidato = this.service.ObtenerUno(id);

                this.UpdateCandidato(candidatoModificado, candidato);

                this.EliminarEmpleosDelCandidato(candidato);

                candidato.Empleos = candidatoModificado.Empleos.ToList();

                this.service.Modificar(candidato);

                return this.Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private ActionResult EliminarEmpleosDelCandidato(Candidatos candidato)
        {
            var serviceEmpleos = new EmpleosService();

            foreach (var empleo in candidato.Empleos)
            {
                serviceEmpleos.Eliminar(empleo.emp_Id);
            }

            return this.NoContent();
        }

        private void UpdateCandidato(Candidatos candidatoModificado, Candidatos candidato)
        {
            candidato.can_Nombre = candidatoModificado.can_Nombre;
            candidato.can_Apellido = candidatoModificado.can_Apellido;
            candidato.can_Email = candidatoModificado.can_Email;
            candidato.can_Telefono = candidatoModificado.can_Telefono;
            candidato.can_FechaNacimiento = candidatoModificado.can_FechaNacimiento;
            candidato.can_PathCV = candidatoModificado.can_PathCV;
        }

        [HttpDelete("{id}")]
        public bool EliminarCandidato(int id)
        {
            this.service = new CandidatosService();

            var candidato = this.service.ObtenerUno(id);

            try
            {
                this.EliminarEmpleosDelCandidato(candidato);

                this.service.Eliminar(id);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
