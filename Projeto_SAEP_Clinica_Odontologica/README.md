# Projeto SAEP 2026 — Clínica Odontológica

Projeto acadêmico desenvolvido a partir do enunciado do Simulado SAEP 2026.

## Tecnologias
- C# / ASP.NET Core MVC (.NET 8)
- Entity Framework Core 8
- SQL Server
- Bootstrap 5

## Antes de executar
1. Instale o Visual Studio 2022 com a carga de trabalho **ASP.NET e desenvolvimento Web**.
2. Instale o SQL Server Express ou use o LocalDB instalado com o Visual Studio.
3. Abra `sistema/ClinicaOdontologica.csproj` no Visual Studio.
4. Confira a conexão em `sistema/appsettings.json`.
5. Execute o arquivo `saep_db.sql` no SQL Server Management Studio (SSMS).
6. No Visual Studio, restaure os pacotes NuGet e pressione F5.

## Acesso de demonstração
- Usuário: `admin`
- Senha: `Admin@123`

Troque a senha e a chave de criptografia antes de qualquer uso real. Os dados incluídos são fictícios.

## Funcionalidades
- Login com senha armazenada em hash PBKDF2 e cookie com expiração de 20 minutos.
- Página inicial com identificação do usuário e navegação.
- Cadastro, busca e edição de pacientes.
- CPF criptografado com AES antes de ser salvo.
- Cadastro e listagem de procedimentos.
- Cadastro, listagem ordenada e alteração da data dos agendamentos.

## Observações importantes
- A chave `Security:CpfKey` do `appsettings.json` é apenas para demonstração. Em um projeto real, use Secret Manager ou variável de ambiente, não publique a chave no repositório.
- O CPF é dado pessoal. Use somente dados fictícios no simulado.
- O projeto é uma base acadêmica e precisa ser executado e testado no computador da estudante antes da entrega.
- A associação paciente/procedimento ocorre no agendamento, seguindo a estrutura de relacionamentos indicada no enunciado.
