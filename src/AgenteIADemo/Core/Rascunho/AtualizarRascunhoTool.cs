using Microsoft.Extensions.AI;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace AplicacaoAgenteIA.Core.Rascunho;

internal class AtualizarRascunhoTool : AIFunction
{
	private readonly RascunhoConversa _rascunho;
	private readonly Dictionary<string, CampoRascunho> _campos;

	public AtualizarRascunhoTool(RascunhoConversa rascunho, List<CampoRascunho> campos)
	{
		_rascunho = rascunho;
		_campos = campos.ToDictionary(c => c.Nome);
		JsonSchema = MontarSchema(campos);
	}

	public override string Name => "atualizar_rascunho";

	public override string Description => """
	Registra informações importantes da conversa que ainda não podem ser enviadas ao ERP.
	Envie apenas os campos que deseja atualizar (atualização parcial).
	Para limpar um campo já preenchido, envie o valor null para ele.
	""";

	public override JsonElement JsonSchema { get; }

	private static JsonElement MontarSchema(List<CampoRascunho> campos)
	{
		var propriedades = new JsonObject();
		foreach (var campo in campos)
		{
			propriedades[campo.Nome] = new JsonObject
			{
				["type"] = new JsonArray(MapearTipo(campo.Tipo), "null"),
				["description"] = campo.Descricao
			};
		}

		var schema = new JsonObject
		{
			["type"] = "object",
			["properties"] = propriedades,
			["additionalProperties"] = false
		};

		return JsonSerializer.SerializeToElement(schema);
	}

	private static string MapearTipo(string tipoContrato) => tipoContrato switch
	{
		"integer" => "integer",
		"number" => "number",
		"boolean" => "boolean",
		"array" => "array",
		_ => "string"
	};

	protected override ValueTask<object?> InvokeCoreAsync(AIFunctionArguments arguments, CancellationToken cancellationToken)
	{
		foreach (var (nomeCampo, valor) in arguments)
		{
			if (_campos.TryGetValue(nomeCampo, out var definicao))
				_rascunho.AtualizarCampo(definicao, valor);
		}

		return new ValueTask<object?>("Rascunho atualizado.");
	}
}