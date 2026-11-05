-- Banco de dados do Simulado SAEP 2026 - Clínica Odontológica
-- Execute no SQL Server Management Studio (SSMS).
IF DB_ID('saep_db') IS NULL CREATE DATABASE saep_db;
GO
USE saep_db;
GO
DROP TABLE IF EXISTS agendamentos;
DROP TABLE IF EXISTS procedimentos;
DROP TABLE IF EXISTS pacientes;
DROP TABLE IF EXISTS usuarios;
GO
CREATE TABLE usuarios (
 id INT IDENTITY(1,1) PRIMARY KEY,
 nome NVARCHAR(80) NOT NULL,
 login NVARCHAR(50) NOT NULL UNIQUE,
 senha_hash NVARCHAR(300) NOT NULL
);
CREATE TABLE pacientes (
 id INT IDENTITY(1,1) PRIMARY KEY,
 nome NVARCHAR(120) NOT NULL,
 cpf_criptografado NVARCHAR(300) NOT NULL,
 telefone NVARCHAR(20) NOT NULL
);
CREATE TABLE procedimentos (
 id INT IDENTITY(1,1) PRIMARY KEY,
 nome NVARCHAR(100) NOT NULL,
 descricao NVARCHAR(300) NULL,
 valor DECIMAL(10,2) NOT NULL CHECK (valor >= 0)
);
CREATE TABLE agendamentos (
 id INT IDENTITY(1,1) PRIMARY KEY,
 paciente_id INT NOT NULL,
 procedimento_id INT NOT NULL,
 data_agendamento DATETIME2 NOT NULL,
 CONSTRAINT FK_agendamentos_pacientes FOREIGN KEY (paciente_id) REFERENCES pacientes(id),
 CONSTRAINT FK_agendamentos_procedimentos FOREIGN KEY (procedimento_id) REFERENCES procedimentos(id)
);
GO
INSERT INTO usuarios (nome, login, senha_hash) VALUES
(N'Administrador',N'admin',N'PBKDF2$BQzqdDNYZZwtGCTLw0xdmw==$TCaorV+BKD59P/8XpJtidmzjiVAxaiJ1TcM2a/l8idI='),
(N'Recepcionista Ana',N'ana',N'PBKDF2$GXOmVtg5XSVmefr9NXeHng==$RL0+TvTsgK3lMNFNkShVYoqvss38aa49i73JlE3BzGQ='),
(N'Recepcionista Bruno',N'bruno',N'PBKDF2$thmihzSbhlmvz9xD+tmT8A==$kmcP70XEn6drHgSmi77O3yflY0ieZXrovg8tmY5VwsA=');
INSERT INTO pacientes (nome, cpf_criptografado, telefone) VALUES
(N'Maria Silva',N'rblBB6YOT9vlpUGOliFnmeSx8EoTxY/cf8lGOBdZDFE=',N'(14) 90000-0001'),
(N'João Santos',N'RfXCJTj7BvwQIcV1K70XMmJ8Y7Hre0I/z6TPAf988EU=',N'(14) 90000-0002'),
(N'Carla Oliveira',N'vY8rc7x3uFEUxHEe+EeQDV7q5h+UK/Ztwroma0gNrPQ=',N'(14) 90000-0003');
INSERT INTO procedimentos (nome, descricao, valor) VALUES
(N'Limpeza dentária (profilaxia)',N'Remoção de placa e tártaro',180.00),
(N'Restauração dentária',N'Restauração de dente',250.00),
(N'Avaliação odontológica',N'Consulta de avaliação',100.00);
INSERT INTO agendamentos (paciente_id, procedimento_id, data_agendamento) VALUES
(1,1,'2026-11-10T09:00:00'),
(2,2,'2026-11-10T10:30:00'),
(3,3,'2026-11-11T14:00:00');
GO
-- Acesso de demonstração: admin / Admin@123
