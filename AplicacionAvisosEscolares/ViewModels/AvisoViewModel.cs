using AplicacionAvisosEscolares.Models;
using AplicacionAvisosEscolares.Services;
using AplicacionAvisosEscolares.Views;
using System.Collections.ObjectModel;
using System.Timers;
using System.ComponentModel;

namespace AplicacionAvisosEscolares.ViewModels
{
    public class AvisosViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<AvisoDTO> Generales { get; set; } = new();
        public ObservableCollection<AvisoDTO> Personales { get; set; } = new();
        System.Timers.Timer timer;

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

        public bool VerGenerales { get; set; } = true;
        public bool VerPersonales { get; set; }
        public bool SoloNoLeidos { get; set; }
        public Color ColorGenerales
        {
            get
            {
                if (VerGenerales)
                    return Color.FromArgb("#1E88E5");
                else
                    return Color.FromArgb("#BBDEFB");
            }
        }
        public Color ColorPersonales
        {
            get
            {
                if (VerPersonales)
                    return Color.FromArgb("#1E88E5");
                else
                    return Color.FromArgb("#BBDEFB");
            }
        }

        public Color TextoGenerales
        {
            get
            {
                if (VerGenerales)
                    return Colors.White;
                else
                    return Colors.Black;
            }
        }
        public Color TextoPersonales
        {
            get
            {
                if (VerPersonales)
                    return Colors.White;
                else
                    return Colors.Black;
            }
        }

        public Command MostrarGeneralesCommand { get; set; }
        public Command MostrarPersonalesCommand { get; set; }
        public Command<AvisoDTO> VerAvisoCommand { get; set; }
        public Command NoLeidosCommand { get; set; }

        AvisosService service;

        public AvisosViewModel()
        {
            service = new AvisosService();
            MostrarGeneralesCommand = new Command(OnMostrarGenerales);
            MostrarPersonalesCommand = new Command(OnMostrarPersonales);

            VerAvisoCommand = new Command<AvisoDTO>(async (aviso) => await VerAviso(aviso));
            NoLeidosCommand = new Command(OnToggleNoLeidos);

            Cargar();

            timer = new System.Timers.Timer(10000);
            timer.Elapsed += async (s, e) => await AutoRefresh();
            timer.Start();
        }

        private async Task VerAviso(AvisoDTO aviso)
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
                int idAlumno = Preferences.Get("IdAlumno", 0);

                await service.MarcarLeido(aviso.IdAviso, idAlumno);

                aviso.FechaLeido = DateTime.Now;

                if (aviso.TipoAviso == "GENERAL")
                {
                    int index = Generales.IndexOf(aviso);
                    if (index >= 0)
                    {
                        Generales[index] = aviso;
                    }
                }
                else
                {
                    int index = Personales.IndexOf(aviso);
                    if (index >= 0)
                    {
                        Personales[index] = aviso;
                    }
                }
                await App.Current.MainPage.Navigation.PushAsync(new AvisoDetallePage(aviso));
            }
          
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Ocurrió un error inesperado.", "OK");
                await App.Current.MainPage.Navigation.PushAsync(new AvisoDetallePage(aviso));
            }
        }

        private async void Cargar()
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

                int idAlumno = Preferences.Get("IdAlumno", 0);

                var lista = await service.GetAvisosAlumno(idAlumno) ?? new List<AvisoDTO>();

                Generales.Clear();
                Personales.Clear();

                foreach (var item in lista)
                {
                    if (SoloNoLeidos && item.FechaLeido != null)
                        continue;

                    if (item.TipoAviso == "GENERAL")
                        Generales.Add(item);
                    else
                        Personales.Add(item);
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

        private void OnMostrarGenerales()
        {
            VerGenerales = true;
            VerPersonales = false;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VerGenerales)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VerPersonales)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ColorGenerales)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ColorPersonales)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextoGenerales)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextoPersonales)));
        }

        private void OnMostrarPersonales()
        {
            VerGenerales = false;
            VerPersonales = true;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VerGenerales)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VerPersonales)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ColorGenerales)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ColorPersonales)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextoGenerales)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextoPersonales)));
        }

        private void OnToggleNoLeidos()
        {
            SoloNoLeidos = !SoloNoLeidos;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SoloNoLeidos)));

            Cargar();
        }

        private async Task AutoRefresh()
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await RecargarSilencioso();
            });
        }

        private async Task RecargarSilencioso()
        {
            try
            {
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
            }
            catch (ApiConnectionException)
            {
                Console.WriteLine("Error de conexión");
            }
            catch (Exception)
            {
                Console.WriteLine("Error inesperado en conexión");
            }
        }
    }
}