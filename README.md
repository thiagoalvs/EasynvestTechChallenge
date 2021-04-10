# Easynvest Tech Demo

# Estrutura

O projeto está estruturado tendo como base o modelo abaixo:

[![N|Solid](https://alexcodetuts.files.wordpress.com/2020/02/untitled-diagram.png?w=640)](https://nodesource.com/products/nsolid)

## Presentation
A camada Presentation é responsável por realizar os procedimentos de inicialização da aplicação e exposição de endpoints.
Essa camada possui referencias as camadas Application, Domain e Infrastructure, porém só se comunica de fato com a camada Application, as demais referencias só existem para registrar os serviços que serão injetados via DI.

## Application
A camada Application é responsável por orquestrar o funcionamento da aplicação. Essa camada possui referencia a camada Domain. Todas as comunicações com as demais camadas são realizadas através de interfaces declaradas nessa camada.

## Domain
A camada Domain é responsável por conter os objetos que possuem regras de negócio. Essa camada não possui referência a nenhuma outra.

## Infrastructure
A camada Infrastrucuture possui os itens externos ao core da aplicação. Essa camada possui referencia a camada aplication e normalmente possui as implementações concretas das interfaces lá contidas.

## Shared
Essa camada é utilizada apenas para conter utilitários que podem ser usados em todas as demais camadas.

# Estratégias utilizadas
- RetryPolicy - Caso alguma das chamadas ao endpoints externos falhem, o HttpClient está configurado para tentar novamente mais duas vezes respeitando um intervalo de 3 segundos entre cada chamada.
- Cache - O resultado das chamadas ficam cacheados em memória local até o dias seguinta(00:00:00). Em um cenário real, o ideal seria possuir um servidor/cluster dedicado para manter essas informações.
- Errorhandling Middleware - Caso ocorra alguma exceção na aplicação, existe um middleware responsável por interceptar o erro, realizar alguma tratamento e entregar uma responsa amigável ao chamador. Essa estratégia permite que seja possível realizar um log detalhado do erro e devolver uma mensagem menos poluida ao chamador.
