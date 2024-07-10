using SQLite;
using System.Threading.Tasks;
using System.Collections.Generic;
using OpenLibraryEA.Modelos;

namespace OpenLibraryEA.Servicios
{
    public class EABaseDatosServicio
    {
        private readonly SQLiteAsyncConnection _database;

        public EABaseDatosServicio(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<EALibro>().Wait();
        }

        public Task<List<EALibro>> ObtenerLibrosAsync()
        {
            return _database.Table<EALibro>().ToListAsync();
        }

        public Task<int> GuardarLibroAsync(EALibro libro)
        {
            return _database.InsertAsync(libro);
        }

        public Task<int> EliminarLibroAsync(EALibro libro)
        {
            return _database.DeleteAsync(libro);
        }
    }
}
