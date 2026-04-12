using AplicacionAvisosEscolares.ViewModels;

namespace AplicacionAvisosEscolares.Views;

public partial class CrearAvisoPage : ContentPage
{
	public CrearAvisoPage()
	{
		InitializeComponent();
        BindingContext = new CrearAvisoViewModel();

    }
}