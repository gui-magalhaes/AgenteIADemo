# AgenteIADemo
Demo de um agente de IA que interage com o usuário e responde a perguntas em tempo real.  
Feito para estudo e demonstração de conceitos de integração com APIs de IA em .NET, utilizando a biblioteca Microsoft.Extensions.AI (MEAI).

### Funcionalidades demonstradas
- Integração com APIs de IA diversas (OpenAI, Google AI, Anthropic), utilizando as abstrações do Microsoft.Extensions.AI
- Encurtamento de contexto utilizando janela deslizante (sliding window) de turnos de conversa
- Espaço de rascunho para o agente armazenar informações importantes durante a conversa
    - Com campos definidos externamente à ferramenta, para que a escolha dos campos disponíveis seja independente do agente
