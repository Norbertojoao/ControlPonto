# ControlPonto API & Web — Gestão de Jornada de Trabalho

Projeto desenvolvido para a disciplina **Práticas DevOps em Projetos Web** (Senac SC - Jovem Programador), aplicando a metodologia **Spec-Driven Development (Desenvolvimento Guiado por Especificações)** com **GitHub Copilot** integrado ao **Visual Studio 2022**.

## 📋 Visão Geral do Projeto

Sistema web completo (Backend REST API em .NET 8 + Frontend responsivo) para registro e controle de ponto eletrônico corporativo, cálculo automático de jornada diária, banco de horas e prevenção de fraudes de sequência de marcação.

## 🏗️ Estrutura do Repositório

```text
├── .github/
│   └── copilot-instructions.md   # Regras globais de contexto para o GitHub Copilot
├── specs/                        # Arquivos .md de especificação de contexto (Padrão DevOps)
│   ├── 01-visao-produto.md
│   ├── 02-regras-negocio.md
│   ├── 03-arquitetura-dados.md
│   └── 04-contratos-api.md
├── docs/                         # Documentação e Apresentação do Produto
│   └── APRESENTACAO-PRODUTO.md   # Slides/Roteiro de apresentação do produto
├── src/                          # Código-fonte completo da solução (.NET 8)
│   ├── ControlPonto.Domain/
│   ├── ControlPonto.Application/
│   ├── ControlPonto.Infrastructure/
│   └── ControlPonto.API/         # API + Interface Web (wwwroot)
└── tests/                        # Testes unitários das regras de negócio (xUnit)
    └── ControlPonto.Tests/
```

## 🚀 Como Executar Localmente

### Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 ou VS Code

### Passo a Passo
1. Clone este repositório:
   ```bash
   git clone https://github.com/SEU-USUARIO/controlponto-devops-spec.git
   cd controlponto-devops-spec
   ```
2. Execute a aplicação:
   ```bash
   dotnet run --project src/ControlPonto.API
   ```
3. Acesse no navegador:
   - **Aplicação Web (Painel de Ponto e Cadastro):** `http://localhost:5000`
   - **Documentação Swagger API:** `http://localhost:5000/swagger`

4. Execute os testes automatizados:
   ```bash
   dotnet test
   ```

## 👨‍💻 Autor
- **Aluno:** Norberto João Leite
- **Curso:** Jovem Programador — Senac SC
- **Disciplina:** Práticas DevOps em Projetos Web
