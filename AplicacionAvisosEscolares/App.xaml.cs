using AplicacionAvisosEscolares.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AplicacionAvisosEscolares
{
    public partial class App : Application
    {
        private bool mostrandoMensaje = false;

        public App()
        {
            InitializeComponent();

        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}