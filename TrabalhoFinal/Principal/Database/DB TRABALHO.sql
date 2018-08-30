DROP TABLE historico_de_viagens;
DROP TABLE viagens_turistas;
DROP TABLE viagens;
DROP TABLE pacotes_pontos_turisticos;
DROP TABLE pontos_turisticos;
DROP TABLE pacotes;
DROP TABLE idiomas;
DROP TABLE guias;
DROP TABLE turistas;
DROP TABLE enderecos;
DROP TABLE cidades;
DROP TABLE estados;
DROP TABLE paises;
DROP TABLE continentes;




CREATE TABLE continentes (
	id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	nome VARCHAR(100) NOT NULL
);


CREATE TABLE paises (
	id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	id_continente INT,
	nome VARCHAR(100) NOT NULL,
	FOREIGN KEY (id_continente) REFERENCES continentes(id)
);

CREATE TABLE estados (
	id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	id_pais INT NOT NULL,
	nome VARCHAR(100) NOT NULL,
	FOREIGN KEY (id_pais) REFERENCES paises(id)
);


CREATE TABLE cidades (
	id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	id_estado INT NOT NULL,
	nome VARCHAR(100) NOT NULL,
	FOREIGN KEY (id_estado) REFERENCES estados(id)
);


CREATE TABLE enderecos (
	id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	id_cidade INT NOT NULL,
	cep VARCHAR(8) NOT NULL,
	logradouro VARCHAR(150) NOT NULL,
	numero SMALLINT NOT NULL,
	complemento VARCHAR(20),
	referencia VARCHAR(400),
	FOREIGN KEY (id_cidade) REFERENCES cidades(id)
);


CREATE TABLE turistas (	
	login_ VARCHAR(100),
	sexo CHAR(1),
    senha VARCHAR(100),
    ativo CHAR(1) DEFAULT '1',
    perfil VARCHAR(15) DEFAULT 'USER',
    id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	id_endereco INT,
    nome VARCHAR(100) NOT NULL,
    sobrenome VARCHAR(100) NOT NULL,
    cpf VARCHAR(11) NOT NULL,
    rg VARCHAR(20) NOT NULL,
    data_nascimento DATE NOT NULL,	
	premium BIT DEFAULT 0,
	FOREIGN KEY (id_endereco) REFERENCES enderecos(id)
);


CREATE TABLE guias (
	login_ VARCHAR(100),
	sexo VARCHAR(10),
	senha VARCHAR(100),
    ativo CHAR(1) DEFAULT '1',
    perfil VARCHAR(15) DEFAULT 'ADMIN',
	id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	id_endereco INT,
	nome VARCHAR(100) NOT NULL,
	sobrenome VARCHAR(100) NOT NULL,
	numero_carteira_trabalho VARCHAR(11) NOT NULL,
	categoria_habilitacao VARCHAR(10) NOT NULL,	
	salario FLOAT NOT NULL,
	cpf VARCHAR(11) NOT NULL,
    rg VARCHAR(20) NOT NULL,
    data_nascimento DATE NOT NULL,	
	rank_ SMALLINT, 
	FOREIGN KEY (id_endereco) REFERENCES enderecos(id)
);


CREATE TABLE idiomas (
	id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	id_guia INT,
	nome VARCHAR(100),
	FOREIGN KEY (id_guia) REFERENCES guias(id)
);



CREATE TABLE pacotes (
	id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	nome VARCHAR(100),
	valor FLOAT,
	percentual_max_desconto TINYINT,
);

CREATE TABLE pontos_turisticos (
	id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,	
	id_endereco INT,
	nome VARCHAR(100) NOT NULL,
	FOREIGN KEY (id_endereco) REFERENCES enderecos(id)
);


CREATE TABLE pacotes_pontos_turisticos (
	id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	id_ponto_turistico INT,
	id_pacote INT ,
	FOREIGN KEY (id_ponto_turistico) REFERENCES pontos_turisticos(id),
	FOREIGN KEY (id_pacote) REFERENCES pacotes(id),
); --NXN


CREATE TABLE viagens (
	id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	data DATE NOT NULL,
	id_pacote INT,
	id_guia INT,
	data_horario_saida DATETIME,
	data_horario_volta DATETIME,
	FOREIGN KEY (id_pacote) REFERENCES pacotes(id),
	FOREIGN KEY (id_guia) REFERENCES guias(id)
);


CREATE TABLE viagens_turistas (
	id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	id_turista INT,
	id_viagem INT,
	valor FLOAT NOT NULL,
	FOREIGN KEY (id_turista) REFERENCES turistas(id),
	FOREIGN KEY (id_viagem) REFERENCES viagens(id)	
);	--NXN


CREATE TABLE historico_de_viagens (
	id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	id_pacote INT NOT NULL,
	data_ DATE NOT NULL,
	FOREIGN KEY (id_pacote) REFERENCES pacotes(id)
);

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

INSERT INTO continentes (nome) VALUES
('Europeu'),
('Asiático'),
('Americano'),
('Africano');

INSERT INTO paises (id_continente,nome) VALUES
((SELECT id FROM continentes WHERE nome = 'Europeu'), 'Alemanha'),
((SELECT id FROM continentes WHERE nome = 'Asiático'), 'Japão'),
((SELECT id FROM continentes WHERE nome = 'Americano'), 'Brasil'),
((SELECT id FROM continentes WHERE nome = 'Africano'), 'Egito'),
((SELECT id FROM continentes WHERE nome = 'Europeu'), 'Belgica'),
((SELECT id FROM continentes WHERE nome = 'Asiático'), 'China'),
((SELECT id FROM continentes WHERE nome = 'Americano'), 'Guatemala'),
((SELECT id FROM continentes WHERE nome = 'Africano'), 'Angola');


INSERT INTO estados (id_pais, nome) VALUES
((SELECT id FROM paises WHERE nome = 'Alemanha'), 'Berlim'),
((SELECT id FROM paises WHERE nome = 'Japão'), 'Fukuoka'),
((SELECT id FROM paises WHERE nome = 'Brasil'), 'Santa Catarina'),
((SELECT id FROM paises WHERE nome = 'Egito'), 'siwa'),
((SELECT id FROM paises WHERE nome = 'Belgica'), 'Somuona'),
((SELECT id FROM paises WHERE nome = 'China'), 'Chorinchi'),
((SELECT id FROM paises WHERE nome = 'Guatemala'), 'Azemara'),
((SELECT id FROM paises WHERE nome = 'Angola'), 'Tunama');

INSERT INTO cidades (id_estado, nome) VALUES
((SELECT id FROM estados WHERE nome = 'Berlim'), 'Hamburgo'),
((SELECT id FROM estados WHERE nome = 'Fukuoka'), 'Fukuoka'),
((SELECT id FROM estados WHERE nome = 'Santa Catarina'), 'Blumenau'),
((SELECT id FROM estados WHERE nome = 'siwa'), 'Oásis de siwa'),
((SELECT id FROM estados WHERE nome = 'Somuona'), 'Marua'),
((SELECT id FROM estados WHERE nome = 'Chorinchi'), 'Kura'),
((SELECT id FROM estados WHERE nome = 'Azemara'), 'Balaci'),
((SELECT id FROM estados WHERE nome = 'Tunama'), 'Muria');

INSERT INTO enderecos (id_cidade, cep, logradouro, numero, complemento, referencia) VALUES
((SELECT id FROM cidades WHERE nome = 'Hamburgo'), 14785236, 'rua das flores', 658, 'casa', 'proximo ao mercado de cosmeticos'),
((SELECT id FROM cidades WHERE nome = 'Fukuoka'), 87965425, 'rua dos rosais', 895, 'edificio marinais', 'proximo a praça central'),
((SELECT id FROM cidades WHERE nome = 'Blumenau'), 89015255, 'rua florianópolis', 458, 'casa de alvenaria', 'final da rua'),
((SELECT id FROM cidades WHERE nome = 'Oásis de siwa'), 98543228, 'rua jorumiaki', 796, 'castelo tres torres', 'nas colinas do oeste'),
((SELECT id FROM cidades WHERE nome = 'Marua'), 12345678, 'rua marinaria', 123, 'casa amarela', 'morro azul'),
((SELECT id FROM cidades WHERE nome = 'Kura'), 01234567, 'rua amerua', 321, 'casa de pedra', 'perto das dunas'),
((SELECT id FROM cidades WHERE nome = 'Balaci'), 14785236, 'rua ventura', 987, 'casa sem portao', 'ao lado do mercado'),
((SELECT id FROM cidades WHERE nome = 'Muria'), 96325874, 'rua zerumiru', 357, 'edificio azul', 'proximo a igreja');


INSERT INTO turistas (id_endereco, nome, sobrenome, sexo, cpf, rg, data_nascimento) VALUES
((SELECT id FROM enderecos WHERE cep = 14785236 AND numero = 658), 'Josão', 'Fernandes', 'Masculino', 74125878965, 7896523, '02-07-1998'),
((SELECT id FROM enderecos WHERE cep = 8796542 AND numero = 895), 'Antonio', 'Amaral', 'Masculino', 87965823654, 1478965, '05-12-1994'),
((SELECT id FROM enderecos WHERE cep = 89015255 AND numero = 458), 'Maria', 'Da Rosa', 'Feminino', 74523698541, 4569871, '10-14-1997'),
((SELECT id FROM enderecos WHERE cep =  98543228 AND numero =  796), 'Camila', 'Vieira', 'Feminino', 98745632147, 8796541, '04-16-1997');

   
INSERT INTO guias (id_endereco, nome, sobrenome, data_nascimento, sexo, cpf, rg, numero_carteira_trabalho, salario, categoria_habilitacao, rank_) VALUES
((SELECT id FROM enderecos WHERE cep = 12345678  AND numero = 123), 'Marcos', 'Antonio', '04-10-1990', 'Masculino', 35789654123, 7532147),
((SELECT id FROM enderecos WHERE cep = 01234567  AND numero = 321), 'Marcio', 'Luz', '05-12-1900', 'Masculino',  75325896325, 0147898),
((SELECT id FROM enderecos WHERE cep = 14785236  AND numero =  987), 'Eduarda', 'Volx', 'Feminino', '07-02-1995',  54896325418, 5789632),
((SELECT id FROM enderecos WHERE cep = 96325874  AND numero = 357), 'Fernanda', 'Fortuna', 'Feminino', '10-07-2000', 47896521478, 4789654);
