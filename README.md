# Microserviços com Mensageria RabbitMQ
## Visão Geral
Este projeto consiste em uma arquitetura de microserviços que se comunicam entre si por meio de mensageria assíncrona usando RabbitMQ. Cada microserviço é responsável por uma parte específica da lógica de negócios e se comunica com outros serviços por meio de mensagens.

## Tecnologias Utilizadas
- ASP.NET Core: Framework para desenvolvimento de microserviços em C#.
  
- RabbitMQ: Servidor de mensageria que suporta comunicação assíncrona entre os microserviços.
  
- Swagger: Ferramenta de documentação de APIs para facilitar o teste e a compreensão dos endpoints dos microserviços.

## Estrutura do Projeto
O projeto está estruturado da seguinte forma:

Microserviço UserService: Microserviço para usuário, onde possue apenas o metodo create e que envia uma mensagem para ser consumida pelo Microserviço de Email.
Microserviço EmailService: Somente consome a mensagem do RabbitMQ e envia um email de Welcome.

## Configuração e Execução
### Configurar o RabbitMQ:

- Instale e inicie o RabbitMQ no seu ambiente local ou utilize uma instância na nuvem.
- Configure as filas e os exchanges conforme necessário para cada microserviço.
