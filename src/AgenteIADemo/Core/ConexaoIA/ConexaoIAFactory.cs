using Anthropic.Core;
using AplicacaoAgenteIA.Constantes;
using AplicacaoAgenteIA.Core.ConexaoIA.Interfaces;
using AplicacaoAgenteIA.Core.Contexto;
using AplicacaoAgenteIA.Core.Contexto.Interfaces;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using ClienteAnthropic = Anthropic.AnthropicClient;
using ClienteGoogle = Google.GenAI.Client;
using ClienteOpenAI = OpenAI.Chat.ChatClient;

namespace AplicacaoAgenteIA.Core.ConexaoIA;
public class ConexaoIAFactory
{
	private readonly IConfiguration _configuracao;

	public ConexaoIAFactory(IConfiguration configuracao)
	{
		_configuracao = configuracao;
	}

	public IConexaoIA CriarConexao(ProvedorIA provedor, string promptSistema)
	{
		IChatClient cliente = CriarClienteBuilder(provedor)
			.UseFunctionInvocation()
			.Build();
		IContextoIA contexto = new ContextoIA(promptSistema);

		return new ConexaoIA(cliente, contexto, FerramentasFake.ObterTodas());
	}

	private ChatClientBuilder CriarClienteBuilder(ProvedorIA provedor)
	{
		return (
			provedor switch
			{
				ProvedorIA.OpenAI => CriarClienteOpenAI(),
				ProvedorIA.Google => CriarClienteGoogle(),
				ProvedorIA.Anthropic => CriarClienteAnthropic(),
				_ => throw new Exception("Provedor não encontrado.")
			})
			.AsBuilder();
	}

	private IChatClient CriarClienteOpenAI()
	{
		string? chaveApi = _configuracao["OpenAI:ChaveApi"] ?? throw new Exception("Não encontrei a chave da API.");
		string? modelo = _configuracao["OpenAI:Modelo"] ?? throw new Exception("Não encontrei o modelo selecionado.");
		return new ClienteOpenAI(model: modelo, apiKey: chaveApi).AsIChatClient();
	}

	private IChatClient CriarClienteGoogle()
	{
		string? chaveApi = _configuracao["Google:ChaveApi"] ?? throw new Exception("Não encontrei a chave da API.");
		string? modelo = _configuracao["Google:Modelo"] ?? throw new Exception("Não encontrei o modelo selecionado.");

		return new ClienteGoogle(apiKey: chaveApi).AsIChatClient(modelo);
	}

	private IChatClient CriarClienteAnthropic()
	{
		string? chaveApi = _configuracao["Anthropic:ChaveApi"] ?? throw new Exception("Não encontrei a chave da API.");
		string? modelo = _configuracao["Anthropic:Modelo"] ?? throw new Exception("Não encontrei o modelo selecionado.");
		var opcoes = new ClientOptions
		{
			ApiKey = chaveApi,
		};

		return new ClienteAnthropic(options: opcoes).AsIChatClient(defaultModelId: modelo);
	}
}

