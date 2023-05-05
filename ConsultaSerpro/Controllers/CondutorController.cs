using ConsultaSerpro.Models.Servico;
using ConsultaSerpro.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;

namespace ConsultaSerpro.Controllers
{
    [Route("condutor")]
    [ApiController]
    public class CondutorController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuario;
        private readonly IDadosConsultaRepositorio _dadosConsulta;
        private readonly IRetornoCNHRepositorio _cnh;
        private readonly IConsumoSerpro _consumo;
        public CondutorController(IUsuarioRepositorio usuario, IConsumoSerpro consumo, IDadosConsultaRepositorio dadosConsulta, IRetornoCNHRepositorio cnh)
        {
            _usuario = usuario;
            _consumo = consumo;
            _dadosConsulta = dadosConsulta;
            _cnh = cnh;
        }

        #region Documentação
        /// <summary>
        /// Consulta validade do número de segurança da CNH
        /// </summary>
        /// <param name="usuario">Usuario cadastrado</param>
        /// <param name="senha">Senha do usuario</param>
        /// <param name="servico">Id do serviço</param>
        /// <param name="cpf">Número do CPF do proprietário</param>
        /// <param name="registroCnh">Número gerado pela BINCO para identificar o condutor</param>
        /// <param name="numeroSeguranca">Número gerado a partir de algoritmo específico e de propriedade da Senatran, composto pelos dados individuais de cada CNH, permitindo a validação da CNH emitida</param>
        /// <returns></returns>
        #endregion
        [HttpGet]
        [Route("validar_cnh_cpf/servico/{servico}/cpf/{cpf}/registrocnh/{registroCnh}/numeroSeguranca/{numeroSeguranca}")]
        [SwaggerOperation(Summary = "Consulta validade do número de segurança da CNH")]
        public async Task<ActionResult<RetornoCNH>> GetValidarCNH([FromHeader, Required] string usuario, [FromHeader, Required] string senha, int servico, string cpf, string registroCnh, string numeroSeguranca)
        {
            try
            {
                if(servico!= 2)
                    return BadRequest("Número de serviço informado, não corresponde endpoint solicitado!");
                var usuarioRetorno = await _usuario.RetornaUsario(usuario, senha);
                if (usuarioRetorno == null)
                    return BadRequest("Usuário ou senha inválido");
                if (usuarioRetorno.Id == 0)
                    return BadRequest("Usuário ou senha inválido");
                if (usuarioRetorno.Empresa == 0)
                    return BadRequest("Usuário não vinculado a nenhuma empresa");
                string retornoConsulta = "";

                if (string.IsNullOrEmpty(cpf) || string.IsNullOrEmpty(registroCnh) || string.IsNullOrEmpty(numeroSeguranca))
                    return BadRequest("Campos obrigatorios não informado");

                long empresa = usuarioRetorno.Empresa;
                retornoConsulta = _consumo.RealizarConsultaAsync($"condutores/validacao/cpf/{cpf}/registroCnh/{registroCnh}/segurancaCnh/{numeroSeguranca}");
                var consulta = await _dadosConsulta.AdicionarAsync(new DadosConsulta(empresa, usuarioRetorno.Id, servico));

                if (string.IsNullOrEmpty(retornoConsulta))
                    return BadRequest("Erro ao comunicar com o Serpro!");

                if(retornoConsulta.Contains("Error api"))
                    return BadRequest(retornoConsulta);
                if (!retornoConsulta.Contains("returnCode"))
                {
                    RetornoCNH cnh = JsonConvert.DeserializeObject<RetornoCNH>(retornoConsulta);

                    cnh.Retorno = retornoConsulta;
                    cnh.IdConsulta = consulta.IdEmpresa;
                    _cnh.AdicionarValidacaoAsync(cnh);
                    return Ok(cnh);
                }
                else
                {
                    RetornoCNH cnh = new RetornoCNH();
                    cnh.Retorno = retornoConsulta;
                    cnh.IdConsulta = consulta.IdEmpresa;
                    _cnh.AdicionarValidacaoAsync(cnh);
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
