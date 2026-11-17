# Especificação 03: Arquitetura e Modelo de Dados

## 1. Camadas da Solução
- **ControlPonto.Domain:** Entidades puras, Enums e Interfaces de Repositório (sem dependências externas).
- **ControlPonto.Application:** Serviços de negócio, DTOs de entrada/saída e regras de cálculo.
- **ControlPonto.Infrastructure:** `AppDbContext` (EF Core + SQLite) e implementações de repositórios.
- **ControlPonto.API:** Endpoints RESTful, configuração de injeção de dependência, Swagger e arquivos estáticos (`wwwroot`).

## 2. Entidades Principais

### `Employee` (Colaborador)
| Campo | Tipo | Restrições |
| :--- | :--- | :--- |
| `Id` | `Guid` | PK, gerado automaticamente |
| `Name` | `string` | Obrigatório, máx. 120 caracteres |
| `RegistrationCode` | `string` | Obrigatório, único (Matrícula) |
| `Department` | `string` | Obrigatório, máx. 80 caracteres |
| `DailyWorkloadMinutes` | `int` | Padrão `480` (8 horas) |
| `IsActive` | `bool` | Padrão `true` |

### `TimeRecord` (Registro de Ponto)
| Campo | Tipo | Restrições |
| :--- | :--- | :--- |
| `Id` | `Guid` | PK, gerado automaticamente |
| `EmployeeId` | `Guid` | FK para `Employee` |
| `Timestamp` | `DateTime` | Data e hora exata da marcação |
| `Type` | `RecordType` | Enum (`Entrada = 1`, `SaidaIntervalo = 2`, `RetornoIntervalo = 3`, `Saida = 4`) |
| `Notes` | `string?` | Opcional, máx. 250 caracteres |
