using AplicacionAvisosEscolares.ViewModels;

namespace AplicacionAvisosEscolares.Views;

public partial class AvisoMaestroPage : ContentPage
{
	public AvisoMaestroPage()
	{
		InitializeComponent();
        BindingContext = new AvisosMaestroViewModel();
    }

    private async void OnCrearAviso(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("CrearAvisoPage");
    }

    private async void OnLogoutTapped(object sender, EventArgs e)
    {
        bool ok = await DisplayAlert("Cerrar sesión", "¿Quieres salir?", "Sí", "No");
        if (!ok) return;

        Preferences.Clear();

        await Navigation.PushAsync(new LoginPage());
    }

    private async void OnAlumnosTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AlumnosPage());
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is AvisosMaestroViewModel vm)
        {
            vm.Cargar();
        }
    }

}