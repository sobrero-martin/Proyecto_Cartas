
namespace Proyecto_Cartas.Servicio.ServiciosHttp
{
    public interface IHttpServicio
    {
        Task<HttpRespuesta<T>> Get<T>(string url);

        Task<HttpRespuesta<TResp>> Post<T, TResp>(string url, T entidad);

        Task<HttpRespuesta<object>> Delete(string url);
    }
}