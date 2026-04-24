using AplicacionAvisosEscolares.Models;
using AplicacionAvisosEscolares.Services.AvisosApp.Services;
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
            IsLoading = true;

            var lista = await service.GetAvisosAlumno(idAlumno);

            Avisos.Clear();

            foreach (var item in lista.OrderByDescending(x => x.FechaEnvio))
            {
                Avisos.Add(item);
            }

            IsLoading = false;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
