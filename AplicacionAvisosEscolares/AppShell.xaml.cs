using AplicacionAvisosEscolares.Views;

namespace AplicacionAvisosEscolares
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("AvisosPage", typeof(AvisosPage));
            Routing.RegisterRoute("AvisoMaestroPage", typeof(AvisoMaestroPage));
            Routing.RegisterRoute("CrearAvisoPage", typeof(CrearAvisoPage));
            Routing.RegisterRoute("AvisoDetallePage", typeof(AvisoDetallePage));
        }
    }
}
