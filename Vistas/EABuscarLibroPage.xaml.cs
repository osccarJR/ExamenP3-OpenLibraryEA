using Microsoft.Maui.Controls;
using System.IO;
using OpenLibraryEA.ViewModels;
using OpenLibraryEA.Servicios;

namespace OpenLibraryEA.Views
{
    public partial class EABuscarLibroPage : ContentPage
    {
        public EABuscarLibroPage()
        {
            InitializeComponent();
            BindingContext = new EALibroViewModel(new EALibroServicio(), new EABaseDatosServicio(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EALibroSQLite.db3")));
        }
    }
}
