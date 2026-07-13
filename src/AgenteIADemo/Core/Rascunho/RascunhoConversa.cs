using System.Text.Json;

namespace AplicacaoAgenteIA.Core.Rascunho;

internal class RascunhoConversa
{
	private readonly Dictionary<string, object?> _dados = [];

	public IReadOnlyDictionary<string, object?> Dados => _dados;

	public void AtualizarCampo(CampoRascunho definicao, object? valor)
	{
		if (valor is null)
		{
			_dados.Remove(definicao.Nome);
			return;
		}

		if (definicao.Comportamento == ComportamentoCampo.Acrescentar)
		{
			if (_dados.TryGetValue(definicao.Nome, out var atual) && atual is List<object?> lista)
				lista.Add(valor);
			else
				_dados[definicao.Nome] = new List<object?> { valor };
			return;
		}

		_dados[definicao.Nome] = valor;
	}

	public string ParaJson() => JsonSerializer.Serialize(_dados);
}