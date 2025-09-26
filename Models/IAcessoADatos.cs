public interface IAccesoADatos<T>
{
    List<T> Cargar(string archivo);
}