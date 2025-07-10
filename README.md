# RDTrackr: Sistema de Gerenciamento de Estoque para Empresas de Usinagem

## Resumo
O **RDTrackr** é um sistema web de gerenciamento de estoque desenvolvido para empresas de usinagem que enfrentam desafios no controle de insumos e ferramentas. O projeto oferece atualização em tempo real, rastreabilidade completa das movimentações e alertas automáticos para itens críticos. Sua arquitetura é baseada em Django, Celery e Redis, promovendo escalabilidade, desempenho e modularidade, com um frontend moderno construído em React.

---

## 1. Introdução

### Contexto
A gestão de estoque exerce um papel estratégico nas organizações, sendo fator determinante para a eficiência produtiva e a saúde financeira. Conforme evidenciado por Rezende (2008), falhas na administração de materiais e na rastreabilidade das entradas e saídas podem resultar em excessos ou rupturas de estoque, impactando diretamente o fluxo operacional e o nível de serviço ao cliente. No setor de usinagem, onde o controle de insumos e ferramentas é crítico, um sistema especializado como o RDTrackr se justifica por permitir acompanhamento em tempo real, redução de desperdícios e aumento da confiabilidade dos processos.

### Justificativa
Para evitar paradas na produção, atrasos em entregas e desperdícios, torna-se essencial contar com um sistema que vá além do simples registro: é necessário monitorar continuamente o estoque, emitindo alertas preventivos. Rezende (2008) destaca que falhas na gestão de estoque resultam em custos adicionais e comprometem o fluxo operacional, reforçando a importância de soluções especializadas. O RDTrackr foi concebido como uma solução sob medida para empresas de usinagem, garantindo controle total, integração e automação.

### Objetivos
#### Objetivo Principal
Desenvolver um sistema web modular para gerenciamento de estoque, focado em atualização em tempo real, rastreabilidade e automação de alertas operacionais.

#### Objetivos Secundários
- Proporcionar uma interface web intuitiva e responsiva;
- Facilitar o acompanhamento em tempo real de saldos e movimentações;
- Gerar alertas automáticos para reposição de itens críticos;
- Permitir emissão de relatórios por setor, período e movimentação;
- Incorporar dashboards interativos para análise estratégica do estoque.

---

## 2. Descrição do Projeto

### Tema
Sistema web de gerenciamento de estoque voltado para empresas de usinagem, com ênfase em rastreabilidade, automação e escalabilidade.

### Problemas a Resolver
- Falta de controle de estoque em tempo real;
- Ausência de alertas automáticos para itens críticos;
- Dificuldade em rastrear movimentações e responsáveis;
- Carência de uma interface especializada para o setor de usinagem.

### Limitações
- Integrações com sistemas externos (ERP, financeiro) não fazem parte do escopo atual;
- O módulo de controle de produção não está incluído no MVP.

---

## 3. Especificação Técnica

### Requisitos Funcionais (RF)
- RF01: Permitir cadastro de itens no estoque.
- RF02: Permitir edição de itens no estoque.
- RF03: Registrar entradas com origem e quantidade.
- RF04: Registrar saídas com destino e responsável.
- RF05: Consultar saldo atualizado por item/setor.
- RF06: Emitir alertas automáticos conforme regras de estoque.
- RF07: Manter histórico completo de movimentações.

### Requisitos Não Funcionais (RNF)
- RNF01: Garantir tempo de resposta inferior a 500ms para operações críticas. 
- RNF02: Utilizar Celery com Redis como broker e backend para processamento assíncrono.
- RNF03: Garantir autenticação segura via JWT. 
- RNF04: Garantir interface responsiva em diferentes dispositivos. 
- RNF05: Permitir configuração de permissões por tipo de usuário. 
- RNF06: Disponibilizar API REST para futuras integrações com sistemas externos. 
---

## 4. Stack Tecnológica e Considerações de Design

### Considerações de Design
- **Monólito Modularizado:** backend construído em Django REST Framework, separado em módulos de domínio como estoque, movimentações e alertas.
- **MVC:** Django organiza Models, Views e Serializers, mantendo coesão e manutenibilidade.
- **Event-Driven:** o Celery, com Redis como broker e backend, viabiliza processamento assíncrono para relatórios e alertas.

### Tecnologias Utilizadas
| Camada         | Tecnologias                      |
|----------------|---------------------------------|
| Linguagens     | Python, JavaScript              |
| Backend        | Django, Django REST Framework   |
| Frontend       | React, Tailwind CSS             |
| Tarefas        | Celery                          |
| Cache/Filas    | Redis                           |
| Banco de Dados | PostgreSQL                      |
| Monitoramento  | Prometheus, Grafana, Loguru     |
| CI/CD          | GitHub Actions, Podman          |

---

## 5. Diagramas de Caso de Uso (UML)

### Caso de Uso 1: Processo de Compra
![Caso de Uso 1](docs/CasoDeUso-ProcessoCompra.png)

### Caso de Uso 2: Movimentação e Cadastro de Produtos
![Caso de Uso 2](docs/CasoDeUso-MovimentacaoCadastro.jpg)

### Caso de Uso 3: Gestão de Estoque e Alertas
![Caso de Uso 3](docs/CasoDeUso-GestaoEstoque.png)

---

## 6. Modelagem C4

O modelo C4 foi adotado para representar a arquitetura em níveis. O diagrama abaixo mostra a visão de containers, já considerando orquestração com Podman para padronizar ambientes.

![Modelagem C4](docs/ModelagemC4.png)

---

## 7. Considerações de Segurança

- **HTTPS em toda comunicação:** protegendo dados sensíveis e credenciais.
- **JWT + RBAC:** controle de acesso baseado em papéis e autenticação stateless.
- **Logs estruturados:** facilitando auditorias, rastreamento e monitoramento.
- **Validações robustas:** prevenindo SQL Injection e XSS.

---

## 8. Próximos Passos

- Validação do escopo e modelo com stakeholders e orientadores.
- Refinamento dos requisitos e dos diagramas UML e C4.
- Montagem do pipeline de CI/CD com Podman e GitHub Actions.
- Desenvolvimento do MVP com sprints quinzenais.
- Implantação em ambiente controlado para homologação.
- Coleta de feedback e ajustes contínuos.

---

## 9. Referências

### Frameworks e Bibliotecas
- [Django](https://www.djangoproject.com/)
- [Django REST Framework](https://www.django-rest-framework.org/)
- [React.js](https://reactjs.org/)
- [Tailwind CSS](https://tailwindcss.com/)
- [Celery](https://docs.celeryq.dev/)
- [Redis](https://redis.io/)
- [JWT](https://jwt.io/)

### Ferramentas de Desenvolvimento e Gestão
- [GitHub Actions](https://github.com/features/actions)
- [Podman](https://podman.io/)
- [Prometheus](https://prometheus.io/)
- [Grafana](https://grafana.com/)
- [VS Code](https://code.visualstudio.com/)
- [Postman](https://www.postman.com/)

### Documentação e Artigos
- [Django Docs](https://docs.djangoproject.com/en/stable/)
- [DRF Quickstart](https://www.django-rest-framework.org/tutorial/quickstart/)
- [React Learn](https://react.dev/learn)
- [Tailwind Docs](https://tailwindcss.com/docs)
- [Celery Docs](https://docs.celeryq.dev/en/stable/)

### Trabalhos Acadêmicos
- REZENDE, Juliana Pinheiro. *Gestão de Estoque: um estudo de caso em uma empresa de materiais para construção*. Monografia (Administração de Empresas) — UniCEUB, Brasília, 2008.

---

## 10. Autor

**João Antonio David**  
Curso: Engenharia de Software – Católica de Santa Catarina  
Orientador: Prof. Diogo Vinícius Winck
