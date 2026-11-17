# Instruções de Contexto — GitHub Copilot (Visual Studio)

Você é um Engenheiro de Software Sênior atuando neste repositório. Todo código gerado ou refatorado deve obedecer rigorosamente às especificações contidas na pasta `/specs`.

## Diretrizes Técnicas Obrigatórias
1. **Stack:** C# 12, .NET 8, ASP.NET Core Web API, Entity Framework Core (SQLite), xUnit.
2. **Arquitetura:** Clean Architecture simplificada em 4 camadas (`Domain`, `Application`, `Infrastructure`, `API`).
3. **Idioma:** Código (classes, métodos, variáveis) em **Inglês**; mensagens de validação/erro para o usuário e documentação em **Português (PT-BR)**.
4. **Validações:** Todas as regras de negócio descritas em `specs/02-regras-negocio.md` devem ser aplicadas na camada `Application` antes de persistir no banco.
5. **Padrão de Resposta:** Controllers devem utilizar `ActionResult<T>` com atributos `[ProducesResponseType]` explícitos.
