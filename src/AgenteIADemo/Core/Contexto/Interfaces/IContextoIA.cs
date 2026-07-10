using Microsoft.Extensions.AI;

namespace AplicacaoAgenteIA.Core.Contexto.Interfaces
{
	public interface IContextoIA : IEnumerable<ChatMessage>
	{
		void AdicionarUsuario(string texto);
		void AdicionarResposta(ChatResponse resposta);
	}
}
