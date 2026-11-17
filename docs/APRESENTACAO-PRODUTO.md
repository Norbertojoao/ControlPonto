# Apresentação do Produto: ControlPonto
**Disciplina:** Práticas DevOps em Projetos Web  
**Autor:** Norberto João Leite  
**Ferramenta:** Visual Studio 2022 + GitHub Copilot (Spec-Driven Development)

---

## Slide 1 — O Problema de Negócio
- **Cenário:** Pequenas empresas ainda controlam jornadas em planilhas manuais ou cadernos, gerando retrabalho no RH e insegurança jurídica.
- **Dor Principal:** Falta de validação em tempo real de batidas duplicadas e dificuldade para visualizar o saldo diário de horas.

---

## Slide 2 — A Solução: ControlPonto
- Sistema Web completo para registro de ponto digital com **validação instantânea de jornada**.
- **Diferenciais:**
  - Cadastro rápido de colaboradores com definição de carga horária diária.
  - Bloqueio automático de sequências inválidas (ex: registrar duas entradas seguidas).
  - Cálculo imediato de horas trabalhadas e saldo diário (horas extras ou débito).
  - Interface limpa e responsiva integrada diretamente à API.

---

## Slide 3 — Metodologia: Spec-Driven Development com GitHub Copilot
- Em vez de prompts soltos, o projeto foi guiado por **4 arquivos de especificação de contexto (`/specs`)** e instruções globais (`.github/copilot-instructions.md`):
  1. `01-visao-produto.md` (Personas e Escopo)
  2. `02-regras-negocio.md` (Regras RN01 a RN05)
  3. `03-arquitetura-dados.md` (Clean Architecture e Entidades)
  4. `04-contratos-api.md` (Rotas e Status Codes HTTP)
- **Resultado:** O GitHub Copilot gerou código aderente à arquitetura, sem alucinações de domínio e com testes unitários alinhados às regras.

---

## Slide 4 — Arquitetura Técnica & Qualidade (DevOps)
- **Backend:** .NET 8 Web API + Entity Framework Core + SQLite (zero configuração externa para rodar).
- **Frontend:** HTML5, CSS3 e JavaScript Vanilla integrado via `wwwroot`.
- **Qualidade Automatizada:** Projeto `ControlPonto.Tests` com xUnit validando todas as regras de negócio (RN01–RN05) e pipeline CI configurado no GitHub Actions.

---

## Slide 5 — Demonstração do Fluxo
1. Cadastro de um novo colaborador (Nome, Matrícula, Departamento e Carga Horária).
2. Registro sequencial: **Entrada (08:00)** → **Saída Almoço (12:00)** → **Retorno Almoço (13:00)** → **Saída (17:30)**.
3. Visualização em tempo real do **Total Trabalhado (08:30)** e **Saldo Positivo (+00:30)**.
