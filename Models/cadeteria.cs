using System.Linq;

public class Cadeteria
{
    public string Nombre { get; private set; }
    public int Telefono { get; private set; }
    public List<Cadete> ListadoCadetes { get; private set; }
    public List<Pedido> ListadoPedidos { get; private set; }

    public Cadeteria(string nombre, int telefono, List<Cadete> cadetes)
    {
        Nombre = nombre;
        Telefono = telefono;
        ListadoCadetes = cadetes ?? new List<Cadete>();
        ListadoPedidos = new List<Pedido>();
    }

    public bool CrearPedido(bool estado, string nombreCliente, string direccionCliente,
                          int telefonoCliente, int nroPedido, string observaciones)
    {
        var cliente = new Cliente(nombreCliente, direccionCliente, telefonoCliente);
        var nuevoPedido = new Pedido(estado, cliente, nroPedido, observaciones);

        ListadoPedidos.Add(nuevoPedido);
        return true;
    }
    public bool AsignarCadeteAPedido(int idCadete, int idPedido)
    {
        var pedido = ListadoPedidos.FirstOrDefault(p => p.Nro == idPedido);
        var cadete = ListadoCadetes.FirstOrDefault(c => c.Id == idCadete);

        if (pedido == null || cadete == null)
            return false;

        pedido.CadeteAsignado = cadete;
        return true;
    }
    public bool CambiarEstadoPedido(int idPedido, bool nuevoEstado)
    {
        var pedido = ListadoPedidos.FirstOrDefault(p => p.Nro == idPedido);

        if (pedido == null)
            return false;

        pedido.Estado = nuevoEstado;
        return true;
    }

    public double CalcularJornalCadete(int idCadete)
    {
        return ListadoPedidos
            .Count(p => p.CadeteAsignado?.Id == idCadete && p.Estado) * 500;
    }
    public string ObtenerInformacionPedido(int idPedido)
    {
        var pedido = ListadoPedidos.FirstOrDefault(p => p.Nro == idPedido);
        return pedido?.ObtenerInformacionCompleta() ?? "Pedido no encontrado";
    }
    public string ObtenerListaCadetes()
    {
        if (ListadoCadetes.Count == 0)
            return "No hay cadetes registrados";

        string lista = "Lista de Cadetes:\n";
        foreach (var cadete in ListadoCadetes)
        {
            lista += $"- {cadete.ObtenerInformacionCadete()}\n";
        }
        return lista;
    }
    public string ObtenerListaPedidos()
    {
        if (ListadoPedidos.Count == 0)
            return "No hay pedidos registrados";

        string lista = "Lista de Pedidos:\n";
        foreach (var pedido in ListadoPedidos)
        {
            lista += $"- Pedido {pedido.Nro}: {(pedido.Estado ? "Entregado" : "Pendiente")}\n";
        }
        return lista;
    }
    public bool ReasignarPedido(int idPedido, int idNuevoCadete)
    {
        var pedido = ListadoPedidos.FirstOrDefault(p => p.Nro == idPedido);
        var nuevoCadete = ListadoCadetes.FirstOrDefault(c => c.Id == idNuevoCadete);

        if (pedido == null || nuevoCadete == null)
            return false;

        pedido.CadeteAsignado = nuevoCadete;
        return true;
    }
    public int ObtenerProximoNumeroPedido()
    {
        if (ListadoPedidos.Count == 0)
            return 1;

        return ListadoPedidos.Max(p => p.Nro) + 1;
    }
}