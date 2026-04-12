using AplicacionAvisosEscolares.Services.AvisosApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AplicacionAvisosEscolares.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public string Matricula { get; set; }
        public string Password { get; set; }

        public Command LoginCommand { get; }

        AvisosService service;

        public event PropertyChangedEventHandler? PropertyChanged;

        public LoginViewModel()
        {
            service = new AvisosService();
            LoginCommand = new Command(async () => await Login());
        }

        private async Task Login()
        {
            var alumno = await service.LoginAlumno(Matricula, Password);

            if (alumno != null)
            {
                Preferences.Set("TipoUsuario", "Alumno");
                Preferences.Set("IdAlumno", alumno.IdAlumno);

                await Shell.Current.GoToAsync("AvisosPage");
                return;
            }

            if (int.TryParse(Matricula, out int idMaestro))
            {
                var maestro = await service.LoginMaestro(idMaestro, Password);

                if (maestro != null)
                {
                    Preferences.Set("TipoUsuario", "Maestro");
                    Preferences.Set("IdMaestro", maestro.IdMaestro);

                    await Shell.Current.GoToAsync("AvisoMaestroPage");
                    return;
                }
            }

            await App.Current.MainPage.DisplayAlert("Error", "Datos incorrectos", "OK");
        }
    }
}
