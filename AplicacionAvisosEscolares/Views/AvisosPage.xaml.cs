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
}