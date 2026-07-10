using AplicacaoAgenteIA.Core.ConexaoIA.Interfaces;
using AplicacaoAgenteIA.Core.Contexto.Interfaces;
using Microsoft.Extensions.AI;

namespace AplicacaoAgenteIA.Core.ConexaoIA;

internal class ConexaoIA : IConexaoIA
{
	private readonly IChatClient _cliente;
	private readonly IContextoIA _contexto;
	private readonly ChatOptions _opcoes;

	public ConexaoIA(IChatClient cliente, IContextoIA contexto, List<AITool> ferramentas)
	{
		_cliente = cliente;
		_contexto = contexto;
		_opcoes = new ChatOptions { Tools = ferramentas };
	}

	public async Task<ChatResponse> ObterRespostaAsync()
	{
		ChatResponse resposta = await _cliente.GetResponseAsync(_contexto, _opcoes);
		_contexto.AdicionarResposta(resposta);
		return resposta;
	}

	public async IAsyncEnumerable<ChatResponseUpdate> ObterRespostaEmStreamAsync()
	{
		List<ChatResponseUpdate> atualizacoes = [];
		await foreach (ChatResponseUpdate item in _cliente.GetStreamingResponseAsync(_contexto, _opcoes))
		{
			atualizacoes.Add(item);
			yield return item;
		}

		_contexto.AdicionarResposta(atualizacoes.ToChatResponse());
	}

	public void AdicionarMensagemDeUsuario(string mensagem)
		=> _contexto.AdicionarUsuario(mensagem);

	public void Dispose()
	{
		_cliente.Dispose();
		GC.SuppressFinalize(this);
	}
}

