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
            catch (ApiConnectionException)
            {
                await App.Current.MainPage.DisplayAlert("Sin conexión", "No se puede conectar con el servidor. No se pudo eliminar el aviso.", "OK");
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
            catch (ApiConnectionException)
            {
                await App.Current.MainPage.DisplayAlert("Sin conexión", "No se pudieron cargar los avisos. Verifica tu conexión.", "OK");
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
