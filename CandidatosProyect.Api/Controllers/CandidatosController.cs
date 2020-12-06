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
        private readonly ICandidatosService service;

        public CandidatosController(ICandidatosService candidatosService)
        {
            this.service = candidatosService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Candidatos>> GetCandidatos()
        {
            var lista = this.service.ObtenerTodos().ToList();

            return lista;
        }

        [HttpGet("{id}")]
        public ActionResult<Candidatos> GetCandidato(int id)
        {
            var candidato = this.service.ObtenerUno(id);

            return candidato;
        }

        [HttpPost]
        public ActionResult NuevoCandidato(Candidatos candidato)
        {
            try
            {
                this.service.Alta(candidato);

                Log.Information($"Agregado Nuevo Candidato - {candidato.can_Apellido}, {candidato.can_Nombre}");

                return this.Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw new Exception (ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult ModificarCandidato(int id, Candidatos candidatoModificado)
        {
            try
            {
                var candidato = this.service.ObtenerUno(id);

                this.UpdateCandidato(candidatoModificado, candidato);

                this.EliminarEmpleosDelCandidato(candidato);

                candidato.Empleos = candidatoModificado.Empleos.ToList();

                this.service.Modificar(candidato);

                Log.Information($"Modificado el Candidato - {candidatoModificado.can_Apellido}, {candidatoModificado.can_Nombre}");

                return this.Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
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

            return this.Ok();
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
        public ActionResult EliminarCandidato(int id)
        {
            try
            {
                this.service.Eliminar(id);

                Log.Information($"Candidato Eliminado");

                return this.Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
