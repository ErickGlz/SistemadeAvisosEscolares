using AplicacionAvisosEscolares.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace AplicacionAvisosEscolares.Services
{
    namespace AvisosApp.Services
    {
        public class AvisosService
        {
            string baseUrl = "https://localhost:7277/";
            HttpClient client;

            public AvisosService()
            {
                client = new HttpClient()
                {
                    BaseAddress = new Uri(baseUrl)
                };
            }

            // 🔐 LOGIN ALUMNO
            public async Task<AlumnoDTO?> LoginAlumno(string matricula, string password)
            {
                var response = await client.PostAsJsonAsync("api/Alumnos/login", new
                {
                    Matricula = matricula,
                    Password = password
                });

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<AlumnoDTO>();
                }

                return null;
            }

            // 🔐 LOGIN MAESTRO
            public async Task<MaestroDTO?> LoginMaestro(int idMaestro, string password)
            {
                var response = await client.PostAsJsonAsync("api/Maestros/login", new
                {
                    IdMaestro = idMaestro,
                    Password = password
                });

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<MaestroDTO>();
                }

                return null;
            }

            // 📢 AVISOS DEL ALUMNO
            public async Task<List<AvisoDTO>> GetAvisosAlumno(int idAlumno)
            {
                var response = await client.GetAsync($"api/Avisos/alumno/{idAlumno}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadFromJsonAsync<List<AvisoDTO>>();
                    return json ?? new List<AvisoDTO>();
                }

                return new List<AvisoDTO>();
            }

            // 👨‍🏫 AVISOS DEL MAESTRO
            public async Task<List<AvisoDTO>> GetAvisosMaestro(int idMaestro)
            {
                var response = await client.GetAsync($"api/Maestros/{idMaestro}/avisos");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadFromJsonAsync<List<AvisoDTO>>();
                    return json ?? new List<AvisoDTO>();
                }

                return new List<AvisoDTO>();
            }

            // 👁️ MARCAR COMO LEÍDO
            public async Task<bool> MarcarLeido(int idAviso, int idAlumno)
            {
                var response = await client.PutAsJsonAsync("api/Avisos/leido", new
                {
                    IdAviso = idAviso,
                    IdAlumno = idAlumno
                });

                return response.IsSuccessStatusCode;
            }

            // ➕ CREAR AVISO (MAESTRO)
            public async Task<bool> CrearAviso(CrearAvisoDTO dto)
            {
                var response = await client.PostAsJsonAsync("api/Avisos", dto);
                return response.IsSuccessStatusCode;
            }

            public async Task<AlumnoDTO?> GetAlumnoPorMatricula(string matricula)
            {
                try
                {
                    var response = await client.GetAsync($"api/alumnos/matricula/{matricula}");

                    if (response.IsSuccessStatusCode)
                    {
                        var alumno = await response.Content.ReadFromJsonAsync<AlumnoDTO>();
                        return alumno;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
