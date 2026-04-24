using AplicacionAvisosEscolares.Models;
using AplicacionAvisosEscolares.ViewModels;

namespace AplicacionAvisosEscolares.Views;

public partial class AvisosPage : ContentPage
{
	public AvisosPage()
	{
		InitializeComponent();
		BindingContext = new ViewModels.AvisosViewModel();
    }

    private async void OnLogoutTapped(object sender, EventArgs e)
    {
        bool ok = await DisplayAlert("Cerrar sesión", "¿Quieres salir?", "Sí", "No");
        if (!ok) return;

        Preferences.Clear();

        await Navigation.PushAsync(new LoginPage());
    }
}