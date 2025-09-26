using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class AccesoADatosJSON<T> : IAccesoADatos<T>
{
    public List<T> Cargar(string archivo)
    {
        if (!File.Exists(archivo))
        {
            return new List<T>(); 
        }

        string json = File.ReadAllText(archivo);
        return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
    }

    public void Guardar(List<T> datos, string ruta)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(datos, options);
        File.WriteAllText(ruta, json);
    }
}