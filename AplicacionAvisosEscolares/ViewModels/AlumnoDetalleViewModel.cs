using AplicacionAvisosEscolares.Models;
using AplicacionAvisosEscolares.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace AplicacionAvisosEscolares.ViewModels
{
    public class AlumnoDetalleViewModel : INotifyPropertyChanged
    {
        public string Nombre { get; set; }
        public string Matricula { get; set; }

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

        public AlumnoDetalleViewModel(AlumnoDTO alumno)
        {
            service = new AvisosService();

            Nombre = alumno.Nombre;
            Matricula = alumno.Matricula;

            Cargar(alumno.IdAlumno);
        }

        public async void Cargar(int idAlumno)
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

                var lista = await service.GetAvisosAlumno(idAlumno);

                Avisos.Clear();

                if(lista!= null)
                {
                    foreach (var item in lista.OrderByDescending(x => x.FechaEnvio))
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

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
