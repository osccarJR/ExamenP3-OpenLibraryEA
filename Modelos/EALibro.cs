using SQLite;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OpenLibraryEA.Modelos
{
    public class EALibro
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;

        [Ignore]
        public List<EAAutor> Autores { get; set; } = new();

        public string AutoresJson
        {
            get => JsonConvert.SerializeObject(Autores);
            set => Autores = string.IsNullOrWhiteSpace(value) ? new List<EAAutor>() : JsonConvert.DeserializeObject<List<EAAutor>>(value);
        }
    }

    public class EAAutor
    {
        public string Nombre { get; set; } = string.Empty;
    }

    public class EALibroContenedor
    {
        [JsonProperty("title")]
        public string Titulo { get; set; }

        [JsonProperty("authors")]
        public List<EAAutor> Autores { get; set; }
    }
}
