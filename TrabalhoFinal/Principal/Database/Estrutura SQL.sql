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
    senha VARCHAR(128),
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
    cpf VARCHAR(14),
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
	ativo BIT DEFAULT '1',
	data_requisicao DATE
	FOREIGN KEY (id_turista) REFERENCES turistas(id),
	FOREIGN KEY (id_pacote) REFERENCES pacotes(id)
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
('admin@admin.com', 'd61d004c03457bac7b90c1e8d4f51113be162346b27af5307caffe21ef88597ff15ab1569e07155302ff7b0af29f7f0431531004568da3849a5708176815a70f', 'Administrador'),
('v@a.com', '6275aa6bc22ffa83a1c3f7bf8a559dcded6ded804b449c2a90e8a8b8b2ecdf08894de351039a98a1d4de0fe58fd0d35c02bee760de5ae781a02ce68ac08a44a1', 'Administrador'),
('user@u.com', '8af874dfc6ca0920d1bf81f08972e483c53436796da0da2c8ae08b251b1e00776a4c7b6db8e7ae2bf6e1869e34e8149ff88623c40a0d3e443e6c4639c38ec16b', 'Usuário'),
('user@u2.com', 'a8360e04e4144307b5e1d06b9298a908aaa9846923191dc4ceff123f7360a269049950126d00bbe1a0328300614ec0692be297015767b56f5e67dd2caeb3df1f', 'Usuário'),
('f@f.com', 'e1bebd885e0679d69e7b515c60b6e99eb8d9025903b6069db911c1dbc0358f82620c6302a51abe350727355c792e65578ef6715f352ecf2e77011dfbc3563e46', 'Funcionário'),
('f@f2.com', '288fd03c0f00a7bd9fa1124747c87a2bd55a6a6701e45c3ee5c440e0044fcedcb8dc13d34473090be74fdafb9604268dd805f2e6267f1e91208878166dfdc7b5', 'Funcionário');

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
((SELECT id FROM cidades WHERE nome = 'Hamburgo'), 14785236, 'Rua das flores', 658, 'Casa', 'Proximo ao mercado de cosmeticos'),
((SELECT id FROM cidades WHERE nome = 'Fukuoka'), 87965425, 'Rua dos rosais', 895, 'Edificio marinais', 'Proximo a praça central'),
((SELECT id FROM cidades WHERE nome = 'Blumenau'), 89015255, 'Rua florianópolis', 458, 'Casa de alvenaria', 'Final da rua'),
((SELECT id FROM cidades WHERE nome = 'Oásis de siwa'), 98543228, 'Rua jorumiaki', 796, 'Castelo tres torres', 'Nas colinas do oeste'),
((SELECT id FROM cidades WHERE nome = 'Marua'), 12345678, 'Rua roça', 123, 'Casa', 'Morro azul'),
((SELECT id FROM cidades WHERE nome = 'Kura'), 11234567, 'Rua amerua', 321, 'Casa de pedra', 'Perto das dunas'),
((SELECT id FROM cidades WHERE nome = 'Balaci'), 14785236, 'Rua ventura', 987, 'Casa sem portao', 'Ao lado do mercado'),
((SELECT id FROM cidades WHERE nome = 'Muria'), 96325874, 'Rua zerumiru', 357, 'Edificio azul', 'Proximo a igreja'),
((SELECT id FROM cidades WHERE nome = 'Catalo'), 96325777, 'Centro', 777, 'Centro', 'Proximo a capela 3 anjos');

INSERT INTO turistas (id_login ,id_endereco, nome, sobrenome, sexo, cpf, rg, data_nascimento) VALUES
(3,(SELECT id FROM enderecos WHERE cep = 87965425 AND numero = 895), 'Antonio', 'Amaral', 'Masculino', 87965823654, 1478965, '05-12-1994'),
(4,(SELECT id FROM enderecos WHERE cep = 89015255 AND numero = 458), 'Maria', 'Rosa', 'Feminino', 74523698541, 4569871, '03-11-1997'),
(5,(SELECT id FROM enderecos WHERE cep = 14785236 AND numero = 658), 'João', 'Fernandes', 'Masculino', 74125878965, 7896523, '02-07-1998'),
(6,(SELECT id FROM enderecos WHERE cep = 98543228 AND numero = 796), 'Camila', 'Vieira', 'Feminino', 98745632147, 8796541, '04-02-1997');
   
INSERT INTO guias (id_login, id_endereco, nome, sobrenome, data_nascimento, sexo, cpf, rg, numero_carteira_trabalho, salario, categoria_habilitacao, rank_) VALUES
(1, (SELECT id FROM enderecos WHERE cep = 12345678 AND numero = 123), 'Marcos', 'Antonio', '04-10-1990', 'M', 35789654123, 7532147, 12345678912, 2000, 'AB', 3),
(2, (SELECT id FROM enderecos WHERE cep = 12345678 AND numero = 123), 'Victor', 'Hugo', '11-09-2001', 'M', 07611034901, 7240414, 4444512, 10000, 'AB', 5);


INSERT INTO idiomas (nome) VALUES
('Russo'),
('Alemão'),
('Português'),
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

INSERT INTO viagens_turistas (id_turista, id_viagem) VALUES
((SELECT id FROM turistas WHERE nome = 'Fernanda'), (SELECT id FROM viagens WHERE data_compra = '10-05-2012')),
((SELECT id FROM turistas WHERE nome = 'Eduarda'), (SELECT id FROM viagens WHERE data_compra = '07-08-2017')),
((SELECT id FROM turistas WHERE nome = 'Marcio'), (SELECT id FROM viagens WHERE data_compra = '10-08-2015')),
((SELECT id FROM turistas WHERE nome = 'Marcos'), (SELECT id FROM viagens WHERE data_compra = '01-02-2016'));

INSERT INTO turistas_pacotes (id_turista, id_pacote, status_do_pedido, data_requisicao) VALUES
((SELECT id FROM turistas WHERE nome = 'João'), (SELECT id FROM pacotes WHERE nome = 'Disney'), 'Aguardando Aprovação', '2018-09-16'),
((SELECT id FROM turistas WHERE nome = 'Maria'), (SELECT id FROM pacotes WHERE nome = 'Amsterdam'), 'Aguardando Pagamento', '2017-12-25'),
((SELECT id FROM turistas WHERE nome = 'Camila'), (SELECT id FROM pacotes WHERE nome = 'Paris'), 'Aguardando Aprovação', '2017-08-10');