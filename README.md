# Atividade02

Aluno: Fernando Areias

E-mail: nando.calheirosx@gmail.com

## Rodando o projeto

`docker-compose up --build`

Após iniciar o docker-compose crie o banco de dados ProposalsDB e a collection Store no mongo e insira esse documento

```
{
"_id": {
"$oid": "65f9b994d947ac0184a9be12"
},
"cnpj": "18524535000127",
"name": "Store Example"
}

```

Depois disso acesse `http://localhost:3000/authentication/login`

## Aplicações

| Application  | Host                   |
| :----------- | :--------------------- |
| MS Proposta  | http://localhost:5155  |
| MS Portador  | http://localhost:5030  |
| Mongodb      | http://localhost:27017 |
| RabbitMQ     | http://localhost:5672  |
| ADM RabbitMQ | http://localhost:15672 |

## Documentação

### Diagramas

`documentacao/Atividade04.xml`

### Postman collection

`documentacao/Atividade02.postman_collection.json`

![Atividade02 drawio-5-Arquitetura drawio](https://github.com/fernandoareias/atividade04/assets/87771786/22a0cce1-05c4-4bd8-938c-34491aded47f)
