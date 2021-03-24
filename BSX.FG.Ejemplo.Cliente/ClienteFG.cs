using BSX.FG.Ejemplo.Cliente.Base;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web.Script.Serialization;

namespace BSX.FG.Ejemplo.Cliente
{
    public struct ApiUrls
    {
        public const string DocumentoSimple = "/v2/api/documentos/crear/simple";

        public const string NuevoCliente = "/v2/api/clientes/nuevo";

        public const string ListaClientes = "/v2/api/clientes";

        public const string ListaDocumentos = "/v2/api/documentos";
    }

    public class ClienteFG
    {
        private const string CONTENT_TYPE = "application/json";
        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
        private static HttpClient fg;

        private string _apikey;
        private string _credentials;

        private JavaScriptSerializer _serializer = new JavaScriptSerializer();

        public ClienteFG()
        {
            fg = new HttpClient();
        }

        public OperationResult<IEnumerable<ClienteRow>> ObtenerClientes(string cualquiera)
        {
            log.Info("Consultando lista de clientes");
            var result = new OperationResult<IEnumerable<ClienteRow>>();

            try
            {
                log.Debug("Haciendo la llamada a la API");
                HttpResponseMessage response = fg.GetAsync($"{ApiUrls.ListaClientes}?cualquiera={cualquiera}").Result;

                var response_string = response.Content.ReadAsStringAsync().Result;
                if (!response.IsSuccessStatusCode)
                {
                    log.Debug(">> Se ha recibido un error");
                    var response_result = _serializer.Deserialize<ResultModel>(response_string);
                    result.MarkAsFailed(response_result.Messages);
                    result.SetErrorCode((ErrorCodeEnum)response_result.ErrorCode);
                    return result;
                }

                log.Debug(">> Se ha recibido EXITO. Continuamos con el proceso");
                var response_reult = _serializer.Deserialize<IEnumerable<ClienteRow>>(response_string);
                log.Debug(response_string);
                result.SetContent(response_reult);
            }
            catch (ArgumentException ex)
            {
                result.MarkAsFailed(ex);
            }
            catch (InvalidOperationException ex)
            {
                result.MarkAsFailed(ex);
            }
            catch (Exception ex)
            {
                result.MarkAsFailed(ex);
            }

            return result;
        }

        internal OperationResult<IEnumerable<DocumentoRow>> ObtenerDocumentos(string cualquiera)
        {
            log.Info("Consultando lista de documentos");
            var result = new OperationResult<IEnumerable<DocumentoRow>>();

            try
            {
                log.Debug("Haciendo la llamada a la API");
                HttpResponseMessage response = fg.GetAsync($"{ApiUrls.ListaDocumentos}?cualquiera={cualquiera}").Result;

                var response_string = response.Content.ReadAsStringAsync().Result;
                if (!response.IsSuccessStatusCode)
                {
                    log.Debug(">> Se ha recibido un error");
                    var response_result = _serializer.Deserialize<ResultModel>(response_string);
                    result.MarkAsFailed(response_result.Messages);
                    result.SetErrorCode((ErrorCodeEnum)response_result.ErrorCode);
                    return result;
                }

                log.Debug(">> Se ha recibido EXITO. Continuamos con el proceso");
                var response_reult = _serializer.Deserialize<IEnumerable<DocumentoRow>>(response_string);
                log.Debug(response_string);
                result.SetContent(response_reult);
            }
            catch (Exception ex)
            {
                result.MarkAsFailed(ex);
            }

            return result;
        }

        public OperationResult<int> CrearCliente(ClienteSimpleModel model)
        {
            log.Info("Creando cliente");
            var result = new OperationResult<int>();

            var content = new StringContent(_serializer.Serialize(model), Encoding.UTF8, CONTENT_TYPE);

            try
            {
                log.Debug("Haciendo la llamada a la API");
                HttpResponseMessage response = fg.PostAsync(ApiUrls.NuevoCliente, content).Result;

                var response_string = response.Content.ReadAsStringAsync().Result;
                if (!response.IsSuccessStatusCode)
                {
                    log.Debug(">> Se ha recibido un error");
                    var response_result = _serializer.Deserialize<ResultModel>(response_string);
                    result.MarkAsFailed(response_result.Messages);
                    result.SetErrorCode((ErrorCodeEnum)response_result.ErrorCode);
                    return result;
                }

                log.Debug(">> Se ha recibido EXITO. Continuamos con el proceso");
                var response_reult = _serializer.Deserialize<ClienteSimpleModel>(response_string);
                log.Debug(response_string);
                result.SetContent(response_reult.ClienteId);
            }
            catch (Exception ex)
            {
                result.MarkAsFailed(ex);
            }

            return result;
        }

        public OperationResult<string> CrearFactura(DocumentoSimpleModel model)
        {
            log.Info("Creando comprobante");
            var result = new OperationResult<string>();

            var content = new StringContent(_serializer.Serialize(model), Encoding.UTF8, CONTENT_TYPE);

            try
            {
                log.Debug("Haciendo la llamada a la API");
                HttpResponseMessage response = fg.PostAsync(ApiUrls.DocumentoSimple, content).Result;

                var response_string = response.Content.ReadAsStringAsync().Result;
                if (!response.IsSuccessStatusCode)
                {
                    log.Debug(">> Se ha recibido un error");
                    var response_result = _serializer.Deserialize<ResultModel>(response_string);
                    result.MarkAsFailed(response_result.Messages);
                    result.SetErrorCode((ErrorCodeEnum)response_result.ErrorCode);
                    return result;
                }

                log.Debug(">> Se ha recibido EXITO. Continuamos con el proceso");
                var response_reult = _serializer.Deserialize<DocumentoLocalModel>(response_string);
                log.Debug(response_string);
                result.SetContent(response_reult.DocumentoId);
            }
            catch (Exception ex)
            {
                result.MarkAsFailed(ex);
            }

            return result;
        }

        public void Inicializar(string apiUrl, string apiKey, string user = null, string pass = null)
        {
            log.Info("Iniciando cliente FG");
            log.Debug("Api URL: {0}", apiUrl);
            log.Debug("Api Key: {0}", apiKey);

            _apikey = apiKey;
            _credentials = ($"{user ?? "user"}:{pass ?? "pass"}").EncodeBase64();

            fg.BaseAddress = new Uri(apiUrl);
            fg.DefaultRequestHeaders.Accept.Clear();
            fg.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            fg.DefaultRequestHeaders.Add("ApiKey", _apikey);

            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(pass))
            {
                log.Trace("Utilizando usuario y password");
                fg.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", _credentials);
            }
        }
    }
}