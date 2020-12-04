namespace CandidatosProyect.Datos
{
    public interface IUnitOfWork
    {
        void Dispose();

        IRepository<T> GetRepository<T>() where T : class;

        void Save();
    }
}