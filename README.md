# RDTrackr – Sistema de Gerenciamento de Estoque para Empresa de Usinagem

## 📘 Descrição

O **RDTrackr** é um sistema web de gerenciamento de estoque voltado para empresas do setor de usinagem. A proposta é automatizar o controle de entrada e saída de materiais, ferramentas e produtos acabados, com foco em rastreabilidade e eficiência operacional.

A aplicação segue uma arquitetura baseada em **microsserviços**, utilizando tecnologias modernas como `.NET`, `Docker`, `RabbitMQ`, `Redis`, `SQL Server` e interface em **Blazor** ou **React**.

---

## 🚀 Objetivos

- Gerenciar o estoque de forma automatizada e centralizada;
- Rastrear movimentações de materiais e ferramentas;
- Emitir relatórios e alertas em tempo real;
- Prover escalabilidade e desempenho com arquitetura moderna.

---

## ⚙️ Funcionalidades

- Cadastro de materiais, ferramentas e produtos acabados;
- Registro de entradas e saídas no estoque;
- Visualização de saldo e histórico de movimentações;
- Controle de usuários com níveis de acesso (admin/operador);
- Geração de relatórios por período, setor, tipo de item e responsável;
- Notificações de estoque mínimo e vencimento;
- Integração com setores e ordens de produção.

---

## 🔐 Requisitos Não Funcionais

- Alta disponibilidade (mínimo de 99,5%);
- Tempo de resposta crítico < 500ms;
- Comunicação segura via HTTPS;
- Autenticação com JWT;
- Microsserviços independentes;
- Logging estruturado e monitoramento centralizado.

---

## 🛠️ Tecnologias Utilizadas

| Camada         | Tecnologia              |
|----------------|--------------------------|
| Backend        | .NET Core                |
| Mensageria     | RabbitMQ                 |
| Cache          | Redis                    |
| Banco de Dados | SQL Server               |
| Containers     | Docker                   |
| Frontend       | Blazor ou React          |
| Monitoramento  | Grafana                  |
| CI/CD          | Azure DevOps             |

---

## 🧪 Plano de Testes

- **Testes Unitários**: lógica de microsserviços (xUnit);
- **Testes de Integração**: comunicação entre serviços (Postman);
- **Testes de Interface**: interação do usuário;
- **Testes de Performance**: latência e carga (JMeter, SonarCloud).

---

## 🗂️ Metodologia

A metodologia adotada será o **Scrum**, com sprints quinzenais:

1. Levantamento de requisitos;
2. Modelagem de arquitetura e banco;
3. Implementação incremental;
4. Testes e integração contínua;
5. Feedback e iterações.

---

## 📊 Modelagem UML

O projeto inclui:
- Diagrama de Classes (Modelo de Domínio);
- Fluxo de movimentação de estoque;
- Documentação técnica detalhada em LaTeX.

---

## 📅 Cronograma (a definir)

| Atividade                               | Início    | Término   |
|----------------------------------------|-----------|-----------|
| Levantamento de requisitos             | A definir | A definir |
| Modelagem da arquitetura e banco       | A definir | A definir |
| Implementação dos microsserviços       | A definir | A definir |
| Integração com frontend                | A definir | A definir |
| Testes e documentação final            | A definir | A definir |

---

## 📚 Referências

- [.NET Docs](https://learn.microsoft.com/dotnet/)
- [Docker Docs](https://docs.docker.com/)
- [Redis Docs](https://redis.io/docs/)
- [RabbitMQ Docs](https://www.rabbitmq.com/documentation.html)

---

## ✅ Status

📌 Projeto em fase de planejamento e documentação técnica. Implementação prevista para próxima etapa.

---

## 👤 Autor

**João Antonio David**  
Engenharia de Software – Católica de Santa Catarina  
Orientador: DIOGO VINÍCIUS WINCK

