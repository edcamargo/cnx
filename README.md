# CNX Backend Test
Crie um microsserviço para registrar um usuário e recomendar músicas com base na cidade natal.


### Clone Projetct
 ##git clone https://github.com/edcamargo/cnx.git

### Migrations
dotnet ef migrations add InitialCreate --startup-project ..\Conexia.Api && dotnet ef database update --startup-project ..\Conexia.Api

### Business Rules 
A API precisa registrar os seguintes campos do usuário: 
	i) nome; 
       ii) email; 
      iii) senha; 
       iv) notas pessoais (múltiplos);
	v) cidade natal.

![screenshoot](https://github.com/edcamargo/cnx/blob/master/Docs/Documentacao-Swagger.PNG "Screenshoot of the project")

### Notas pessoais e senha não devem estar visíveis no banco de dados.
![screenshoot](https://github.com/edcamargo/cnx/blob/master/Docs/CamposInvisiveisBanco.PNG "Screenshoot of the project")
![screenshoot](https://github.com/edcamargo/cnx/blob/master/Docs/CamposVisivelApricacao.PNG "Screenshoot of the project")

### A rota de autenticação deve funcionar com o método JWT.
![screenshoot](https://github.com/edcamargo/cnx/blob/master/Docs/Autenticacao-JWT.PNG "Screenshoot of the project")

### A API precisa fornecer um mecanismo de redefinição e esqueci a senha.
********************![screenshoot](https://github.com/edcamargo/cnx/Docs/Documentacao-Swagger.PNG "Screenshoot of the project")

### Registre todas as solicitações para futuras auditorias.
![screenshoot](https://github.com/edcamargo/cnx/blob/master/Docs/Log-Solicitacao.PNG "Screenshoot of the project")

### Com base na cidade natal e na temperatura atual, é necessário recomendar uma lista de reprodução da seguinte forma: 
  *    i) se a temperatura (celcius) estiver acima de 30 graus, suggests faixas para festa; 
  *    ii) caso a temperatura esteja entre 15 e 30 graus, sugere faixas de música pop; 
  *    iii) se estiver um pouco frio (entre 10 e 14 graus), sugere faixas de música rock. 
  *    iv) caso contrário, se estiver frio lá fora, sugere faixas de música clássica.
  - APIs do OpenWeatherMaps (https://openweathermap.org) para buscar dados de temperatura e o 
        Spotify (https://developer.spotify.com) para sugerir as faixas como parte da lista de reprodução.


O Readme deve conter todas as instruções para executar seu projeto.
	Você pode usar a API do OpenWeatherMaps (https://openweathermap.org) para buscar dados de temperatura e o 
        Spotify (https://developer.spotify.com) para sugerir as faixas como parte da lista de reprodução.

#Plus
* ORM
* Docker
* Queue
* Deploy on Cloud