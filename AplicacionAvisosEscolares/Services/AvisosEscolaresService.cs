using AplicacionAvisosEscolares.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace AplicacionAvisosEscolares.Services
{
    public class ApiConnectionException : Exception
    {
        public ApiConnectionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class AvisosService
    {
        private readonly string baseUrl = "https://crudeness-stinging-thirty.ngrok-free.dev";
        private readonly HttpClient client;

        public AvisosService()
        {
            var handler = new HttpClientHandler();

            client = new HttpClient(handler)
            {
                BaseAddress = new Uri(baseUrl),
                Timeout = TimeSpan.FromSeconds(30)
            };
        }

        public bool TieneInternet()
        {
            return Connectivity.Current.NetworkAccess == NetworkAccess.Internet;
        }

        private async Task<T> HandleApiCallAsync<T>(
            Func<Task<T>> apiCall,
            T defaultValue = default)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                throw new ApiConnectionException(
                    "No hay conexión a Internet.",
                    new Exception("Sin conexión"));
            }

            try
            {
                return await apiCall();
            }
            catch (TaskCanceledException ex)
            {
                throw new ApiConnectionException(
                    "La solicitud ha expirado. El servidor no responde.",
                    ex);
            }
            catch (HttpRequestException ex)
            {
                throw new ApiConnectionException(
                    "No se pudo conectar con el servidor.",
                    ex);
            }
            catch (Exception ex)
            {
                throw new ApiConnectionException(
                    "Ocurrió un error inesperado al comunicarse con el servidor.",
                    ex);
            }
        }

        public async Task<AlumnoDTO?> LoginAlumno(string matricula, string password)
        {
            return await HandleApiCallAsync(async () =>
            {
                var response = await client.PostAsJsonAsync("api/Alumnos/login", new
                {
                    Matricula = matricula,
                    Password = password
                });

                if (!response.IsSuccessStatusCode)
                    return null;

                return await response.Content.ReadFromJsonAsync<AlumnoDTO>();
            });
        }

        public async Task<MaestroDTO?> LoginMaestro(int idMaestro, string password)
        {
            return await HandleApiCallAsync(async () =>
            {
                var response = await client.PostAsJsonAsync("api/Maestros/login", new
                {
                    IdMaestro = idMaestro,
                    Password = password
                });

                if (!response.IsSuccessStatusCode)
                    return null;

                return await response.Content.ReadFromJsonAsync<MaestroDTO>();
            });
        }

        public async Task<List<AvisoDTO>?> GetAvisosAlumno(int idAlumno)
        {
            return await HandleApiCallAsync(async () =>
            {
                var response = await client.GetAsync($"api/Avisos/alumno/{idAlumno}");

                if (!response.IsSuccessStatusCode)
                    return new List<AvisoDTO>();

                return await response.Content.ReadFromJsonAsync<List<AvisoDTO>>()
                       ?? new List<AvisoDTO>();
            });
        }

        public async Task<List<AvisoDTO>?> GetAvisosMaestro(int idMaestro)
        {
            return await HandleApiCallAsync(async () =>
            {
                var response = await client.GetAsync($"api/Maestros/{idMaestro}/avisos");

                if (!response.IsSuccessStatusCode)
                    return new List<AvisoDTO>();

                return await response.Content.ReadFromJsonAsync<List<AvisoDTO>>()
                       ?? new List<AvisoDTO>();
            });
        }

        public async Task<bool> MarcarLeido(int idAviso, int idAlumno)
        {
            return await HandleApiCallAsync(async () =>
            {
                var response = await client.PutAsJsonAsync("api/Avisos/leido", new
                {
                    IdAviso = idAviso,
                    IdAlumno = idAlumno
                });

                return response.IsSuccessStatusCode;
            });
        }

        public async Task<bool> CrearAviso(CrearAvisoDTO dto)
        {
            return await HandleApiCallAsync(async () =>
            {
                var response = await client.PostAsJsonAsync("api/Avisos", dto);
                return response.IsSuccessStatusCode;
            });
        }

        public async Task<AlumnoDTO?> GetAlumnoPorMatricula(string matricula)
        {
            return await HandleApiCallAsync(async () =>
            {
                var response = await client.GetAsync($"api/alumnos/matricula/{matricula}");

                if (!response.IsSuccessStatusCode)
                    return null;

                return await response.Content.ReadFromJsonAsync<AlumnoDTO>();
            });
        }

        public async Task<bool> EliminarAviso(int idAviso)
        {
            return await HandleApiCallAsync(async () =>
            {
                var response = await client.DeleteAsync($"api/Avisos/{idAviso}");
                return response.IsSuccessStatusCode;
            });
        }

        public async Task<List<AlumnoDTO>?> GetAlumnos(int idMaestro)
        {
            return await HandleApiCallAsync(async () =>
            {
                var response = await client.GetAsync($"api/maestros/{idMaestro}/alumnos");

                if (!response.IsSuccessStatusCode)
                    return new List<AlumnoDTO>();

                return await response.Content.ReadFromJsonAsync<List<AlumnoDTO>>()
                       ?? new List<AlumnoDTO>();
            });
        }
    }
}
