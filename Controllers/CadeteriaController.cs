using System.Security.AccessControl;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class CadeteriaController : ControllerBase
{
    private readonly AccesoADatosJSON<Cadete> accesoADatosJSONCadete;
    private readonly AccesoADatosJSON<Pedido> accesoADatosJSONPedido;
    public CadeteriaController()
    {
        accesoADatosJSONCadete = new AccesoADatosJSON<Cadete>();
        accesoADatosJSONPedido = new AccesoADatosJSON<Pedido>();
    }

    //Get

    /// <summary>
    /// Obtiene el Pedidos
    /// </summary>
    /// <returns>Retorna los Pedidos cargados</returns>
    [HttpGet("GetPedidos")]
    public IActionResult GetPedidos()
    {
        return Ok(accesoADatosJSONPedido.Cargar("data/pedidos.json"));
    }

    /// <summary>
    /// Obtiene los Cadetes
    /// </summary>
    /// <returns>Retorna los Cedidos cargados</returns>
    [HttpGet("GetCadetes")]
    public List<Cadete> GetCadetes()
    {
        return accesoADatosJSONCadete.Cargar("data/cadetes.json");
    }

    /// <summary>
    /// Obtiene los datos de los Pedidos y Estudiantes
    /// </summary>
    /// <returns>200 - Datos de pedidos, clientes y los cadetes</returns>
    [HttpGet("GetInforme")]
    public IActionResult GetInforme()
    {
        var cadetes = accesoADatosJSONCadete.Cargar("data/cadetes.json");
        var pedidos = accesoADatosJSONPedido.Cargar("data/pedidos.json");

        var informe = new
        {
            TotalCadetes = cadetes.Count,
            TotalPedidos = pedidos.Count,
            Cadetes = cadetes
        };

        return Ok(informe);
    }


    //Post

    /// <summary>
    /// Su funcion es agregar un pedido al JSON
    /// </summary>
    /// <param name="pedido"></param>
    /// <returns>Agregar el pedido entregado</returns>
    [HttpPost("AgregarPedido")]
    public IActionResult AgregarPedido([FromBody] Pedido pedido)
    {
        var pedidos = accesoADatosJSONPedido.Cargar("data/pedidos.json");
        pedidos.Add(pedido);
        accesoADatosJSONPedido.Guardar(pedidos, "data/pedidos.json");
        return Ok(pedido);
    }

    //Put

    /// <summary>
    /// Asignar un pedido a un cadete
    /// </summary>
    /// <param name="idPedido"></param>
    /// <param name="idCadete"></param>
    /// <returns>Retorna el pedido o id del pedido/id del cadete</returns>
    [HttpPut("AsignarPedido")]
    public IActionResult AsignarPedido(int idPedido, int idCadete)
    {
        var pedidos = accesoADatosJSONPedido.Cargar("data/pedidos.json");
        var cadetes = accesoADatosJSONCadete.Cargar("data/cadetes.json");

        var pedido = pedidos.FirstOrDefault(p => p.Nro == idPedido);
        if (pedido == null)
            return NotFound($"No se encontró el pedido Nro {idPedido}");

        var cadete = cadetes.FirstOrDefault(c => c.Id == idCadete); 
        if (cadete == null)
            return NotFound($"No se encontró el cadete Id {idCadete}");

        pedido.CadeteAsignado = cadete;
        accesoADatosJSONPedido.Guardar(pedidos, "data/pedidos.json");

        return Ok(pedido);
    }

    /// <summary>
    /// Cambia el estado del pedido con el id solicitado
    /// </summary>
    /// <param name="idPedido"></param>
    /// <param name="estado"></param>
    /// <returns>El pedido encontrado o id del pedido</returns>
    [HttpPut("CambiarEstadoPedido")]
    public IActionResult CambiarEstadoPedido(int idPedido, [FromQuery] bool estado)
    {
        var pedidos = accesoADatosJSONPedido.Cargar("data/pedidos.json");

        var pedido = pedidos.FirstOrDefault(p => p.Nro == idPedido);
        if (pedido == null)
            return NotFound($"No se encontró el pedido Nro {idPedido}");

        pedido.Estado = estado;
        accesoADatosJSONPedido.Guardar(pedidos, "data/pedidos.json");

        return Ok(pedido);
    }

    /// <summary>
    /// Cambia el cadete asignado a un pedido existente
    /// </summary>
    /// <param name="idPedido"></param>
    /// <param name="idNuevoCadete"></param>
    /// <returns>Pedido actualizado o error si no se encuentra</returns>
    [HttpPut("CambiarCadetePedido")]
    public IActionResult CambiarCadetePedido(int idPedido, int idNuevoCadete)
    {
        var pedidos = accesoADatosJSONPedido.Cargar("data/pedidos.json");
        var cadetes = accesoADatosJSONCadete.Cargar("data/cadetes.json");

        var pedido = pedidos.FirstOrDefault(p => p.Nro == idPedido);
        if (pedido == null)
            return NotFound($"No se encontró el pedido Nro {idPedido}");

        var cadete = cadetes.FirstOrDefault(c => c.Id == idNuevoCadete);
        if (cadete == null)
            return NotFound($"No se encontró el cadete Id {idNuevoCadete}");

        pedido.CadeteAsignado = cadete;

        accesoADatosJSONPedido.Guardar(pedidos, "data/pedidos.json");

        return Ok(pedido);
    }

}