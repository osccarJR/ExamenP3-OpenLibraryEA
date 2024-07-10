using Microsoft.Maui.Controls;
using System.IO;
using OpenLibraryEA.ViewModels;
using OpenLibraryEA.Servicios;

namespace OpenLibraryEA.Views
{
    public partial class EALibroListPage : ContentPage
    {
        public EALibroListPage()
        {
            InitializeComponent();
            BindingContext = new EALibroViewModel(new EALibroServicio(), new EABaseDatosServicio(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EALibroSQLite.db3")));
        }
    }
}
