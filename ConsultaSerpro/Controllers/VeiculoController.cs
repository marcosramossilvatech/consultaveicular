using ConsultaSerpro.Models;
using ConsultaSerpro.Models.Servico;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace ConsultaSerpro.Controllers
{
    [Route("veiculo")]
    [ApiController]
    public class VeiculoController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuario;
        private readonly IConsumoSerpro _consumo;
        private readonly IDadosConsultaRepositorio _dadosConsulta;
        private readonly IRetornoMultaRepositorio _multa;
        private readonly IRetornoVeiculoRepositorio _veiculo;
        public VeiculoController(IUsuarioRepositorio usuario, IConsumoSerpro consumo, IDadosConsultaRepositorio dadosConsulta, IRetornoMultaRepositorio multa, IRetornoVeiculoRepositorio veiculo)
        {
            _usuario = usuario;
            _consumo = consumo;
            _dadosConsulta = dadosConsulta;
            _multa = multa;
            _veiculo = veiculo;
        }
        #region Documentação
        /// <summary>
        /// Consulta multas interestaduais do proprietário do veículo com a placa e Renavam e proprietário informado
        /// </summary>
        /// <param name="usuario">Usuario cadastrado</param>
        /// <param name="senha">Senha do usuario</param>
        /// <param name="servico">Id do serviço</param>
        /// <param name="cpf">Número do CPF do proprietário</param>
        /// <param name="placa">Código que identifica externamente um veículo (a nível nacional)</param>
        /// <param name="renavam">Código que identifica a inscrição no Registro Nacional de Veículos Automotores</param>
        /// <returns></returns>
        #endregion
        [HttpGet]
        [Route("infracao_por_proprietario/servico/{servico}/cpf/{cpf}/placa/{placa}/renavam/{renavam}")]
        public async Task<ActionResult<RetornoMultaResponse>> GetInfracaoProprietario([FromHeader, Required] string usuario, [FromHeader, Required] string senha, int servico, string cpf, string placa, string renavam)
        {
            try
            {
                if (servico != 3)
                    return BadRequest("Número de serviço informado, não corresponde endpoint solicitado!");

                string? retornoConsulta = "";

                var usuarioRetorno = await _usuario.RetornaUsario(usuario, senha);
                if (usuarioRetorno == null)
                    return BadRequest("Usuário ou senha inválido");
                if (usuarioRetorno.Id == 0)
                    return BadRequest("Usuário ou senha inválido");

                if (usuarioRetorno.Empresa == 0)
                    return BadRequest("Usuário não vinculado a nenhuma empresa");

                if (string.IsNullOrEmpty(cpf) || string.IsNullOrEmpty(placa) || string.IsNullOrEmpty(renavam))
                    return BadRequest("Campos obrigatorios não informado");

                long empresa = usuarioRetorno.Empresa;

                retornoConsulta = _consumo.RealizarConsultaAsync($"veiculos/multaInterestadual/cpf/{cpf}/renavam/{renavam}/placa/{placa}");
                var consulta = await _dadosConsulta.AdicionarAsync(new DadosConsulta(empresa, usuarioRetorno.Id, servico));

                if (string.IsNullOrEmpty(retornoConsulta))
                    return BadRequest("Erro ao comunicar com o Serpro!");

                if (retornoConsulta.Contains("Error api"))
                    return BadRequest(retornoConsulta);

                if (!retornoConsulta.Contains("returnCode"))
                {
                    JsonDocument document = JsonDocument.Parse(retornoConsulta);
                    JsonElement root = document.RootElement;
                    RetornoMultaResponse retornoMultaResponse = new RetornoMultaResponse();

                    retornoMultaResponse.Quantidade = root.GetProperty("quantidade").GetInt32();
                    retornoMultaResponse.QuantidadeReal = root.GetProperty("quantidadeReal").GetInt32();
                    JsonElement multas = root.GetProperty("multas");

                    List<RetornoMulta> listaMultas = JsonConvert.DeserializeObject<List<RetornoMulta>>(multas.ToString());


                    _multa.AdicionarMultasAsync(listaMultas, consulta.Id, retornoConsulta);

                    retornoMultaResponse.Multas = listaMultas;
                    return Ok(retornoMultaResponse);
                }
                else
                {
                    _multa.AdicionarMultasAsync(new List<RetornoMulta>(), consulta.Id, retornoConsulta);
                    return Ok(retornoConsulta);
                }
            }
            catch (Exception ex)
            {

                return BadRequest("Erro ao acessar api:" + ex.Message.ToString());
            }

        }

        #region Documentação
        /// <summary>
        /// Consulta veículo por proprietario, placa e renavam
        /// </summary>
        /// <param name="usuario">Usuario cadastrado</param>
        /// <param name="senha">Senha do usuario</param>
        /// <param name="servico">Id do serviço</param>
        /// <param name="cpf">Número do CPF do proprietário</param>
        /// <param name="placa">Código que identifica externamente um veículo (a nível nacional)</param>
        /// <param name="renavam">Código que identifica a inscrição no Registro Nacional de Veículos Automotores</param>
        /// <returns></returns>
        #endregion
        [HttpGet]
        [Route("veiculo_por_proprietario/servico/{servico}/cpf/{cpf}/placa/{placa}/renavam/{renavam}")]
        public async Task<ActionResult<RetornoVeiculo>> GetVeiculoProprietario([FromHeader, Required] string usuario, [FromHeader, Required] string senha, int servico, string cpf, string placa, string renavam)
        {
            try
            {
                if (servico != 1)
                    return BadRequest("Número de serviço informado, não corresponde endpoint solicitado!");

                var usuarioRetorno = await _usuario.RetornaUsario(usuario, senha);
                if (usuarioRetorno == null)
                    return BadRequest("Usuário ou senha inválido");
                if (usuarioRetorno.Id == 0)
                    return BadRequest("Usuário ou senha inválido");
                if (usuarioRetorno.Empresa == 0)
                    return BadRequest("Usuário não vinculado a nenhuma empresa");
                string retornoConsulta = "";
                if (string.IsNullOrEmpty(cpf) || string.IsNullOrEmpty(placa) || string.IsNullOrEmpty(renavam))
                    return BadRequest("Campos obrigatorios não informado");

                long empresa = usuarioRetorno.Empresa;
                retornoConsulta = _consumo.RealizarConsultaAsync($"veiculos/proprietario/cpf/{cpf}/placa/{placa}/renavam/{renavam}");
                var consulta = await _dadosConsulta.AdicionarAsync(new DadosConsulta(empresa, usuarioRetorno.Id, servico));
                if (string.IsNullOrEmpty(retornoConsulta))
                    return BadRequest("Erro ao comunicar com o Serpro!");

                if (retornoConsulta.Contains("Error api"))
                    return BadRequest(retornoConsulta);

                if (!retornoConsulta.Contains("returnCode"))
                {
                    RetornoVeiculo veiculo = JsonConvert.DeserializeObject<RetornoVeiculo>(retornoConsulta);

                    veiculo.Retorno = retornoConsulta;
                    veiculo.IdConsulta = consulta.IdEmpresa;
                    _veiculo.AdicionarVeiculoAsync(veiculo);
                     return Ok(veiculo);
                }
                else
                {
                    RetornoVeiculo veiculo = new RetornoVeiculo();
                    veiculo.Retorno = retornoConsulta;
                    veiculo.IdConsulta = consulta.IdEmpresa;
                    _veiculo.AdicionarVeiculoAsync(veiculo);
                    return Ok(retornoConsulta);
                }

            }
            catch (Exception ex)
            {

                return BadRequest("Erro ao acessar api:" + ex.Message.ToString());
            }
        }

    }
}
