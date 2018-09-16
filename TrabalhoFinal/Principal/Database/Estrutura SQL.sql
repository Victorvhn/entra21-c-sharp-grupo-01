DROP TABLE historico_de_viagens;
DROP TABLE viagens_turistas;
DROP TABLE viagens;
DROP TABLE pacotes_pontos_turisticos;
DROP TABLE pontos_turisticos;
DROP TABLE turistas_pacotes;
DROP TABLE pacotes;
DROP TABLE guias;
DROP TABLE turistas;
DROP TABLE enderecos;
DROP TABLE cidades;
DROP TABLE estados;
DROP TABLE idiomas;
DROP TABLE logins;

CREATE TABLE logins (
	id INT IDENTITY(1,1) PRIMARY KEY,
	privilegio VARCHAR(15),
	email VARCHAR(150),
    senha VARCHAR(100),
	ativo BIT DEFAULT '1'
);

CREATE TABLE idiomas (
    id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    nome VARCHAR(100),
	ativo BIT DEFAULT '1',
);

CREATE TABLE estados (
    id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    nome VARCHAR(100),
	ativo BIT DEFAULT '1'
);

CREATE TABLE cidades (
    id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    id_estado INT,
    nome VARCHAR(100),
	ativo BIT DEFAULT '1',
    FOREIGN KEY (id_estado) REFERENCES estados(id)
);

CREATE TABLE enderecos (
    id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    id_cidade INT,
    cep VARCHAR(8),
    logradouro VARCHAR(150),
    numero SMALLINT,
    complemento VARCHAR(20),
    referencia VARCHAR(400),
	ativo BIT DEFAULT '1',
    FOREIGN KEY (id_cidade) REFERENCES cidades(id)
);

CREATE TABLE turistas ( 
    id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    id_login INT,
    id_endereco INT,
    ativo BIT DEFAULT '1',
    nome VARCHAR(100),
    sobrenome VARCHAR(100),
    cpf VARCHAR(11),
    rg VARCHAR(20),
    sexo CHAR(10),
    data_nascimento DATE,  
    FOREIGN KEY (id_endereco) REFERENCES enderecos(id),
    FOREIGN KEY (id_login) REFERENCES logins(id)
);

CREATE TABLE guias (
    id INT IDENTITY(1,1) PRIMARY KEY NOT NULL, 
    id_login INT,
    ativo BIT DEFAULT '1',
    id_endereco INT,
    nome VARCHAR(100),
    sobrenome VARCHAR(100),
    numero_carteira_trabalho VARCHAR(11),
    categoria_habilitacao VARCHAR(10), 
    salario FLOAT,
    cpf VARCHAR(11),
    rg VARCHAR(20),
    sexo CHAR(1),
    data_nascimento DATE,  
    rank_ SMALLINT, 
    FOREIGN KEY (id_endereco) REFERENCES enderecos(id),
    FOREIGN KEY (id_login) REFERENCES logins(id)
);

CREATE TABLE pacotes (
    id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    nome VARCHAR(100),
    valor FLOAT,
	ativo BIT DEFAULT '1',
    percentual_max_desconto TINYINT,
);

CREATE TABLE turistas_pacotes(
	id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	id_turista INT,
	id_pacote INT,
	status_do_pedido VARCHAR(20),
	data_requisicao DATE
); --NXN

CREATE TABLE pontos_turisticos (
    id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,  
    id_endereco INT,
    nome VARCHAR(100) NOT NULL,
	valor FLOAT,
	ativo BIT DEFAULT '1',
    FOREIGN KEY (id_endereco) REFERENCES enderecos(id)
);

CREATE TABLE pacotes_pontos_turisticos (
    id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    id_ponto_turistico INT,
    id_pacote INT,
	ativo BIT DEFAULT '1',
    FOREIGN KEY (id_ponto_turistico) REFERENCES pontos_turisticos(id),
    FOREIGN KEY (id_pacote) REFERENCES pacotes(id),
); --NXN

CREATE TABLE viagens (
    id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    data_compra DATE,
    id_pacote INT,
    id_guia INT,
    data_horario_saida DATETIME,
    data_horario_volta DATETIME,
	valor FLOAT,
	ativo BIT DEFAULT '1',
    FOREIGN KEY (id_pacote) REFERENCES pacotes(id),
    FOREIGN KEY (id_guia) REFERENCES guias(id)
);

CREATE TABLE viagens_turistas (
    id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    id_turista INT,
    id_viagem INT,
    valor FLOAT,
	ativo BIT DEFAULT '1',
    FOREIGN KEY (id_turista) REFERENCES turistas(id),
    FOREIGN KEY (id_viagem) REFERENCES viagens(id)  
);  --NXN

CREATE TABLE historico_de_viagens (
    id INT IDENTITY(1,1) PRIMARY KEY ,
    id_pacote INT ,
    data_ DATETIME,
	ativo BIT DEFAULT '1',
    FOREIGN KEY (id_pacote) REFERENCES pacotes(id)
);

INSERT INTO logins(email, senha, privilegio) VALUES
('admin@admin.com', 'admin', 'Admin'),
('user@user.com', 'user', 'User'),
('user@user.com1', 'user1', 'User'),
('user@user.com2', 'user2', 'User'),
('user@user.com3', 'user3', 'User'),
('funcionario@funcionario.com', 'funcionario', 'Funct'),
('funcionario@funcionario.com2', 'funcionario2', 'Funct'),
('funcionario@funcionario.com3', 'funcionario3', 'Funct');

INSERT INTO pacotes (nome, valor, percentual_max_desconto) VALUES
('Disney', 4000, 20),
('Amsterdam', 5000, 10),
('Orlando', 3500, 5),
('Paris', 4500, 15);

INSERT INTO historico_de_viagens (id_pacote, data_) VALUES
((SELECT id FROM pacotes WHERE nome = 'Disney'),'07-10-2014'),
((SELECT id FROM pacotes WHERE nome = 'Amsterdam'),'09-11-2017'),
((SELECT id FROM pacotes WHERE nome = 'Orlando'),'12-05-2016'),
((SELECT id FROM pacotes WHERE nome = 'Paris'),'12-10-2014');

INSERT INTO estados (nome) VALUES
('Berlim'),
('Fukuoka'),
('Santa Catarina'),
('siwa'),
('Somuona'),
('Chorinchi'),
('Azemara'),
('Tunama'),
('Madri');

INSERT INTO cidades (id_estado, nome) VALUES
((SELECT id FROM estados WHERE nome = 'Berlim'), 'Hamburgo'),
((SELECT id FROM estados WHERE nome = 'Fukuoka'), 'Fukuoka'),
((SELECT id FROM estados WHERE nome = 'Santa Catarina'), 'Blumenau'),
((SELECT id FROM estados WHERE nome = 'siwa'), 'Oásis de siwa'),
((SELECT id FROM estados WHERE nome = 'Somuona'), 'Marua'),
((SELECT id FROM estados WHERE nome = 'Chorinchi'), 'Kura'),
((SELECT id FROM estados WHERE nome = 'Azemara'), 'Balaci'),
((SELECT id FROM estados WHERE nome = 'Tunama'), 'Muria'),
((SELECT id FROM estados WHERE nome = 'Madri'), 'Catalo');

INSERT INTO enderecos (id_cidade, cep, logradouro, numero, complemento, referencia) VALUES
((SELECT id FROM cidades WHERE nome = 'Hamburgo'), 14785236, 'rua das flores', 658, 'casa', 'proximo ao mercado de cosmeticos'),
((SELECT id FROM cidades WHERE nome = 'Fukuoka'), 87965425, 'rua dos rosais', 895, 'edificio marinais', 'proximo a praça central'),
((SELECT id FROM cidades WHERE nome = 'Blumenau'), 89015255, 'rua florianópolis', 458, 'casa de alvenaria', 'final da rua'),
((SELECT id FROM cidades WHERE nome = 'Oásis de siwa'), 98543228, 'rua jorumiaki', 796, 'castelo tres torres', 'nas colinas do oeste'),
((SELECT id FROM cidades WHERE nome = 'Marua'), 12345678, 'rua roça', 123, 'casa', 'morro azul'),
((SELECT id FROM cidades WHERE nome = 'Kura'), 11234567, 'rua amerua', 321, 'casa de pedra', 'perto das dunas'),
((SELECT id FROM cidades WHERE nome = 'Balaci'), 14785236, 'rua ventura', 987, 'casa sem portao', 'ao lado do mercado'),
((SELECT id FROM cidades WHERE nome = 'Muria'), 96325874, 'rua zerumiru', 357, 'edificio azul', 'proximo a igreja'),
((SELECT id FROM cidades WHERE nome = 'Catalo'), 96325777, 'centro', 777, 'centro', 'proximo a capela 3 anjos');

INSERT INTO turistas (id_endereco, nome, sobrenome, sexo, cpf, rg, data_nascimento) VALUES
((SELECT id FROM enderecos WHERE cep = 14785236 AND numero = 658), 'João', 'Fernandes', 'Masculino', 74125878965, 7896523, '02-07-1998'),
((SELECT id FROM enderecos WHERE cep = 87965425 AND numero = 895), 'Antonio', 'Amaral', 'Masculino', 87965823654, 1478965, '05-12-1994'),
((SELECT id FROM enderecos WHERE cep = 89015255 AND numero = 458), 'Maria', 'Rosa', 'Feminino', 74523698541, 4569871, '03-11-1997'),
((SELECT id FROM enderecos WHERE cep = 98543228 AND numero = 796), 'Camila', 'Vieira', 'Feminino', 98745632147, 8796541, '04-02-1997');
   
INSERT INTO guias (id_endereco, nome, sobrenome, data_nascimento, sexo, cpf, rg, numero_carteira_trabalho, salario, categoria_habilitacao, rank_) VALUES
((SELECT id FROM enderecos WHERE cep = 12345678 AND numero = 123), 'Marcos', 'Antonio', '04-10-1990', 'M', 35789654123, 7532147, 12345678912, 2000, 'AB', 3),
((SELECT id FROM enderecos WHERE cep = 01234567 AND numero = 321), 'Marcio', 'Luz', '05-12-1900', 'M',  75325896325, 0147898, 11234567891, 3000, 'B', 4),
((SELECT id FROM enderecos WHERE cep = 14785236 AND numero =  987), 'Eduarda', 'Volx', '07-02-1995', 'F',  54896325418, 5789632, 22136547894, 4000, 'A', 2),
((SELECT id FROM enderecos WHERE cep = 96325874 AND numero = 357), 'Fernanda', 'Fortuna', '10-07-2000', 'F', 47896521478, 4789654, 78987456321, 1500, 'ABC', 1);

INSERT INTO idiomas (nome) VALUES
('Ingles'),
('Alemão'),
('Ingles'),
('Alemão');

INSERT INTO pontos_turisticos (id_endereco, nome) VALUES
((SELECT id FROM enderecos WHERE cep = 96325777 AND numero = 777), 'Praça dos Reis'),
((SELECT id FROM enderecos WHERE cep = 14785236 AND numero = 658), 'Ponte das Luzes'),
((SELECT id FROM enderecos WHERE cep = 98543228 AND numero = 796), 'Castelo Triste'),
((SELECT id FROM enderecos WHERE cep = 96325777 AND numero = 777), 'Igreja Matriz');

INSERT INTO pacotes_pontos_turisticos  (id_pacote, id_ponto_turistico) VALUES
((SELECT id FROM pacotes WHERE nome = 'Disney'), (SELECT id FROM pontos_turisticos WHERE nome = 'Praça dos Reis')),
((SELECT id FROM pacotes WHERE nome = 'Amsterdam'), (SELECT id FROM pontos_turisticos WHERE nome = 'Ponte das Luzes')),
((SELECT id FROM pacotes WHERE nome = 'Orlando'), (SELECT id FROM pontos_turisticos WHERE nome = 'Castelo triste')),
((SELECT id FROM pacotes WHERE nome = 'Paris'), (SELECT id FROM pontos_turisticos WHERE nome = 'Igreja Matriz'));

INSERT INTO viagens (id_guia, id_pacote, data_compra, data_horario_saida, data_horario_volta) VALUES
((SELECT id FROM guias WHERE nome = 'Fernanda'), (SELECT id FROM pacotes WHERE nome = 'Disney'), '10-05-2012', '20120618 10:34:09 AM', '20120625 04:20:00:00 PM'),
((SELECT id FROM guias WHERE nome = 'Eduarda'), (SELECT id FROM pacotes WHERE nome = 'Amsterdam'), '07-08-2017', '20170907 07:30:00 AM', '20170908 02:15:00 PM' ),
((SELECT id FROM guias WHERE nome = 'Marcio'), (SELECT id FROM pacotes WHERE nome = 'Orlando'), '10-08-2015', '20151120 10:20:00 AM', '20151201 09:00:00 PM'),
((SELECT id FROM guias WHERE nome = 'Marcos'), (SELECT id FROM pacotes WHERE nome = 'Paris'), '01-02-2016', '20160225 08:25:00 AM', '20160303 03:35:00 PM');

INSERT INTO viagens_turistas (id_turista, id_viagem, valor) VALUES
((SELECT id FROM turistas WHERE nome = 'Fernanda'), (SELECT id FROM viagens WHERE data_compra = '10-05-2012'), 4000),
((SELECT id FROM turistas WHERE nome = 'Eduarda'), (SELECT id FROM viagens WHERE data_compra = '07-08-2017'), 5000),
((SELECT id FROM turistas WHERE nome = 'Marcio'), (SELECT id FROM viagens WHERE data_compra = '10-08-2015'), 3500),
((SELECT id FROM turistas WHERE nome = 'Marcos'), (SELECT id FROM viagens WHERE data_compra = '01-02-2016'), 4500);

INSERT INTO turistas_pacotes (id_turista, id_pacote, status_do_pedido, data_requisicao) VALUES
((SELECT id FROM turistas WHERE nome = 'João'), (SELECT id FROM pacotes WHERE nome = 'Disney'), '1', '2018-09-16'),
((SELECT id FROM turistas WHERE nome = 'Maria'), (SELECT id FROM pacotes WHERE nome = 'Amsterdam'), '1', '2018-09-16'),
((SELECT id FROM turistas WHERE nome = 'Eduarda'), (SELECT id FROM pacotes WHERE nome = 'Paris'), '1', '2018-09-16');