using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using OpenLibraryEA.Modelos;
using OpenLibraryEA.Servicios;
using System.Diagnostics;
using System.Linq;

namespace OpenLibraryEA.ViewModels
{
    public class EALibroViewModel : INotifyPropertyChanged
    {
        private readonly EALibroServicio _libroServicio;
        private readonly EABaseDatosServicio _baseDatosServicio;
        public ObservableCollection<EALibro> Libros { get; }

        private string _isbn;
        public string ISBN
        {
            get => _isbn;
            set
            {
                _isbn = value;
                OnPropertyChanged();
            }
        }

        private string _titulo = string.Empty;
        public string Titulo
        {
            get => _titulo;
            set
            {
                _titulo = value;
                OnPropertyChanged();
            }
        }

        private string _autores = string.Empty;
        public string Autores
        {
            get => _autores;
            set
            {
                _autores = value;
                OnPropertyChanged();
            }
        }

        public ICommand BuscarLibroCommand { get; }
        public ICommand GuardarLibroCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public EALibroViewModel(EALibroServicio libroServicio, EABaseDatosServicio baseDatosServicio)
        {
            _libroServicio = libroServicio;
            _baseDatosServicio = baseDatosServicio;
            Libros = new ObservableCollection<EALibro>();
            BuscarLibroCommand = new Command(async () => await BuscarLibroAsync());
            GuardarLibroCommand = new Command(async () => await GuardarLibroAsync());
            CargarLibros();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task BuscarLibroAsync()
        {
            if (string.IsNullOrWhiteSpace(ISBN))
            {
                Debug.WriteLine("ISBN vacío.");
                Titulo = "ISBN no válido";
                Autores = "";
                return;
            }

            var libro = await _libroServicio.ObtenerLibroAsync(ISBN);
            if (libro != null)
            {
                Titulo = libro.Titulo;
                Autores = string.Join(", ", libro.Autores.Select(a => a.Nombre));
                Debug.WriteLine($"Libro encontrado: {Titulo}, Autores: {Autores}");
            }
            else
            {
                Debug.WriteLine("Libro no encontrado.");
                Titulo = "No encontrado";
                Autores = "";
            }
        }

        public async Task GuardarLibroAsync()
        {
            if (string.IsNullOrWhiteSpace(Titulo) || string.IsNullOrWhiteSpace(Autores))
            {
                Debug.WriteLine("Datos del libro incompletos.");
                return;
            }

            var libro = new EALibro { Titulo = Titulo, Autores = Autores.Split(", ").Select(a => new EAAutor { Nombre = a }).ToList() };
            await _baseDatosServicio.GuardarLibroAsync(libro);
            CargarLibros();
        }

        private async void CargarLibros()
        {
            var libros = await _baseDatosServicio.ObtenerLibrosAsync();
            Libros.Clear();
            foreach (var libro in libros)
            {
                Libros.Add(libro);
            }
        }
    }
}
