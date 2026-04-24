using AplicacionAvisosEscolares.Models;
using AplicacionAvisosEscolares.ViewModels;

namespace AplicacionAvisosEscolares.Views;

public partial class AlumnosPage : ContentPage
{
    public AlumnosPage()
    {
        InitializeComponent();
        BindingContext = new AlumnosViewModel();
    }

    private async void OnBackTapped(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnAlumnoTapped(object sender, EventArgs e)
    {
        var border = sender as Border;
        var alumno = border?.BindingContext as AlumnoDTO;

        if (alumno != null)
        {
            await Navigation.PushAsync(new AlumnoDetallePage(alumno));
        }
    }
}