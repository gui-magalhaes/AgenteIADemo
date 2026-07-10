using Microsoft.Extensions.AI;

namespace AplicacaoAgenteIA.Core.ConexaoIA.Interfaces;

public interface IConexaoIA : IAsyncDisposable
{
	Task<ChatResponse> ObterRespostaAsync();
	IAsyncEnumerable<ChatResponseUpdate> ObterRespostaEmStreamAsync();
	void AdicionarMensagemDeUsuario(string mensagem);
}

