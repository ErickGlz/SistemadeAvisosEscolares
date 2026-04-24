using AplicacionAvisosEscolares.Models;
using AplicacionAvisosEscolares.ViewModels;

namespace AplicacionAvisosEscolares.Views;

public partial class AlumnoDetallePage : ContentPage
{
    public AlumnoDetallePage(AlumnoDTO alumno)
    {
        InitializeComponent();
        BindingContext = new AlumnoDetalleViewModel(alumno);
    }

    private async void OnBackTapped(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}