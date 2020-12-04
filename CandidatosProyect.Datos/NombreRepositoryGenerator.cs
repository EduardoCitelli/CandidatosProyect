namespace CandidatosProyect.Datos
{
    public class NombreRepositoryGenerator : INombreRepositoryGenerator
    {
        public string GetNombreRepository<T>()
        {
            var nombreClase = typeof(T).Name;
            return $"{nombreClase}Repository";
        }
    }
}