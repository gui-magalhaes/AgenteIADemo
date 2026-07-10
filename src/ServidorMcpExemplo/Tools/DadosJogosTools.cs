using ModelContextProtocol.Server;
using System.ComponentModel;

namespace ServidorMcpExemplo.Tools;

internal enum GeneroDeJogo
{
	Acao,
	Aventura,
	Fps,
	Terror,
	Sandbox
};

[McpServerToolType]
internal class DadosJogosTools
{
	[McpServerTool(Name = "BuscarJogosMaisVendidos")]
	[Description("Busca os 5 jogos mais vendidos, opcionalmente filtrado por gênero")]
	public static List<string> BuscarJogosMaisVendidos(
	[Description("Gênero para filtrar o ranking. Omita este parâmetro para retornar o ranking geral (todos os gêneros).")] GeneroDeJogo? genero)
	{
		string textoDebug = "Consultei os 5 jogos mais vendidos";
		if (genero != null)
			textoDebug += " do gênero " + genero.ToString();

		Console.WriteLine(textoDebug);

		return [
			"Minecraft",
			"Minecraft 2: O Retorno",
			"Minecraft 3: Vida Real",
			"Minecraft 4: Espaço Sideral",
			"Minecraft 5: O Fim"
		];
	}
}

