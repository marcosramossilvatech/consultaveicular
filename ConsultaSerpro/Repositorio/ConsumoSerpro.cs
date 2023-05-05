using ConsultaSerpro.Models.Servico;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Runtime.ConstrainedExecution;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace ConsultaSerpro.Repositorio
{
    public class ConsumoSerpro : IConsumoSerpro
    {
        private IConfiguration _configuration;
        private HttpClient _httpClient;
        private HttpClientHandler _clientHandler;
        private static  ILogger<ConsumoSerpro> _logger;
        private static string _Certificado;
        private static string _CertificadoSenha;
        //private static IWebHostEnvironment _env;
        public ConsumoSerpro(IConfiguration configuration, ILogger<ConsumoSerpro> logger)
        {
            _configuration = configuration;
            _clientHandler = new HttpClientHandler();
            _logger = logger;
            _Certificado = _configuration.GetSection("AcessoApi")["certificado_name"];
            _CertificadoSenha = _configuration.GetSection("AcessoApi")["certificado_senha"];
        }

        public string RealizarConsultaAsync(string consulta)
        {
            try
            {
                string retorno = "";
                string urlBase = _configuration.GetSection("AcessoApi")["url_base"];
                string cpfConsulta = _configuration.GetSection("AcessoApi")["cpf_consulta"];
                string serial = _configuration.GetSection("AcessoApi")["certificado"];
                _clientHandler.SslProtocols = SslProtocols.Tls12;
                _clientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
                var certificado = ExtrairCertificado(serial);
                if (certificado != null)
                {
                    _clientHandler.ClientCertificates.Add(certificado);
                    HttpClient client = new HttpClient(_clientHandler);
                    Uri uri = new Uri(urlBase);
                    client.BaseAddress = uri;
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, consulta);
                    request.Headers.Clear();
                    request.Headers.Add("x-cpf-usuario", cpfConsulta);
                    string contentType = "application/json";
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));

                    HttpResponseMessage response = client.Send(request);

                    using (HttpContent content = response.Content)
                    {
                        retorno = new StreamReader(content.ReadAsStream()).ReadToEnd();
                    }
                }

                return retorno;
            }
            catch (Exception ex)
            {
                return "Error api: "+ ex.ToString();
                throw;
            }
        }
        private static X509Certificate ExtrairCertificado(string serial)
        {
            X509Store store = null;
            try
            {
                string path = System.AppDomain.CurrentDomain.BaseDirectory.ToString()+ "\\wwwroot\\certificados";
                string caminho = Path.Combine(path, _Certificado);
                var cert1 = new X509Certificate2(caminho, _CertificadoSenha);


                return cert1;
            }
            catch (Exception ex)
            {                
                return null;
            }
            finally
            {
                if (store != null) store.Close();
            }

        }
    }
}
