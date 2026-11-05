# PASSO A PASSO — EXECUÇÃO E TESTES

## 1. Preparar o ambiente
1. Instale o Visual Studio 2022 com a carga de trabalho **ASP.NET e desenvolvimento Web**.
2. Instale/tenha disponível o SQL Server Express ou LocalDB.
3. Abra o SQL Server Management Studio (SSMS).

## 2. Criar o banco
1. Conecte ao seu servidor no SSMS.
2. Clique em **Nova Consulta**.
3. Abra `saep_db.sql`, copie o conteúdo e cole na consulta.
4. Clique em **Executar**.
5. Confira se `saep_db` possui as tabelas `usuarios`, `pacientes`, `procedimentos` e `agendamentos`.
6. Confira se há pelo menos três registros em cada tabela.

Atenção: o script recria as tabelas do simulado se for executado novamente. Não o execute sobre um banco com dados que deseja preservar.

## 3. Abrir o sistema
1. Abra o Visual Studio.
2. Selecione **Abrir um projeto ou solução**.
3. Abra `sistema/ClinicaOdontologica.csproj`.
4. Aguarde a restauração dos pacotes NuGet.
5. Abra `sistema/appsettings.json` e confira a connection string. O padrão usa `(localdb)\\MSSQLLocalDB`.
6. Se sua instância SQL tiver outro nome, altere o trecho `Server=...`.
7. Pressione F5.

## 4. Fazer login
- Usuário: `admin`
- Senha: `Admin@123`

Teste também uma senha incorreta. O sistema deve exibir uma mensagem de erro. A autenticação usa hash PBKDF2 e o cookie expira em 20 minutos.

## 5. Testar pacientes
1. Entre em **Pacientes**.
2. Cadastre um paciente usando dados fictícios.
3. Pesquise pelo nome ou telefone.
4. Edite o registro e salve.
5. No SSMS, execute `SELECT nome, cpf_criptografado FROM pacientes;`. O CPF deve estar armazenado em formato criptografado.

## 6. Testar procedimentos
1. Entre em **Procedimentos**.
2. Cadastre nome, descrição e valor.
3. Confira a listagem.

## 7. Testar agendamentos
1. Entre em **Agendamentos**.
2. Cadastre um atendimento selecionando paciente e procedimento existentes.
3. Confira se a listagem está ordenada por data.
4. Edite a data de um agendamento e confira a alteração.

## 8. Preparar a entrega
A pasta contém `documentacao.pdf`, `der.png`, `saep_db.sql` e a pasta `sistema` com o código-fonte.
Preencha nome, data e número da prova no documento. Renomeie a pasta para seu nome completo e compacte em ZIP, RAR ou 7z.

## Limitações e cuidados
- Os dados de exemplo são fictícios.
- A chave de criptografia em `appsettings.json` é didática; não publique chaves em repositórios.
- A senha de demonstração deve ser trocada antes de qualquer uso real.
- Execute e teste o projeto no seu computador antes da entrega. Não foi possível validar a execução no seu ambiente local.
