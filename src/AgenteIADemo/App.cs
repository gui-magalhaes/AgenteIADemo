using AplicacaoAgenteIA.Constantes;
using AplicacaoAgenteIA.Core.ConexaoIA;
using AplicacaoAgenteIA.Core.ConexaoIA.Interfaces;
using Microsoft.Extensions.AI;

namespace AplicacaoAgenteIA;
internal class App
{
	private readonly ConexaoIAFactory _factory;

	public App(ConexaoIAFactory factory)
	{
		_factory = factory;
	}

	public async Task ExecutarAsync()
	{
		using IConexaoIA conexao = _factory.CriarConexao(ProvedorIA.OpenAI, PromptsSistema.AssistenteJogos);

		Console.WriteLine("-== AgenteIADemo ==-");
		Console.WriteLine("");

		while (true)
		{
			Console.WriteLine("Seu prompt");
			string? promptUsuario = Console.ReadLine();
			conexao.AdicionarMensagemDeUsuario(promptUsuario ?? string.Empty);
			Console.WriteLine("");

			Console.WriteLine("Bob ->");
			ChatResponse resposta = await LerRespostaEmStreamAsync(conexao.ObterRespostaEmStreamAsync());

			Console.WriteLine("");
			Console.WriteLine($"[Bob usou {resposta.Usage!.TotalTokenCount} tokens para pensar nisso!]");

			Console.WriteLine("");
		}
	}

	private static async Task<ChatResponse> LerRespostaEmStreamAsync(IAsyncEnumerable<ChatResponseUpdate> atualizacoes)
	{
		List<ChatResponseUpdate> lista = [];
		await foreach (ChatResponseUpdate atualizacao in atualizacoes)
		{
			Console.Write(atualizacao.Text);
			lista.Add(atualizacao);
		}

		return lista.ToChatResponse();
	}
}

