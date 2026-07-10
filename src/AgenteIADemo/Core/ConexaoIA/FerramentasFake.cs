using Microsoft.Extensions.AI;
using System.ComponentModel;

namespace AplicacaoAgenteIA.Core.ConexaoIA;

internal enum GeneroDeJogo
{
	Acao,
	Aventura,
	Fps,
	Terror,
	Sandbox
};

internal static class FerramentasFake
{

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

	public static List<AITool> ObterTodas() =>
	[
		AIFunctionFactory.Create(BuscarJogosMaisVendidos),
	];
}

