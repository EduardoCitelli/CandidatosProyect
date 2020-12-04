namespace CandidatosProyect.Client.Controllers
{
    using CandidatosProyect.Api.Client;
    using CandidatosProyect.Client.Models;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class CandidatosController : Controller
    {
        private readonly ICandidatosClient candidatosClient;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CandidatosController(ICandidatosClient candidatosClient, IWebHostEnvironment webHostEnvironment)
        {
            this.candidatosClient = candidatosClient;
            this.webHostEnvironment = webHostEnvironment;
        }


        public async Task<ActionResult> Index()
        {
            var candidatos = await this.candidatosClient.GetCandidatosAsync();

            var model = candidatos.Select(x => new CandidatoModel(x));

            return this.View(model);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return this.View();
        }

        public ActionResult GetChildEmpleo() => this.PartialView("Empleo", new EmpleoModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CandidatoModel candidato)
        {
            if (!this.ModelState.IsValid)
                return this.View();

            try
            {
                var pathCV = Path.Combine(this.webHostEnvironment.WebRootPath, "CV");

                string filePath;

                filePath = Path.Combine(pathCV, candidato.CV.FileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await candidato.CV.CopyToAsync(fileStream);
                }

                var candidatoApi = new Candidatos()
                {
                    Can_Nombre = candidato.Nombre,
                    Can_Apellido = candidato.Apellido,
                    Can_FechaNacimiento = candidato.FechaNacimiento,
                    Can_Email = candidato.Email,
                    Can_Telefono = candidato.Telefono,
                    Can_PathCV = filePath
                };

                foreach (var empleo in candidato.Empleos)
                {
                    if (empleo == null)
                        continue;

                    var Empleo = new Empleos()
                    {
                        Emp_NombreEmpresa = empleo.NombreEmpresa,
                        Emp_FechaInicio = empleo.FechaInicio,
                        Emp_FechaFinal = empleo.FechaFinal
                    };

                    candidatoApi.Empleos.Add(Empleo);
                }

                var respuesta = await this.candidatosClient.NuevoCandidatoAsync(candidatoApi);

                return this.RedirectToAction(nameof(Index));
            }
            catch
            {
                return this.View();
            }
        }

        public ActionResult Edit(int id)
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            if (!this.ModelState.IsValid)
                return this.View();

            try
            {
                return this.RedirectToAction(nameof(Index));
            }
            catch
            {
                return this.View();
            }
        }

        public ActionResult Delete(int id)
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return this.RedirectToAction(nameof(Index));
            }
            catch
            {
                return this.View();
            }
        }
    }
}
