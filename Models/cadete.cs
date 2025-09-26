public class Cadete
{
    public int Id { get; private set; }
    public string Nombre { get; private set; }
    public string Direccion { get; private set; }
    public int Telefono { get; private set; }

    public Cadete(int id, string nombre, string direccion, int telefono)
    {
        Id = id;
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
    }

    public double CalcularJornal(int cantidadPedidosEntregados)
    {
        return cantidadPedidosEntregados * 500;
    }

    public string ObtenerInformacionCadete()
    {
        return $"ID: {Id}\nNombre: {Nombre}\nDirección: {Direccion}\nTeléfono: {Telefono}";
    }

    public string ObtenerDatosBasicos()
    {
        return $"Cadete {Id}: {Nombre}";
    }
}