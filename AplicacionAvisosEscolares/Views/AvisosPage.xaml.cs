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

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var aviso = e.CurrentSelection.FirstOrDefault() as AvisoDTO;

        if (aviso == null) return;

        var vm = BindingContext as AvisosViewModel;
        vm?.VerAvisoCommand.Execute(aviso);

        ((CollectionView)sender).SelectedItem = null;
    }
}