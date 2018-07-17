# Desafio ATG

## O desafio.

O desafio resume-se a entregar duas ferramentas:
- Página Web;
- API C#;

A página envia dados para a API e a API envia uma requisição para um serviço RabbitMQ.


## O desenvolvimento API C#.

A api foi desenvolvida usando padrão .Net, com envio de mensagem em Json para o serviço RabbitMQ.
O exemplo usado para realizar a conexão foi: https://www.rabbitmq.com/tutorials/tutorial-one-dotnet.html

Nessa API ainda foi criado um projeto de testes simples, somente a fim de exemplificar o uso do mesmo numa API.
