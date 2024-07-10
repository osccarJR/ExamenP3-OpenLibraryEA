using OpenLibraryEA.Views;

namespace OpenLibraryEA
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(EABuscarLibroPage), typeof(EABuscarLibroPage));
            Routing.RegisterRoute(nameof(EALibroListPage), typeof(EALibroListPage));
        }
    }
}
