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

SELECT * FROM guias;
