GERENCIADOR DE RECEITAS - ASP.NET CORE WEB API
==============================================

Este projeto é um sistema de gerenciamento de receitas que:
- Consome a API pública TheMealDB para buscar receitas prontas.
- Permite criar, editar, listar e excluir receitas personalizadas.
- Permite favoritar receitas vindas da API externa.
- Salva tudo em arquivos locais (JSON), sem necessidade de banco de dados.

------------------------------------------------------------
#1. TECNOLOGIAS UTILIZADAS
------------------------------------------------------------
- .NET 8.0
- ASP.NET Core Web API
- Swagger (documentação interativa)
- HttpClient (para consumir a API externa)
- System.Text.Json (para salvar dados em arquivos locais)
- Código em C#
------------------------------------------------------------
#2. COMO INSTALAR E EXECUTAR
------------------------------------------------------------
1. Instale o Visual Studio(Roxo)
2. Instale o .NET 8.0 no seu computador.
3. Baixe ou clone este projeto:
   git clone https://github.com/EnzoBrumate/GerenciadorReceitas.git
4. Restaure as dependências:
   dotnet restore
5. Execute o projeto:
   dotnet run
6. O navegador abrirá automaticamente na página do Swagger, na porta configurada para o seu ambiente.

------------------------------------------------------------
#3. ENDPOINTS DISPONÍVEIS
------------------------------------------------------------

LOCALCONTROLLER (CRUD de receitas personalizadas)
-------------------------------------------------
GET    /local/receitas          -> Lista todas as receitas criadas por você
POST   /local/receitas          -> Cria uma nova receita personalizada
PUT    /local/receitas/{id}     -> Atualiza uma receita existente pelo ID
DELETE /local/receitas/{id}     -> Exclui uma receita pelo ID

APICONTROLLER (Integração com TheMealDB e Favoritos)
----------------------------------------------------
GET    /api/buscar-nome?nome=...        -> Busca receitas pelo nome
GET    /api/buscar-ingrediente?ingrediente=... -> Busca receitas por ingrediente
GET    /api/buscar-id?id=...            -> Busca detalhes de uma receita pelo ID
POST   /api/favoritos                   -> Adiciona uma receita da API aos favoritos
GET    /api/favoritos                   -> Lista todos os favoritos salvos
DELETE /api/favoritos/{id}              -> Remove um favorito pelo ID

------------------------------------------------------------
#4. EXEMPLOS DE USO
------------------------------------------------------------

CRIAR RECEITA PERSONALIZADA (POST /local/receitas)
---------------------------------------------------
Corpo da requisição (JSON):
{
  "title": "Torta de Limão",
  "ingredients": ["limão", "farinha", "leite condensado"],
  "instructions": "Misture tudo e asse por 30 minutos.",
  "image": "https://exemplo.com/torta.jpg"
}

Resposta esperada:
{
  "id": "c1a2b3c4-d5e6-7890-1234-56789abcdef0",
  "title": "Torta de Limão",
  "ingredients": ["limão", "farinha", "leite condensado"],
  "instructions": "Misture tudo e asse por 30 minutos.",
  "image": "https://exemplo.com/torta.jpg"
}

ADICIONAR FAVORITO (POST /api/favoritos)
----------------------------------------
Corpo da requisição (JSON):
{
  "idMeal": "52772",
  "title": "Teriyaki Chicken"
}

Resposta esperada:
{
  "message": "Favorito adicionado com sucesso"
}

------------------------------------------------------------
#5. COMO TESTAR NO SWAGGER
------------------------------------------------------------
Quando você roda o projeto com:

dotnet run ou simplesmente compilando

O navegador já abre automaticamente na página do Swagger.

   OBSERVAÇÃO IMPORTANTE:
- O número da porta (exemplo: 5000, 5173, 7173) pode variar de máquina para máquina ou conforme a configuração do arquivo "launchSettings.json".
- No seu ambiente pode abrir em http://localhost:5000/swagger.
- No computador de outra pessoa pode abrir em http://localhost:5173/swagger ou outro número.
- O importante é que o navegador abre direto na porta configurada, sem necessidade de digitar manualmente.

No Swagger você pode:
1. Clicar em "Try it out" em qualquer endpoint.
2. Preencher os campos necessários (exemplo: nome da receita).
3. Clicar em "Execute".
4. Ver a resposta logo abaixo (status code + JSON retornado).

------------------------------------------------------------
#6. PERSISTÊNCIA
------------------------------------------------------------
- As receitas personalizadas ficam salvas em Data/receitas.json
- Os favoritos da API ficam salvos em Data/favoritos.json
- Esses arquivos são atualizados automaticamente quando você cria, edita ou exclui.

------------------------------------------------------------
#7. OBSERVAÇÕES IMPORTANTES
------------------------------------------------------------
- A API externa TheMealDB só aceita parâmetros em inglês.
  Exemplo: "chicken" em vez de "frango".
- O Swagger mostra os endpoints em português, mas os dados de busca
  precisam ser enviados em inglês para funcionar.
- O projeto não usa banco de dados, apenas arquivos JSON locais.
- É ideal para aprendizado e prática de ASP.NET Core Web API.

------------------------------------------------------------
#8. CONCLUSÃO
------------------------------------------------------------
- Você pode buscar receitas na API externa.
- Criar suas próprias receitas personalizadas.
- Favoritar receitas da API.
- Testar tudo pelo Swagger de forma simples.
- Persistir dados localmente sem complicação.
