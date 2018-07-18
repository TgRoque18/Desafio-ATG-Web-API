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

## A página Web
O desenvolvimento Web foi feito usando Angular CLI, para acessá-lo clique no link: https://github.com/TgRoque18/Desafio-ATG-Web-Application

## Configuração Inicial

No arquivo Web.config existem 6 appSettings, e elas devem ser preenchidas:
    <add key="HOSTNAME" value="" />
    <add key="USERNAME" value="" />
    <add key="PASSWORD" value="" />
    <add key="PORT" value="" />
    <add key="LOGFILE" value="" />
    <add key="QUEUE" value="" />

HOSTNAME: URL do serviço RabbitMQ;
USERNAME: Usuário do serviço RabbitMQ;
PASSWORD: Senha do usuário do serviço Rabbit MQ;
PORT: Porta do serviço Rabbit MQ;
LOGFILE: Caminho onde ficará armazenado o arquivo .txt para amazenamento de logs;
QUEUE: Nome da fila do Serviço RabbitMQ;
