# Ponto Control

Esta é uma API .NET 6 desenvolvida para registrar o ponto eletrônico de colaboradores de uma organização. Ela utiliza o SQL Server como banco de dados e Entity Framework para mapeamento das classes. O objetivo principal desta API é permitir que os colaboradores registrem suas entradas e saídas de trabalho de forma eficiente e precisa.

## Recursos

- Cadastro de novos colaboradores.
- Registro de entrada e saída de funcionários com localização atual.
- Acompanhamento de horas trabalhadas.
- Relatório com filtragem por datas.

## Pré-requisitos

- .NET 6 SDK instalado
- SQL Server instalado e configurado

## Estrutura do Projeto

/
├─ src/
│   ├─ Application/            # Camada de aplicação
│   ├─ Domain/                 # Domínio do modelo de negócios
│   ├─ Infrastructure/         # Camada de infraestrutura (Entity Framework, serviços externos, etc.)
│   ├─ Web/                    # Camada de apresentação (API)
│
├─ tests/       

## Instalação

1. Clone o repositório:

   ```bash
   git clone <URL_DO_REPOSITORIO>
   ```

2. Abra a solução no Visual Studio ou execute dotnet restore na raiz do projeto para restaurar as dependências.

   ```bash
   dotnet restore
   ```

3. Configure a string de conexão com o SQL Server no arquivo appsettings.json na camada de infraestrutura.

4. Execute as migrações para criar o esquema do banco de dados usando o Entity Framework: 

   ```bash
   dotnet ef database update
   ```

5. Execute a aplicação: 

   ```bash
   dotnet run
   ```

## Repositório do Frontend
O frontend desta aplicação é mantido em um repositório separado. Você pode encontrar o código-fonte do frontend e obter mais informações sobre o desenvolvimento, implantação e uso do frontend no seguinte repositório:

[Link para o Repositório do Frontend](https://github.com/ramires-oliveira/PontoControlWeb)

## Uso

A API inclui um recurso de seed para criar um administrador inicial no banco de dados. O administrador é criado com as seguintes credenciais:

- E-mail: admin@admin.com
- Senha: senha123456
