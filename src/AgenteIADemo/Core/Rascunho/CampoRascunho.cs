namespace AplicacaoAgenteIA.Core.Rascunho;

public enum ComportamentoCampo
{
	Substituir,
	Acrescentar
}

public record CampoRascunho
{
	public required string Nome { get; init; }
	public required string Tipo { get; init; } // "string" | "integer" | "number" | "boolean" | "array"
	public required string Descricao { get; init; }
	public ComportamentoCampo Comportamento { get; init; } = ComportamentoCampo.Substituir;
}