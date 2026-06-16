using AplicacionAvisosEscolares.Models;
using AplicacionAvisosEscolares.Services;
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
        public event PropertyChangedEventHandler? PropertyChanged;

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

        public ICommand EliminarCommand { get; set; }

        public AvisosMaestroViewModel()
        {
            service = new AvisosService();
            EliminarCommand = new Command<AvisoDTO>(OnEliminar);
            Cargar();
        }

        private async void OnEliminar(AvisoDTO aviso)
        {
            await Eliminar(aviso);
        }

        private async Task Eliminar(AvisoDTO aviso)
        {
            try
            {
                var profiles = Connectivity.Current.ConnectionProfiles;

                if (!profiles.Contains(ConnectionProfile.WiFi) &&
                    !profiles.Contains(ConnectionProfile.Cellular))
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Sin conexión",
                        "No tienes conexión a Internet",
                        "Aceptar");

                    return;
                }
                bool confirm = await App.Current.MainPage.DisplayAlert("Confirmar", "¿Eliminar este aviso?", "Sí", "No");

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
         
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Ocurrió un error inesperado al eliminar.", "OK");
            }
        }

        public async void Cargar()
        {
            try
            {
                var profiles = Connectivity.Current.ConnectionProfiles;

                if (!profiles.Contains(ConnectionProfile.WiFi) &&
                    !profiles.Contains(ConnectionProfile.Cellular))
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Sin conexión",
                        "No tienes conexión a Internet",
                        "Aceptar");

                    return;
                }
                IsLoading = true;

                int idMaestro = Preferences.Get("IdMaestro", 0);

                var lista = await service.GetAvisosMaestro(idMaestro);

                Avisos.Clear();
                if(lista!= null)
                {
                    foreach (var item in lista)
                    {
                        Avisos.Add(item);
                    }
                }
            }
           
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Ocurrió un error inesperado al cargar los avisos.", "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
