using AplicacionAvisosEscolares.ViewModels;

namespace AplicacionAvisosEscolares.Views;

public partial class CrearAvisoPage : ContentPage
{
	public CrearAvisoPage()
	{
		InitializeComponent();
        BindingContext = new CrearAvisoViewModel();
    }

    private async void OnBackTapped(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}