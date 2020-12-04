namespace CandidatosProyect.Datos
{
    using CandidatosProyect.Entidades;
    using Microsoft.EntityFrameworkCore;

    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }

        #region DbSet

        public virtual DbSet<Candidatos> Candidatos { get; set; }

        public virtual DbSet<Empleos> Empleos { get; set; }

        #endregion
    }
}
