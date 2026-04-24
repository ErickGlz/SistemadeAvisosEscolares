using AplicacionAvisosEscolares.Models;
using AplicacionAvisosEscolares.Services.AvisosApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AplicacionAvisosEscolares.ViewModels
{
    public class CrearAvisoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public string? Titulo { get; set; }
        public string? Contenido { get; set; }
        public string TipoAviso { get; set; } = "GENERAL";
        public string? MatriculaAlumno { get; set; }

        public Command CrearCommand { get; }

        AvisosService service;

        public CrearAvisoViewModel()
        {
            service = new AvisosService();
            CrearCommand = new Command(async () => await CrearAviso());
        }

        private async Task CrearAviso()
        {
            int idMaestro = Preferences.Get("IdMaestro", 0);

            int? idAlumno = null;

            if (!string.IsNullOrEmpty(MatriculaAlumno))
            {
                var alumno = await service.GetAlumnoPorMatricula(MatriculaAlumno);

                if (alumno != null)
                {
                    idAlumno = alumno.IdAlumno;
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Matrícula no encontrada", "OK");
                    return;
                }
            }

            var aviso = new CrearAvisoDTO
            {
                Titulo = Titulo,
                Contenido = Contenido,
                IdMaestro = idMaestro,
                IdAlumno = idAlumno,
                TipoAviso = idAlumno == null ? "GENERAL" : "PERSONAL",
                FechaCaducidad = DateTime.Now.AddDays(7)
            };

            var ok = await service.CrearAviso(aviso);

            if (ok)
            {
                await App.Current.MainPage.DisplayAlert("OK", "Aviso creado", "OK");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se pudo crear", "OK");
            }
        }

    }
}
