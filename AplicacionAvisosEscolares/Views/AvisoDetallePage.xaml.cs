using AplicacionAvisosEscolares.Models;

namespace AplicacionAvisosEscolares.Views;

public partial class AvisoDetallePage : ContentPage
{
    public AvisoDetallePage(AvisoDTO aviso)
    {
        InitializeComponent();
        BindingContext = aviso;
    }

    private async void OnBackTapped(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}