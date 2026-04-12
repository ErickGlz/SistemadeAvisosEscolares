using AplicacionAvisosEscolares.Models;
using AplicacionAvisosEscolares.Services.AvisosApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace AplicacionAvisosEscolares.ViewModels
{
    public class AvisosMaestroViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<AvisoDTO> Avisos { get; set; } = new();

        private bool isLoading;
        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsLoading)));
            }
        }

        AvisosService service;

        public ICommand EliminarCommand { get; }

        public AvisosMaestroViewModel()
        {
            service = new AvisosService();
            EliminarCommand = new Command<AvisoDTO>(async (aviso) => await Eliminar(aviso));
            Cargar();
        }

        private async Task Eliminar(AvisoDTO aviso)
        {
            bool confirm = await App.Current.MainPage.DisplayAlert(
                "Confirmar",
                "¿Eliminar este aviso?",
                "Sí",
                "No");

            if (!confirm) return;

            var ok = await service.EliminarAviso(aviso.IdAviso);

            if (ok)
            {
                Avisos.Remove(aviso);
                Cargar();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se pudo eliminar", "OK");
            }
        }

        private async void Cargar()
        {
            IsLoading = true;

            int idMaestro = Preferences.Get("IdMaestro", 0);

            var lista = await service.GetAvisosMaestro(idMaestro);

            Avisos.Clear();
            foreach (var item in lista)
                Avisos.Add(item);

            IsLoading = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
