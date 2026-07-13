using ModelContextProtocol.Server;
using ModelContextProtocol.Protocol;
using System.ComponentModel;

namespace ServidorMcpExemplo.Resources;

[McpServerResourceType]
public static class RascunhoResources
{
	[McpServerResource(UriTemplate = "resource://campos-rascunho", Name = "CamposRascunho"), Description("Contrato dos campos de rascunho aceitos")]
	public static TextResourceContents CamposRascunho()
	{
		string json = """
		{
		  "genero_preferido": { "type": "string", "description": "Gênero de jogos preferido do usuário", "behavior": "replace" },
		  "nome_usuario": { "type": "string", "description": "Nome dos usuário que está participando da conversa..", "behavior": "append" },
		  "palavra_especifica": { "type": "string", "description": "Palavra específica que o usuário quer que seja lembrado.", "behavior": "replace" }
		}
		""";

		return new TextResourceContents
		{
			Uri = "resource://campos-rascunho",
			MimeType = "application/json",
			Text = json
		};
	}
}