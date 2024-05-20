
use p8g3;

--Quintas 

drop view IF EXISTS AgroTrack.Quinta
go
create view AgroTrack.Quinta as
	select Q.Codigo_quinta, Q.Empresa_Id_Empresa, E.Nome,E.Morada
	from  (AgroTrack_Quinta as Q join AgroTrack_Empresa as E on Q.Empresa_Id_Empresa=E.Id_Empresa)
go

--Agricultores e quintas
-- info dos agricultores e a que quintas pertecem 

drop view IF EXISTS AgroTrack.AgriculQuinta
go
create view AgroTrack.AgriculQuinta as
	select A.Id_Trabalhador, A.Pessoa_N_CartaoCidadao,A.Quinta_Empresa_Id_Empresa, 
Q.Codigo_quinta, Q.Empresa_Id_Empresa
	from  (AgroTrack_Agricultor as A join AgroTrack_Quinta as Q on A.Quinta_Empresa_Id_Empresa= Q.Empresa_Id_Empresa)
go


--Animais e Quinta e tipo de animal
drop view IF EXISTS AgroTrack.AnimaisQuinta
go
create view AgroTrack.AnimaisQuinta as
	select Ani.Id_Animal, Ani.Tipo_de_Animal, QA.Idade, QA.Brinco, QA.Empresa_Id_Empresa
	from  ((AgroTrack_Animal as Ani join AgroTrack_Quinta_Animal as QA on Ani.Id_Animal=QA.Id_Animal) inner join AgroTrack_Quinta as Q on QA.Empresa_Id_Empresa=Q.Empresa_Id_Empresa)
go

--Plantas e Quinta e tipo de planta
drop view IF EXISTS AgroTrack.PlantasQuinta
go
create view AgroTrack.PlantasQuinta as
	select PL.Id_planta, PL.Tipo,PL.Estacao, QP.Empresa_Id_Empresa, QP.Lote
	from  ((AgroTrack_Planta as PL join AgroTrack_Quinta_Planta as QP on PL.Id_planta=QP.Id_planta) inner join AgroTrack_Quinta as Q on QP.Empresa_Id_Empresa=Q.Empresa_Id_Empresa)
go

--Animais e plantas de cada Quinta
drop view IF EXISTS AgroTrack.PlantasAnimaisQuinta
go
create view  AgroTrack.PlantasAnimaisQuinta as
	select PL.Id_planta, PL.Tipo,PL.Estacao, Ani.Id_Animal, Ani.Tipo_de_Animal, Q.Codigo_quinta, Q.Empresa_Id_Empresa
	from  ((((AgroTrack_Planta as PL join AgroTrack_Quinta_Planta as QP on PL.Id_planta=QP.Id_planta) inner join AgroTrack_Quinta_Animal as QA on QP.Empresa_Id_Empresa=QA.Empresa_Id_Empresa) inner join AgroTrack_Animal as Ani
	on  Ani.Id_Animal=QA.Id_Animal) inner join AgroTrack_Quinta as Q on QA.Empresa_Id_Empresa=Q.Empresa_Id_Empresa)
go

--Agricultor e contrato
drop view IF EXISTS AgroTrack.AgriculConquinta
go
create view AgroTrack.AgriculConquinta as
	select C.ID, A.Id_Trabalhador, A.Pessoa_N_CartaoCidadao, C.[Date_str], C.[Date_end], Salario, Q.Codigo_quinta, Q.Empresa_Id_Empresa
	from  ((AgroTrack_Agricultor as A join AgroTrack_Contrato as C on A.Pessoa_N_CartaoCidadao=C.Agricultor_Pessoa_N_CartaoCidadao) inner join AgroTrack_Quinta as Q on A.Quinta_Empresa_Id_Empresa=Q.Empresa_Id_Empresa)
go

--retalhistas e Empresa
drop view IF EXISTS AgroTrack.RetalhistasE
go
create view AgroTrack.Retalhistas as
	select R.Empresa_Id_Empresa, E.Nome, E.Morada
	from  (AgroTrack_Retalhistas as R join AgroTrack_Empresa as E on R.Empresa_Id_Empresa=E.Id_Empresa)
go

--Empresa de transportes e Empresa
drop view IF EXISTS AgroTrack.TransportesE
go
create view AgroTrack.TransportesE as
	select T.Empresa_Id_Empresa, E.Nome,E.Morada
	from  (AgroTrack_Empresa_De_Transportes as T join AgroTrack_Empresa as E on T.Empresa_Id_Empresa=E.Id_Empresa)
go


--Encomenda e items
drop view IF EXISTS AgroTrack.EncomendaItems
go
create view AgroTrack.EncomendaItems as
	select Enc.Codigo, Enc.prazo_entrega, Enc.Morada_entrega, Enc.Entrega, Enc.Retalhista_Empresa_Id_Empresa,Enc.Empresa_De_Transportes_Id_Empresa, Enc.Quinta_Empresa_Id
	from  (AgroTrack_Encomenda as Enc join AgroTrack_Item as I on Enc.Codigo=I.Encomenda_Codigo)
go


--Clientes e compra e produtos
drop view IF EXISTS AgroTrack.ClienteCompra
go
create view AgroTrack.ClienteCompra as
	select Cli.Pessoa_N_CartaoCidadao, Com.ID_Quinta, Com.Produto_codigo, Com.Quantidade, Com.Preco, Com.Metodo_de_pagamento,Pro.Nome, Pro.Tipo_de_Produto
	from  ((AgroTrack_Cliente as Cli join AgroTrack_Compra as Com on Cli.Pessoa_N_CartaoCidadao=Com.Cliente_Pessoa_N_CartaoCidadao) inner join AgroTrack_Produto as Pro on Com.Produto_codigo=Pro.Codigo)
go


--Produto
drop view IF EXISTS AgroTrack.Produto
go
create view AgroTrack.Produto as
	select Pro.Nome, Pro.Id_origem, Pro.Preco,Pro.Taxa_de_iva,Pro.Unidade_medida,Pro.Tipo_de_Produto
	from  AgroTrack_Produto as Pro 
go


--Produto e  Quinta e contem
drop view IF EXISTS AgroTrack.QuintaProduto
go
create view AgroTrack.QuintaProduto as
	select Q.Codigo_quinta, Pro.Nome, Pro.Id_origem, Pro.Preco,Pro.Taxa_de_iva,Pro.Unidade_medida,Pro.Tipo_de_Produto
	from  ((AgroTrack_Quinta as Q join AgroTrack_Contem as Cont on Q.Empresa_Id_Empresa=Cont.Quinta_Empresa_Id_Empresa ) inner join AgroTrack_Produto as Pro on Pro.Codigo=Cont.Produto_codigo)
go

--Agricultor e Colhe
drop view IF EXISTS AgroTrack.Agriculcolhe
go
create view AgroTrack.Agriculcolhe as
	select A.Id_Trabalhador, A.Pessoa_N_CartaoCidadao,A.Quinta_Empresa_Id_Empresa,colhe.Duracao_colheita,colhe.Quantidade,colhe.Produto_codigo, colhe.DataColheita
	from  (AgroTrack_Agricultor as A join AgroTrack_Colhe as colhe on A.Pessoa_N_CartaoCidadao=colhe.Agricultor_Pessoa_N_CartaoCidadao)
go




