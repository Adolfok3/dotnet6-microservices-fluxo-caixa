# Fluxo Caixa

Aplicação de fluxo de caixa construida na arquitetura de microserviço em conjunto com as boas praticas de desenvolvimento, código limpo e os principios do SOLID.

## Desenho da arquitetura
<img src="https://i.imgur.com/qz2pZ4o.png" width="500" height="350" />

## Padrões de microserviço utilizados
- Chassis
- Database per Service
- Shared database
- Health check API
- Messaging
- Externalized configuration
- API Gateway

## Padrões de projetos utilizados
- Builder
- Singleton
- Adapter
- Decorator
- Strategy
- Factory

## Tecnologias e bibliotecas usadas
- .NET 6
- EntityFramework Core
- Postgres
- Prometheus
- Kafka
- Redis
- MongoDb
- Serilog
- Vault
- AutoMapper
- Swagger
- FluentValidation

## Execução do projeto
### Obg: É necessario ter o docker instalado
- Navegar até o diretorio \src\Infra\vault e extrair o conteudo da pasta vault.zip no mesmo diretorio
- Executar o arquivo up.bat dentro da pasta \src\Infra
- Para que o serviços consultem as configurações de cada um, como connectionString, é necessário descelar o vault, para isso é necessário acessar a url http://localhost:8200/ e descelar o vault com a key: 964f9754f8a5d81a9aa339fadda45290c2f83a184e61431244ddc351b54e085b
- Executar o serviços pelo arquivo de docker-compose em \src\FluxoCaixa
- Utilizar a collection do postman em docs para testar api

## Testes
- Foram utilizados testes unitarios e de integração. A imagem abaixo mostra a cobertura de codigo dos serviços
<img src="https://i.imgur.com/L44dvGC.png" width="500" height="120"/>

## Observalidade
- Foi adicionado a opção de observalidade, health check e metricas com o prometheus.
- Para visualizar acesse http://localhost:9090/targes
- Cada serviço possui um endpoint para health check, ex: http://localhost:5000/health
