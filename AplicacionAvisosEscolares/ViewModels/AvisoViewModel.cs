using AplicacionAvisosEscolares.Models;
using AplicacionAvisosEscolares.Services.AvisosApp.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

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

        public bool VerGenerales { get; set; } = true;
        public bool VerPersonales { get; set; }
        public Color ColorGenerales => VerGenerales ? Color.FromArgb("#1E88E5") : Color.FromArgb("#BBDEFB");
        public Color ColorPersonales => VerPersonales ? Color.FromArgb("#1E88E5") : Color.FromArgb("#BBDEFB");

        public Color TextoGenerales => VerGenerales ? Colors.White : Colors.Black;
        public Color TextoPersonales => VerPersonales ? Colors.White : Colors.Black;

        public Command MostrarGeneralesCommand { get; }
        public Command MostrarPersonalesCommand { get; }
        public Command<AvisoDTO> VerAvisoCommand { get; }

        AvisosService service;

        public AvisosViewModel()
        {
            service = new AvisosService();

            MostrarGeneralesCommand = new Command(() =>
            {
                VerGenerales = true;
                VerPersonales = false;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VerGenerales)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VerPersonales)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ColorGenerales)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ColorPersonales)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextoGenerales)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextoPersonales)));
            });

            MostrarPersonalesCommand = new Command(() =>
            {
                VerGenerales = false;
                VerPersonales = true;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VerGenerales)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VerPersonales)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ColorGenerales)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ColorPersonales)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextoGenerales)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextoPersonales)));
            });

            VerAvisoCommand = new Command<AvisoDTO>(async (aviso) => await VerAviso(aviso));

            Cargar();
        }

        private async Task VerAviso(AvisoDTO aviso)
        {
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