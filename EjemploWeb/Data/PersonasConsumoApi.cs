using EjemploWeb.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace EjemploWeb.Data
{
    public class PersonasConsumoApi
    {
        HttpClient client;

        public PersonasConsumoApi()
        {
            client = new HttpClient();
            InitHttpClient();
        }


        private void InitHttpClient()
        {
            client.BaseAddress = new Uri("http://localhost:9999/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Persona>> BuscarTodos()
        {
            List<Persona> listadoPersona = null;
            HttpResponseMessage response = await client.GetAsync("api/Personas");

            if (response.IsSuccessStatusCode)
            {
                string res = await response.Content.ReadAsStringAsync();
                listadoPersona = JsonConvert.DeserializeObject<List<Persona>>(res);

            }
            return listadoPersona;

        }

        public async Task<Persona> BuscarUno(int id)
        {
            Persona persona = null;
            HttpResponseMessage response = await client.GetAsync($"api/Personas/{id}");

            if (response.IsSuccessStatusCode)
            {
                string res = await response.Content.ReadAsStringAsync();
                persona = JsonConvert.DeserializeObject<Persona>(res);

            }
            return persona;

        }

        public async Task<int> EliminarPersona(Persona persona)
        {
            HttpResponseMessage response = await client.DeleteAsync($"api/personas/{persona.Id}");
            response.EnsureSuccessStatusCode();

            string result = await response.Content.ReadAsStringAsync();
            return Convert.ToInt32(result);

            // return await database.DeleteAsync(persona);
        }

        public async Task<int> GuardarPersona(Persona persona)
        {

            string result = string.Empty;

            if (persona.Id != 0)
            {

                HttpResponseMessage response = await client.PutAsJsonAsync($"api/Personas/{persona.Id}", persona);
                response.EnsureSuccessStatusCode();

                result = await response.Content.ReadAsStringAsync();
                return Convert.ToInt32(result);

              
            }
            else
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/Personas", persona);
                response.EnsureSuccessStatusCode();

                result = await response.Content.ReadAsStringAsync();
                return Convert.ToInt32(result);

             
            }
        }
    }
}
