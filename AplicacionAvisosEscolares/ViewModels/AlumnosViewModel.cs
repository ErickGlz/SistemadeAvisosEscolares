using AplicacionAvisosEscolares.Models;
using AplicacionAvisosEscolares.Services.AvisosApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace AplicacionAvisosEscolares.ViewModels
{
    public class AlumnosViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<AlumnoDTO> Alumnos { get; set; } = new();

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
        public string Grupo { get; set; }

        AvisosService service;

        public AlumnosViewModel()
        {
            service = new AvisosService();
            Grupo = Preferences.Get("Grupo", "");
            Cargar();
        }

        public async void Cargar()
        {
            IsLoading = true;

            int idMaestro = Preferences.Get("IdMaestro", 0);

            var lista = await service.GetAlumnos(idMaestro);

            Alumnos.Clear();

            foreach (var item in lista)
            {
                Alumnos.Add(item);
            }

            IsLoading = false;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
