# Especificação 04: Contratos da API REST

## 1. Colaboradores (`/api/employees`)
- `GET /api/employees` — Lista todos os colaboradores cadastrados (`200 OK`).
- `POST /api/employees` — Cadastra um novo colaborador (`201 Created` | `400 Bad Request`).

## 2. Registros de Ponto (`/api/timerecords`)
- `POST /api/timerecords` — Registra uma nova batida de ponto validando RN01, RN02 e RN03 (`201 Created` | `400 Bad Request` | `404 Not Found`).
- `GET /api/timerecords/daily/{employeeId}?date=YYYY-MM-DD` — Retorna as batidas do dia, total trabalhado formatado (`HH:mm`) e saldo do banco de horas (`200 OK` | `404 Not Found`).
