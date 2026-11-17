# Especificação 02: Regras de Negócio (Core Domain)

As seguintes regras são invioláveis e devem ser cobertas por testes unitários:

- **RN01 — Sequência Lógica de Marcação:**
  Um colaborador não pode registrar duas marcações consecutivas do mesmo tipo. A ordem obrigatória no mesmo dia é:
  1. `Entrada` (Início do expediente)
  2. `SaidaIntervalo` (Início do almoço/descanso)
  3. `RetornoIntervalo` (Fim do almoço/descanso)
  4. `Saida` (Fim do expediente)
  *Nota:* Jornadas contínuas sem intervalo (ex: 6h) permitem ir de `Entrada` diretamente para `Saida`.

- **RN02 — Limite Diário de Marcações:**
  Cada colaborador pode realizar no máximo **4 marcações** por dia civil (`DateOnly`).

- **RN03 — Colaborador Inativo:**
  Colaboradores com `Ativo = false` estão impedidos de registrar novas marcações de ponto.

- **RN04 — Cálculo de Horas Trabalhadas:**
  - Com intervalo completo (4 batidas): `(SaidaIntervalo - Entrada) + (Saida - RetornoIntervalo)`.
  - Sem intervalo (2 batidas): `(Saida - Entrada)`.
  - Batidas ímpares (jornada em andamento): calcula o tempo decorrido até a última batida registrada.

- **RN05 — Cálculo de Banco de Horas (Saldo Diário):**
  `Saldo = TotalHorasTrabalhadas - CargaHorariaDiariaMinutos`.
  Valores positivos indicam horas extras; valores negativos indicam débito de horas.
