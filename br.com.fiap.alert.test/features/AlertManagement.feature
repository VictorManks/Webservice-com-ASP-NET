# language: pt

Funcionalidade: Gerenciamento de Alertas Ambientais
  Como um usuário do sistema
  Eu quero poder gerenciar alertas ambientais
  Para manter a população informada sobre situações de risco

  Contexto:
    Dado que eu esteja autenticado no sistema
    E possua um token JWT válido

  Cenário: Criar um novo alerta ambiental com sucesso
    Quando eu enviar uma requisição POST para criar um alerta com os dados:
      | typeAlert | message                     | coords          | author      |
      | ENCHENTE  | Risco de enchente na região | cordenada criada| Maria Silva |
    Então o sistema deve retornar o status code 201
    E retornar o ID do novo alerta criado

  Cenário: Listar alertas com paginação
    Dado que existam alertas cadastrados no sistema
    Quando eu solicitar a listagem de alertas com:
      | referencia | tamanho |
      | 0          | 10      |
    Então o sistema deve retornar o status code 200
    E retornar uma lista com no máximo 10 alertas

  Cenário: Tentar acessar alertas sem autenticação
    Dado que eu não esteja autenticado
    Quando eu tentar listar os alertas
    Então o sistema deve retornar o status code 401