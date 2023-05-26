# luizruiz

# CmCapital.Backoffice.Infrastructure

Microsserviço de Controle de compras de produtos e informações comercializadas.

### Migrations

Para criar uma nova Migration:

* Visual Studio: ``Add-Migration InitialCreate -verbose -project CmCapital.Backoffice.Infrastructure -s CmCapital.Backoffice.Api``

Para aplicar todas as migrations até a mais recente:

* Visual Studio: ``Update-Database -verbose -project CmCapital.Backoffice.Infrastructure -s CmCapital.Backoffice.Api``

### Docker
O projeto tbem está com a inicialização via Docker.

### Sql Server
Troque a Conexão para poder rodas as migrations em seu banco, caso queria executar o script manualmente ele se encontra no camanhinho CmCapital.Backoffice.Api/CmCapital.Backoffice.Infrastructure/Persistence/Scripts/CmCapital.Backoffice.Api.sql

### Arquitetura e informações

Foi utilizado C# dotnet core 7.0, a Api foi criado no padrão Minima para o ganho de performance ao inicializar o projeto. Junto foi utlizado o Pattern mediatoR e CQRS, a arquitetura utilizada "Clean Architheturte" foi bem estruturada respeitando o princípio SOLID e Clean Code



