using AplicacionAvisosEscolares.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AplicacionAvisosEscolares.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string matricula;
        public string Matricula
        {
            get => matricula;
            set
            {
                matricula = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Matricula)));
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));
            }
        }

        private bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set
            {
                isBusy = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsBusy)));
            }
        }

        public Command LoginCommand { get; }

        private readonly AvisosService service;

        public LoginViewModel()
        {
            service = new AvisosService();
            LoginCommand = new Command(async () => await Login());
        }

        private async Task Login()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

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
                        Preferences.Set("Grupo", maestro.Grupo);

                        await Shell.Current.GoToAsync("AvisoMaestroPage");
                        return;
                    }
                }

                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Datos incorrectos",
                    "Aceptar");
            }
            catch (ApiConnectionException)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Sin conexión",
                    "No hay conexión a Internet.",
                    "Aceptar");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    ex.Message,
                    "Aceptar");
            }
            finally
            {
                IsBusy = false;
            }
            IsBusy = false;
        }
    }
}