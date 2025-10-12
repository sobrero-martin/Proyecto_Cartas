using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Proyecto_Cartas.Servicio.ServiciosHttp
{
    public class HttpServicio : IHttpServicio
    {
        private readonly HttpClient http;
        public HttpServicio(HttpClient http)
        {
            this.http = http;
        }

        public async Task<HttpRespuesta<T>> Get<T>(string url)
        {
            var response = await http.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var respuesta = await DesSerializar<T>(response);
                return new HttpRespuesta<T>(respuesta, false, response);
            }
            else
            {
                return new HttpRespuesta<T>(default, true, response);
            }
        }

        public async Task<HttpRespuesta<TResp>> Post<T, TResp>(string url, T entidad)
        {
            var JsonAEnviar = JsonSerializer.Serialize(entidad);
            var contenido = new StringContent(JsonAEnviar, Encoding.UTF8, "application/json");

            var response = await http.PostAsync(url, contenido);
            if (response.IsSuccessStatusCode)
            {
                var respuesta = await DesSerializar<TResp>(response);
                return new HttpRespuesta<TResp>(respuesta, false, response);
            }
            else
            {
                return new HttpRespuesta<TResp>(default, true, response);
            }
        }

        public async Task<HttpRespuesta<object>> Delete(string url)
        {
            var respuesta = await http.DeleteAsync(url);
            return new HttpRespuesta<object>(null, !respuesta.IsSuccessStatusCode, respuesta);
        }

        private async Task<T?> DesSerializar<T>(HttpResponseMessage response)
        {
            /*
            if (typeof(T) == typeof(string))
            {
                var str = await response.Content.ReadAsStringAsync();
                return (T)(object)str;
            }*/

            var respStr = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(respStr,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }
    }
}
