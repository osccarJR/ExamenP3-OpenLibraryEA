using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using OpenLibraryEA.Modelos;
using System.Diagnostics;

namespace OpenLibraryEA.Servicios
{
    public class EALibroServicio
    {
        private readonly HttpClient _httpClient;

        public EALibroServicio()
        {
            _httpClient = new HttpClient();
        }

        public async Task<EALibro> ObtenerLibroAsync(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
            {
                Debug.WriteLine("ISBN vacío o nulo.");
                return null;
            }

            try
            {
                var url = $"https://openlibrary.org/api/books?bibkeys=ISBN:{isbn}&format=json&jscmd=data";
                Debug.WriteLine($"Realizando solicitud a la URL: {url}");
                var response = await _httpClient.GetStringAsync(url);
                Debug.WriteLine($"Respuesta recibida: {response}");

                var libros = JsonConvert.DeserializeObject<Dictionary<string, EALibroContenedor>>(response);

                if (libros != null && libros.TryGetValue($"ISBN:{isbn}", out var libroContenedor))
                {
                    Debug.WriteLine($"Libro encontrado: {libroContenedor.Titulo}");
                    return new EALibro
                    {
                        Titulo = libroContenedor.Titulo,
                        Autores = libroContenedor.Autores
                    };
                }
                else
                {
                    Debug.WriteLine("Libro no encontrado en la respuesta.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al obtener el libro: {ex.Message}");
            }

            return null;
        }
    }
}
