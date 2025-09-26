public class Cliente
{
    public string Nombre { get; set; }
    public string Direccion { get; set; }
    public int Telefono { get; set; }
    public string DatosDeReferenciaDireccion { get; set; }

    public Cliente(string nombre, string direccion, int telefono)
    {
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
        DatosDeReferenciaDireccion = "";
    }

    public Cliente(string nombre, string direccion, int telefono, string datosReferencia)
    {
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
        DatosDeReferenciaDireccion = datosReferencia;
    }
}