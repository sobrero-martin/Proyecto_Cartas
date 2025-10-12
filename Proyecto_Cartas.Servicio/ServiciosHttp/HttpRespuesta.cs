using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.Servicio.ServiciosHttp
{
    public class HttpRespuesta<T>
    {
        public T? Respuesta { get; }
        public bool Error { get; }
        public HttpResponseMessage HttpResponseMessage { get; set; }
        public HttpRespuesta(T? respuesta, bool error, HttpResponseMessage httpResponseMessage)
        {
            this.Respuesta = respuesta;
            this.Error = error;
            this.HttpResponseMessage = httpResponseMessage;
        }

        public string ObtenerError()
        {
            if (!Error)
            {
                return string.Empty;
            }
            else
            { 
                var statuscode = HttpResponseMessage.StatusCode;

                switch (statuscode) 
                { 
                    case HttpStatusCode.NotFound:
                        return "Recurso no encontrado";
                    case HttpStatusCode.Unauthorized:
                        return "No está logueado";
                    case HttpStatusCode.Forbidden:
                        return "No tiene autorización a ejecutar este proceso";
                    case HttpStatusCode.BadRequest:
                        return "No se pudo procesar la información";
                    case HttpStatusCode.InternalServerError:
                        return "Error interno del servidor";
                    default:
                        return $"Error en la solicitud. Código de estado: {statuscode}";
                }
            }
        }
    }
}
