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
}