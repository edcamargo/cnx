# CNX Backend Test
Solução proposta foi a criação de uma APi restful com arquitetura CQRs.
  - Registrar um usuário e recomendar músicas com base na cidade natal.

## Procedimentos

  * Realizar um clone do projeto através do command `git clone https://github.com/edcamargo/cnx.git`

  * Entrar na pasta do projeto e excutar o comando abaixo.

### `dotnet run` 
  - info: Microsoft.Hosting.Lifetime[0]
  -       Now listening on: http://localhost:5000
  - info: Microsoft.Hosting.Lifetime[0]
  -       Application started. Press Ctrl+C to shut down.
  - info: Microsoft.Hosting.Lifetime[0]
  -       Hosting environment: Development

### Business Rules 

A API precisa registrar os seguintes campos do usuário: 
  - i) nome; 
  - ii) email; 
  - iii) senha; 
  - iv) notas pessoais (múltiplos);
  - v) cidade natal.

## Resultado do projeto

![screenshoot](https://github.com/edcamargo/cnx/blob/master/Docs/Documentacao-Swagger.PNG "Screenshoot of the project")

### Notas pessoais e senha não devem estar visíveis no banco de dados.

  - Notas pessoais e senha Criptografados no banco
![screenshoot](https://github.com/edcamargo/cnx/blob/master/Docs/CamposInvisiveisBanco.PNG "Screenshoot of the project")

  - Notas pessoais Descriptografados no retorno da APi
![screenshoot](https://github.com/edcamargo/cnx/blob/master/Docs/CamposVisivelApricacao.PNG "Screenshoot of the project")

### A rota de autenticação deve funcionar com o método JWT.
![screenshoot](https://github.com/edcamargo/cnx/blob/master/Docs/Autenticacao-JWT.PNG "Screenshoot of the project")

### A API precisa fornecer um mecanismo de redefinição e esqueci a senha.
  *    Redefinição de Senha: Mecanismo implementado no método ResetPassword da api. Precisará de um e-mail, senha antiga e nova senha.
  *    Esqueci a senha: Mecanismo implementado no método ForgotPassword da api. Precisará de um e-mail valido, a senha deverá ser enviada por e-mail. 
	`Para efeito de test no processo, a senha está sendo retornada na api como retorno FAKE.`

### Registre todas as solicitações para futuras auditorias.
![screenshoot](https://github.com/edcamargo/cnx/blob/master/Docs/Log-Solicitacao.PNG "Screenshoot of the project")

## As regras abaixo poderão ser conferida no retorno da Api.

### Com base na cidade natal e na temperatura atual, é necessário recomendar uma lista de reprodução da seguinte forma: 
  *    i) se a temperatura (celcius) estiver acima de 30 graus, suggests faixas para festa; 
  *    ii) caso a temperatura esteja entre 15 e 30 graus, sugere faixas de música pop; 
  *    iii) se estiver um pouco frio (entre 10 e 14 graus), sugere faixas de música rock. 
  *    iv) caso contrário, se estiver frio lá fora, sugere faixas de música clássica.
  - APIs do OpenWeatherMaps (https://openweathermap.org) para buscar dados de temperatura e o 
        Spotify (https://developer.spotify.com) para sugerir as faixas como parte da lista de reprodução.

#Plus
  *    [x] ORM
  *    [x] Deploy on Cloud
  *    [x] Swagger
  *    [x] Migrations
  *    [x] CQRs
  *    [x] Clean Clode
  *    [x] DDD
  *    [x] Test Unitário
  *    [x] Docker
  *    [ ] Queue
