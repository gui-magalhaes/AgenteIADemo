using AplicacaoAgenteIA.Core.Contexto.Interfaces;
using AplicacaoAgenteIA.Core.Rascunho;
using Microsoft.Extensions.AI;
using System.Collections;

namespace AplicacaoAgenteIA.Core.Contexto;

internal class ContextoIA : IContextoIA
{
	private const int TamanhoDaJanelaEmTurnos = 6;

	private readonly string _promptSistema;
	private readonly List<ChatMessage> _historico = [];
	private readonly RascunhoConversa _rascunho;

	public ContextoIA(string promptSistema, RascunhoConversa rascunho)
	{
		_promptSistema = promptSistema;
		_rascunho = rascunho;
	}

	public void AdicionarUsuario(string texto) => _historico.Add(new ChatMessage(ChatRole.User, texto));

	public void AdicionarResposta(ChatResponse resposta) => _historico.AddRange(resposta.Messages);

	public IEnumerator<ChatMessage> GetEnumerator()
	{
		string promptComRascunho = ObterPromptSistema();
		yield return new ChatMessage(ChatRole.System, promptComRascunho);

		foreach (var mensagem in ObterMensagensNaJanela())
			yield return mensagem;
	}

	private string ObterPromptSistema()
		=> $"""
		{_promptSistema}
		

		Dados já coletados nesta conversa: 
		{_rascunho.ParaJson()}
		""";

	private IEnumerable<ChatMessage> ObterMensagensNaJanela()
	{
		int indiceInicio = 0;
		int turnosEncontrados = 0;

		for (int i = _historico.Count - 1; i >= 0; i--)
		{
			if (_historico[i].Role == ChatRole.User)
			{
				turnosEncontrados++;
				if (turnosEncontrados == TamanhoDaJanelaEmTurnos)
				{
					indiceInicio = i;
					break;
				}
			}
		}

		for (int i = indiceInicio; i < _historico.Count; i++)
			yield return _historico[i];
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

