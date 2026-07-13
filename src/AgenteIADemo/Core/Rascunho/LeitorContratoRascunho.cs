using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol;
using System.Text.Json;

namespace AplicacaoAgenteIA.Core.Rascunho;

internal static class LeitorContratoRascunho
{
	private const string UriRecurso = "resource://campos-rascunho";
	private record CampoRascunhoModel(string Type, string Description, string Behavior = "replace");

	public static async Task<List<CampoRascunho>> CarregarAsync(McpClient clienteMcp)
	{
		ReadResourceResult resultado = await clienteMcp.ReadResourceAsync(UriRecurso);
		string json = resultado.Contents?[0]?.ToString() ?? 
					  throw new Exception("Resource de campos de rascunho veio vazio.");

		JsonSerializerOptions opcoes = new() { PropertyNameCaseInsensitive = true };
		var campos = JsonSerializer.Deserialize<Dictionary<string, CampoRascunhoModel>>(json, opcoes) ?? 
					 throw new Exception("Não consegui interpretar o contrato de rascunho.");

		return [.. campos.Select(par => new CampoRascunho
		{
			Nome = par.Key,
			Tipo = par.Value.Type,
			Descricao = par.Value.Description,
			Comportamento = par.Value.Behavior == "append" ? ComportamentoCampo.Acrescentar : ComportamentoCampo.Substituir
		})];
	}
}