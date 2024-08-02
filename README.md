Baixar o projeto.
Baixar e instalar o Erlang e o RabbitMQ
inicializar API FinanceSystemBrunoTorres
inicializar ServiceLançamentos
Enviar Requisição via POSTMAN no endpoint
http://localhost:5108/Financial/postEntry
com o JSON:
{
    "id":"1",
    "type": "Credit",
    "value": "200.00",
    "date": "2024-01-01"
}
Observar mensagens no console do ServiceLançamentos
e o Callback no console FinanceSystemBrunoTorres
