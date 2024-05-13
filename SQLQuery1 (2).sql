--create DATABASE AgroTrack
--go 
IF OBJECT_ID ('AgroTrack_Compra','U') IS NOT NULL
	DROP TABLE dbo.AgroTrack_Compra;
IF OBJECT_ID ('AgroTrack_Item','U') IS NOT NULL
	DROP TABLE dbo.AgroTrack_Item;
IF OBJECT_ID ('AgroTrack_Encomenda','U') IS NOT NULL
	DROP TABLE dbo.AgroTrack_Encomenda;
IF OBJECT_ID ('AgroTrack_Colhe','U') IS NOT NULL
	DROP TABLE dbo.AgroTrack_Colhe;
IF OBJECT_ID ('AgroTrack_Contem','U') IS NOT NULL
	DROP TABLE dbo.AgroTrack_Contem;
IF OBJECT_ID ('AgroTrack_Empresa_De_Transportes','U') IS NOT NULL
	DROP TABLE dbo.AgroTrack_Empresa_De_Transportes;
IF OBJECT_ID ('AgroTrack_Retalhistas','U') IS NOT NULL
	DROP TABLE dbo.AgroTrack_Retalhistas;
IF OBJECT_ID ('AgroTrack_Contrato','U') IS NOT NULL
	DROP TABLE dbo.AgroTrack_Contrato;
	IF OBJECT_ID ('AgroTrack_Produto') IS NOT NULL
	DROP TABLE dbo.AgroTrack_Produto;
IF OBJECT_ID ('AgroTrack_Agricultor','U') IS NOT NULL
	DROP TABLE dbo.AgroTrack_Agricultor;
IF OBJECT_ID ('AgroTrack_Cliente','U') IS NOT NULL
	DROP TABLE dbo.AgroTrack_Cliente;
	IF OBJECT_ID ('AgroTrack_Quinta_Planta','U') IS NOT NULL
	DROP TABLE dbo.AgroTrack_Quinta_Planta;
IF OBJECT_ID ('AgroTrack_Planta','U') IS NOT NULL
	DROP TABLE dbo.AgroTrack_Planta;
IF OBJECT_ID ('AgroTrack_Quinta_Animal','U') IS NOT NULL
	DROP TABLE dbo.AgroTrack_Quinta_Animal;
IF OBJECT_ID ('AgroTrack_Animal','U') IS NOT NULL
	DROP TABLE dbo.AgroTrack_Animal;
IF OBJECT_ID ('AgroTrack_Quinta','U') IS NOT NULL
	DROP TABLE dbo.AgroTrack_Quinta;
IF OBJECT_ID ('AgroTrack_Empresa','U') IS NOT NULL
	DROP TABLE dbo.AgroTrack_Empresa;
IF OBJECT_ID ('AgroTrack_Pessoa','U') IS NOT NULL
	DROP TABLE dbo.AgroTrack_Pessoa;




create table AgroTrack_Pessoa (
	Nome				varchar(64)			not null,
	N_CartaoCidadao		int					not null		check(len(N_CartaoCidadao)=8),
	Contacto			int					not null		check(len(Contacto)=9),

	PRIMARY KEY(N_CartaoCidadao)
);


create table AgroTrack_Empresa (
	Id_Empresa			int					not null,
	Nome				varchar(64)			not null,
    Morada				varchar(64)			not null,

	PRIMARY KEY(Id_Empresa),
	unique (Nome)
);

create table AgroTrack_Quinta(
	Codigo_quinta		int					not null,
	Empresa_Id_Empresa	int,

	PRIMARY KEY(Empresa_Id_Empresa)
);

create table AgroTrack_Animal(
	Id_Animal			int					not null,
	Tipo_de_Animal		varchar(64)			not null,	

	PRIMARY KEY (Id_Animal)

);

create table AgroTrack_Quinta_Animal(
	Empresa_Id_Empresa	int,
	Idade				int					not null,
	Brinco			    varchar(16)		    not null,
	Id_Animal			int					not null,

	PRIMARY KEY (Id_Animal,Empresa_Id_Empresa,Brinco)

);

create table AgroTrack_Planta(
	Id_planta			int					not null,
	Tipo				varchar(32)			not null,
    Estacao VARCHAR(32) NOT NULL CHECK (Estacao IN ('Primavera', 'Verao', 'Outono', 'Inverno', 'Todas')),

	PRIMARY KEY (Id_planta)

);
create table AgroTrack_Quinta_Planta(
	Empresa_Id_Empresa	int,
	Lote				varchar(32)		    not null,
	Id_Planta			int					not null,

	PRIMARY KEY (Id_Planta,Empresa_Id_Empresa,Lote)

);



create table AgroTrack_Cliente (
	Pessoa_N_CartaoCidadao int,

	PRIMARY KEY(Pessoa_N_CartaoCidadao)

);

create table AgroTrack_Agricultor (
	Id_Trabalhador			int			    not null,
	Pessoa_N_CartaoCidadao	int,
	Quinta_Empresa_Id_Empresa	int,

	PRIMARY KEY(Pessoa_N_CartaoCidadao)
);

create table AgroTrack_Produto(
	Nome				varchar(64)			not null,
	Quantidade			int					not null,		
	Id_origem			int					not null,		
	Tipo_de_Produto		int					not null,
	Codigo				int					not null, 
	Preco				float               not null        check(Preco > 0),	
	[Data_de_validade]	date				not null,
	Taxa_de_iva			float				not null		check (Taxa_de_iva IN (0.06, 0.13, 0.23)),
	Unidade_medida      int					not null        check (Unidade_medida IN ('unidade', 'kg', 'g', 'litro', 'ml')),
	Agricultor_Pessoa_N_CartaoCidadao int,

	PRIMARY KEY (Codigo)

);
create table AgroTrack_Contrato(
	[Date_str]			date				not null,
	[Date_end]			date				not null,
	Descricao			varchar(64) 		not null,
	Salario				float               not null        check(Salario > 0),
	ID					int					not null, 
	Agricultor_Pessoa_N_CartaoCidadao	int,
	
	CHECK (Date_end > [Date_str]),
    PRIMARY KEY(ID),
    UNIQUE ([Descricao])
);



create table AgroTrack_Retalhistas (
	Empresa_Id_Empresa			int,


	PRIMARY KEY(Empresa_Id_Empresa)
);


create table AgroTrack_Empresa_De_Transportes (
	Empresa_Id_Empresa			int,

	PRIMARY KEY(Empresa_Id_Empresa)
);

create table AgroTrack_Contem (
	Produto_codigo			int,
	Quinta_Empresa_Id_Empresa int,
	Quantidade			int					not null,

	PRIMARY KEY(Produto_codigo,Quinta_Empresa_Id_Empresa) 
);

create table AgroTrack_Colhe (
	Empresa_Id_Empresa			int,
	Agricultor_Pessoa_N_CartaoCidadao int, 
	Duracao_colheita	float				not null,
	Quantidade			int					not null,
	Produto_codigo			int,


	PRIMARY KEY(Agricultor_Pessoa_N_CartaoCidadao,Empresa_Id_Empresa)
);


create table AgroTrack_Encomenda(
	Codigo				int					not null,
	prazo_entrega		int					not null,
	Morada_entrega		varchar(64)			not null, 
	Entrega				date				not null,
	Retalhista_Empresa_Id_Empresa	int,
	Empresa_De_Transportes_Id_Empresa	int,
	Quinta_Empresa_Id	int,


	PRIMARY KEY (Codigo)
);

create table AgroTrack_Item(
	Codigo				int					not null,
	Quantidade			int					not null, 
	Encomenda_Codigo	int,

	PRIMARY KEY (Codigo,Encomenda_Codigo)

);

create table AgroTrack_Compra(
	Produto_codigo			int,
	Cliente_Pessoa_N_CartaoCidadao	int,
	Preco				float               not null        check(Preco > 0),
	Quantidade			int					not null, 
	Metodo_de_pagamento varchar(64)			not null,

	PRIMARY KEY (Produto_codigo,Cliente_Pessoa_N_CartaoCidadao)

); 



alter table AgroTrack_Quinta add constraint Empresa_Id_EmpresaPk_Q foreign key(Empresa_Id_Empresa) references AgroTrack_Empresa(Id_Empresa); 


alter table AgroTrack_Cliente add constraint Pessoa_N_Cartao_CidadaoPK_C foreign key(Pessoa_N_CartaoCidadao) references AgroTrack_Pessoa(N_CartaoCidadao);
alter table AgroTrack_Cliente add constraint Quinta_Empresa_Id_EmpresaFK_C foreign key(Quinta_Empresa_Id_Empresa) references AgroTrack_Quinta(Empresa_Id_Empresa);
					 
alter table AgroTrack_Retalhistas add constraint Empresa_Id_EmpresaPK_R foreign key(Empresa_Id_Empresa) references AgroTrack_Empresa(Id_Empresa);

alter table AgroTrack_Agricultor add constraint Quinta_Empresa_Id_EmpresaFK_A foreign key(Quinta_Empresa_Id_Empresa) references AgroTrack_Quinta(Empresa_Id_Empresa); 

alter table AgroTrack_Quinta_Animal add constraint Id_AnimalFK_AN foreign key(Id_Animal) references AgroTrack_Animal(Id_Animal); 
alter table AgroTrack_Quinta_Animal add constraint Empresa_Id_EmpresaFK_AN foreign key(Empresa_Id_Empresa) references AgroTrack_Quinta(Empresa_Id_Empresa); 
					 
alter table AgroTrack_Quinta_Planta add constraint Id_PlantaFK_P foreign key(Id_Planta) references AgroTrack_Planta(Id_Planta); 
alter table AgroTrack_Quinta_Planta add constraint Empresa_Id_EmpresaFK_P foreign key(Empresa_Id_Empresa) references AgroTrack_Quinta(Empresa_Id_Empresa); 
					 
alter table AgroTrack_Empresa_De_Transportes add constraint Empresa_Id_EmpresaFK_E foreign key(Empresa_Id_Empresa) references AgroTrack_Empresa(Id_Empresa);

alter table AgroTrack_Retalhistas add constraint Empresa_Id_EmpresaFK foreign key(Empresa_Id_Empresa) references AgroTrack_Empresa(Id_Empresa); 
					 
alter table AgroTrack_Contem add constraint Produto_CodigoFK_CO foreign key(Produto_codigo) references AgroTrack_Produto(Codigo); 
alter table AgroTrack_Contem add constraint Quinta_Empresa_Id_EmpresaFK_CO foreign key(Quinta_Empresa_Id_Empresa) references AgroTrack_Quinta(Empresa_Id_Empresa); 
					 
					 
alter table AgroTrack_Colhe add constraint Agricultor_Pessoa_N_CartaoCidadaoFK_C foreign key(Agricultor_Pessoa_N_CartaoCidadao) references AgroTrack_Agricultor(Pessoa_N_CartaoCidadao); 
alter table AgroTrack_Colhe add constraint Produto_codigoFK_C foreign key(Produto_codigo) references AgroTrack_Produto(Codigo); 
					 
alter table AgroTrack_Contrato add constraint Agricultor_Pessoa_N_CartaoCidadaoFK foreign key(Agricultor_Pessoa_N_CartaoCidadao) references AgroTrack_Agricultor(Pessoa_N_CartaoCidadao); 
					 
alter table AgroTrack_Item add constraint Encomenda_CodigoFK foreign key(Encomenda_Codigo) references AgroTrack_Encomenda(Codigo); 
					 
alter table AgroTrack_Encomenda add constraint Retalhista_Empresa_Id_EmpresaFK foreign key(Retalhista_Empresa_Id_Empresa) references AgroTrack_Retalhistas(Empresa_Id_Empresa); 
alter table AgroTrack_Encomenda add constraint Empresa_De_Transportes_Id_EmpresaFK foreign key(Empresa_De_Transportes_Id_Empresa) references AgroTrack_Empresa_De_Transportes(Empresa_Id_Empresa); 
alter table AgroTrack_Encomenda add constraint Quinta_Empresa_IdFK foreign key(Quinta_Empresa_Id) references AgroTrack_Quinta(Empresa_Id_Empresa);
					
alter table AgroTrack_Produto add constraint Agricultor_Pessoa_N_CartaoCidadaoFK_P foreign key(Agricultor_Pessoa_N_CartaoCidadao) references AgroTrack_Agricultor(Pessoa_N_CartaoCidadao); 
					 
alter table AgroTrack_Compra add constraint Produto_codigoFK_Compra foreign key(Produto_codigo) references AgroTrack_Produto(Codigo); 
alter table AgroTrack_Compra add constraint Client_Pessoa_N_CartaoCidadaoFK_Compra foreign key(Cliente_Pessoa_N_CartaoCidadao) references AgroTrack_Cliente(Pessoa_N_CartaoCidadao); 
					 