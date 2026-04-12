using AplicacionAvisosEscolares.Models;
using AplicacionAvisosEscolares.Services.AvisosApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

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

        public AvisosMaestroViewModel()
        {
            service = new AvisosService();
            Cargar();
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
