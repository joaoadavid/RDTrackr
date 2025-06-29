# RDTrackr: Sistema de Gerenciamento de Estoque para Empresas de Usinagem

## Resumo

O **RDTrackr** é um sistema web de gerenciamento de estoque desenvolvido para empresas de usinagem que enfrentam dificuldades no controle de insumos e ferramentas. O projeto oferece atualização em tempo real, rastreabilidade de movimentações e alertas automáticos. A arquitetura é baseada em Django com suporte a tarefas assíncronas via Celery, mensageria com RabbitMQ e cache Redis, promovendo escalabilidade, desempenho e modularidade.

---

## 1. Introdução

### Contexto

Empresas do setor de usinagem operam com altos níveis de complexidade no controle de materiais. A falta de visibilidade em tempo real sobre movimentações e saldos de estoque compromete diretamente a eficiência produtiva. O uso de planilhas ou sistemas genéricos se mostra limitado diante da especificidade dessas operações.

### Justificativa

Para evitar rupturas na produção, atrasos em entregas e desperdícios, é fundamental contar com um sistema que ofereça não apenas o registro, mas também o acompanhamento contínuo das movimentações de estoque. O RDTrackr surge como uma solução sob medida para o setor, garantindo controle total, integração e automação.

### Objetivos

#### Objetivo Principal
Desenvolver um sistema web modular para gerenciamento de estoque com foco em atualização em tempo real, rastreabilidade e automação de alertas operacionais.

#### Objetivos Secundários
- Proporcionar uma interface web intuitiva e responsiva para operadores e gestores;
- Facilitar o acompanhamento em tempo real dos saldos e movimentações;
- Gerar alertas automáticos para reposição de itens críticos;
- Permitir emissão de relatórios por setor, período e movimentação;
- Incorporar visualizações interativas (dashboards) para análise de consumo e controle de estoque estratégico.

---

## 2. Descrição do Projeto

### Tema

Sistema web de gerenciamento de estoque voltado a empresas de usinagem, com foco em rastreabilidade, automação e escalabilidade.

### Problemas a Resolver

- Falta de controle de estoque em tempo real;
- Inexistência de alertas automáticos;
- Dificuldade de rastrear movimentações e responsáveis;
- Ausência de interface especializada para o setor de usinagem.

### Limitações

- Integrações com sistemas externos (ERP, financeiro) não fazem parte da versão atual;
- Módulo de controle de produção não está incluso no MVP.

---

## 3. Especificação Técnica

### Diagrama de Casos de Uso (UML)

[Fluxo de movimentação de estoque](docs/DiagramaCasosDeUso.png)

#### Modelos C4

[Modelagem C4](docs/ModelagemC4.png)

### 3.1 Requisitos

#### Requisitos Funcionais (RF)

- RF01: O sistema deve permitir o cadastro e a edição de itens no estoque.  
- RF02: O sistema deve permitir o registro de entradas e saídas com indicação de origem e destino.  
- RF03: O sistema deve permitir a consulta do saldo atualizado por item e por setor.  
- RF04: O sistema deve emitir alertas automáticos com base em regras pré-definidas.  
- RF05: O sistema deve conter o histórico completo de movimentações.  
- RF06: O sistema deve conter uma interface responsiva para diferentes dispositivos.  
- RF07: O sistema deve permitir a configuração de permissões por tipo de usuário.  
- RF08: O sistema deve permitir integração via API REST.  

#### Requisitos Não Funcionais (RNF)

- RNF01: O sistema deve garantir tempo de resposta inferior a 500ms nas operações principais.  
- RNF02: O sistema deve permitir atualização assíncrona com Celery e Redis.  
- RNF03: O sistema deve utilizar Redis para cache de dados críticos.  
- RNF04: O sistema deve garantir autenticação via JWT para segurança.  

### 3.2 Design da Solução

#### Visão Geral da Arquitetura

- **Backend:** Django REST Framework
- **Tarefas assíncronas:** Celery
- **Cache:** Redis
- **Banco de Dados:** PostgreSQL
- **Frontend:** React (SPA)
- **Orquestração:** Docker + Docker Compose

#### Padrões de Arquitetura

- MVC para estrutura do backend
- Modularização por domínio funcional (modular monolith)
- Estilo orientado a eventos (Event-Driven Architecture)
---

## 4. Stack Tecnológica

| Camada         | Tecnologias                                   |
|----------------|-----------------------------------------------|
| Linguagens     | Python, JavaScript                            |
| Backend        | Django, Django REST Framework                 |
| Frontend       | React, Axios, Tailwind CSS                    |
| Tarefas        | Celery                                        |
| Cache          | Redis                                         |
| Banco de Dados | PostgreSQL                                    |
| Monitoramento  | Prometheus, Grafana, Loguru                   |
| CI/CD          | GitHub Actions, Docker, Docker Compose        |

---

## 5. Segurança

- Comunicação via HTTPS;
- Autenticação com JWT;
- Controle de acesso baseado em papéis (RBAC);
- Logs estruturados e auditáveis;
- Sanitização e validação de entrada de dados.

---

## 6. Próximos Passos

- Implementar funcionalidades principais do MVP;
- Realizar testes automatizados e de integração;
- Implantar ambiente de homologação;
- Obter feedback de usuários reais.

---

## 7. Referências

- [Django](https://docs.djangoproject.com/)
- [Django REST Framework](https://www.django-rest-framework.org/)
- [Celery](https://docs.celeryq.dev/)
- [RabbitMQ](https://www.rabbitmq.com/)
- [Redis](https://redis.io/)
- [React](https://reactjs.org/)
- [C4 Model](https://c4model.com/)

---

## 8. Autor

**João Antonio David**  
Curso: Engenharia de Software – Católica de Santa Catarina  
Orientador: Prof. Diogo Vinícius Winck

---

## 9. Status

📌 Projeto em desenvolvimento. MVP previsto para próxima etapa do Portfólio.
