using AplicacionAvisosEscolares.Models;
using AplicacionAvisosEscolares.Services.AvisosApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace AplicacionAvisosEscolares.ViewModels
{
    public class AvisosViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<AvisoDTO> Generales { get; set; } = new();
        public ObservableCollection<AvisoDTO> Personales { get; set; } = new();

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
        public Command<AvisoDTO> VerAvisoCommand { get; }

        AvisosService service;

        public AvisosViewModel()
        {
            service = new AvisosService();
            VerAvisoCommand = new Command<AvisoDTO>(async (aviso) => await VerAviso(aviso));
            Cargar();
        }

        private async Task VerAviso(AvisoDTO aviso)
        {
            int idAlumno = Preferences.Get("IdAlumno", 0);

            await App.Current.MainPage.DisplayAlert("ID", idAlumno.ToString(), "OK");
            // Marcar como leído en API
            await service.MarcarLeido(aviso.IdAviso, idAlumno);

            // Actualizar en memoria (para UI)
            aviso.FechaLeido = DateTime.Now;

            // Refrescar lista
            Cargar();
        }

        private async void Cargar()
        {
            IsLoading = true;

            int idAlumno = Preferences.Get("IdAlumno", 0);

            var lista = await service.GetAvisosAlumno(idAlumno) ?? new List<AvisoDTO>();

            Generales.Clear();
            Personales.Clear();

            foreach (var item in lista)
            {
                if (item.TipoAviso == "GENERAL")
                    Generales.Add(item);
                else
                    Personales.Add(item);
            }

            IsLoading = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
