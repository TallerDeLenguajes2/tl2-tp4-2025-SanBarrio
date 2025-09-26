public class Pedido
{
    public bool Estado { get; set; }
    public Cliente Cliente { get; set; }
    public int Nro { get; set; }
    public string Obs { get; set; }
    public Cadete? CadeteAsignado { get; set; }

    public Pedido(bool estado, Cliente cliente, int nro, string obs, Cadete? cadeteAsignado = null)
    {
        Estado = estado;
        Cliente = cliente;
        Nro = nro;
        Obs = obs;
        CadeteAsignado = cadeteAsignado;
    }

    public string ObtenerInformacionCompleta()
    {
        string info = $"Pedido Nro: {Nro}\n" +
                     $"Estado: {(Estado ? "Completado" : "Pendiente")}\n" +
                     $"Observaciones: {Obs}\n" +
                     $"Cliente: {Cliente.Nombre}\n" +
                     $"Teléfono: {Cliente.Telefono}";

        if (CadeteAsignado != null)
        {
            info += $"\nCadete Asignado: {CadeteAsignado.Nombre}";
        }

        return info;
    }
}