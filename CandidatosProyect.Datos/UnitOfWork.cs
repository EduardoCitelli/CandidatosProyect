namespace CandidatosProyect.Datos
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly Contexto context = new Contexto(new DbContextOptionsBuilder<Contexto>()
                                              .UseSqlServer(@"Server=EDU-PC\EDU;Database=CandidatosProyect;Trusted_Connection=True;")
                                              .Options);

        private readonly NombreRepositoryGenerator nombreRepositoryGenerator;

        private readonly Dictionary<string, object> repositories;

        private bool Disposed = false;

        public UnitOfWork()
        {
            this.nombreRepositoryGenerator = new NombreRepositoryGenerator();
            this.repositories = new Dictionary<string, object>();
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            var nombre = this.nombreRepositoryGenerator.GetNombreRepository<T>();

            if (!this.repositories.ContainsKey(nombre))
            {
                var repo = this.CreateRepository<T>(nombre);
                this.repositories.Add(nombre, repo);
            }

            return this.repositories[nombre] as IRepository<T>;
        }

        protected virtual IRepository<T> CreateRepository<T>(string nombre) where T : class => new Repository<T>(this.context);

        public void Save() => this.context.SaveChanges();

        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.Disposed)
                if (disposing)
                    this.context.Dispose();

            this.Disposed = true;
        }
    }
}