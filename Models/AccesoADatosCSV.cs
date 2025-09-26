using System;
using System.Collections.Generic;
using System.IO;

public class AccesoADatosCSV<T> : IAccesoADatos<T>
{
    public List<T> Cargar(string archivo)
    {
        if (!File.Exists(archivo))
        {
            return new List<T>();
        }

        var lineas = File.ReadAllLines(archivo);
        var lista = new List<T>();

        for (int i = 1; i < lineas.Length; i++)
        {
            var separate = lineas[i].Split(',');

            if (typeof(T) == typeof(Pedido) && separate.Length >= 7)
            {
                int nro = int.Parse(separate[0]);
                string obs = separate[1];
                bool estado = bool.Parse(separate[2]);

                Cliente cliente = new Cliente(
                    separate[3],
                    separate[4], 
                    int.Parse(separate[5]),
                    separate[6]
                );

                var pedido = new Pedido(estado, cliente, nro, obs, null);
                
                if (pedido is T pedidoT)
                {
                    lista.Add(pedidoT);
                }
            }
            else if (typeof(T) == typeof(Cadete) && separate.Length >= 4)
            {
                int id = int.Parse(separate[0]);
                string nombre = separate[1];
                string direccion = separate[2];
                int telefono = int.Parse(separate[3]);

                var cadete = new Cadete(id, nombre, direccion, telefono);
                
                if (cadete is T cadeteT)
                {
                    lista.Add(cadeteT);
                }
            }
        }

        return lista;
    }

    public void Guardar(List<T> datos, string ruta)
    {
        using (var writer = new StreamWriter(ruta))
        {
            if (typeof(T) == typeof(Cadete))
            {
                writer.WriteLine("Id,Nombre,Direccion,Telefono");
                foreach (var item in datos)
                {
                    if (item is Cadete cadete)
                    {
                        writer.WriteLine($"{cadete.Id},{cadete.Nombre},{cadete.Direccion},{cadete.Telefono}");
                    }
                }
            }
            else if (typeof(T) == typeof(Pedido))
            {
                writer.WriteLine("Nro,Obs,Estado,ClienteNombre,ClienteDireccion,ClienteTelefono,ClienteReferencia");
                foreach (var item in datos)
                {
                    if (item is Pedido pedido)
                    {
                        writer.WriteLine($"{pedido.Nro},{pedido.Obs},{pedido.Estado}," +
                                       $"{pedido.Cliente.Nombre},{pedido.Cliente.Direccion}," +
                                       $"{pedido.Cliente.Telefono},{pedido.Cliente.DatosDeReferenciaDireccion}");
                    }
                }
            }
        }
    }
}