# Especificação 01: Visão do Produto e Contexto

## 1. Problema
Pequenas e médias empresas enfrentam dificuldades para controlar horários de entrada, intervalos e saída de colaboradores, gerando inconsistências no fechamento de folha e cálculo manual propenso a erros de banco de horas.

## 2. Solução Proposta
O **ControlPonto** é uma aplicação web leve, auditável e intuitiva que permite aos colaboradores registrarem suas batidas de ponto em tempo real e aos gestores acompanharem o saldo de horas trabalhadas diárias.

## 3. Personas
- **Colaborador:** Registra entrada, saída para almoço, retorno do almoço e saída final; consulta seu histórico do dia e saldo de horas.
- **Gestor / RH:** Cadastra colaboradores, define a carga horária diária padrão (ex: 8h) e audita os registros realizados.

## 4. Escopo do MVP
- Cadastro e listagem de colaboradores ativos.
- Registro de ponto com validação automática de sequência lógica.
- Cálculo automático de horas trabalhadas no dia e saldo (crédito/débito) em relação à jornada contratual.
- Interface Web integrada para operação imediata sem necessidade de ferramentas externas.
