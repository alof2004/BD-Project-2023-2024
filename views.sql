
use p8g3;

--Quintas 
drop view IF EXISTS AgroTrack.Quinta
go
create view AgroTrack.Quinta as
	select Q.Empresa_Id_Empresa, E.Nome,E.Morada, E.Contacto
	from  (AgroTrack_Quinta as Q join AgroTrack_Empresa as E on Q.Empresa_Id_Empresa=E.Id_Empresa)
go

--Empresa
drop view IF EXISTS AgroTrack.Empresa
go
create view AgroTrack.Empresa as
	select E.Id_Empresa,E.Nome,E.Morada, E.Contacto, E.Tipo_De_Empresa
	from  AgroTrack_Empresa as E
go

--Agricultores e quintas - info dos agricultores e a que quintas pertecem 
drop view IF EXISTS AgroTrack.AgriculQuinta
go
create view AgroTrack.drop view IF EXISTS AgroTrack.AgriculQuinta
 as
	select A.Id_Trabalhador, A.Pessoa_N_CartaoCidadao, A.Quinta_Empresa_Id_Empresa, E.Nome as NomeQuinta, E.Morada, Q.Empresa_Id_Empresa, P.Nome, P.Contacto
	from  (AgroTrack_Agricultor as A join AgroTrack_Pessoa as P On A.Pessoa_N_CartaoCidadao=P.N_CartaoCidadao join AgroTrack_Quinta as Q on A.Quinta_Empresa_Id_Empresa=Q.Empresa_Id_Empresa join AgroTrack_Empresa as E on Q.Empresa_Id_Empresa=E.Id_Empresa)

--Animais e Quinta e tipo de animal
drop view IF EXISTS AgroTrack.AnimaisQuinta
go
create view AgroTrack.AnimaisQuinta as
	select Ani.Id_Animal, Ani.Tipo_de_Animal, QA.Idade, QA.Brinco, QA.Empresa_Id_Empresa
	from  ((AgroTrack_Animal as Ani join AgroTrack_Quinta_Animal as QA on Ani.Id_Animal=QA.Id_Animal)
	inner join AgroTrack_Quinta as Q on QA.Empresa_Id_Empresa=Q.Empresa_Id_Empresa)
go

--Plantas e Quinta e tipo de planta
drop view IF EXISTS AgroTrack.PlantasQuinta
go
create view AgroTrack.PlantasQuinta as
	select PL.Id_planta, PL.Tipo,PL.Estacao, QP.Empresa_Id_Empresa, QP.Lote
	from  ((AgroTrack_Planta as PL join AgroTrack_Quinta_Planta as QP on PL.Id_planta=QP.Id_planta)
	inner join AgroTrack_Quinta as Q on QP.Empresa_Id_Empresa=Q.Empresa_Id_Empresa)
go

--Animais e plantas de cada Quinta
drop view IF EXISTS AgroTrack.PlantasAnimaisQuinta
go
create view  AgroTrack.PlantasAnimaisQuinta as
	select PL.Id_planta, PL.Tipo,PL.Estacao, Ani.Id_Animal, Ani.Tipo_de_Animal, Q.Empresa_Id_Empresa
	from  ((((AgroTrack_Planta as PL join AgroTrack_Quinta_Planta as QP on PL.Id_planta=QP.Id_planta)
	inner join AgroTrack_Quinta_Animal as QA on QP.Empresa_Id_Empresa=QA.Empresa_Id_Empresa) inner join AgroTrack_Animal as Ani
	on  Ani.Id_Animal=QA.Id_Animal) inner join AgroTrack_Quinta as Q on QA.Empresa_Id_Empresa=Q.Empresa_Id_Empresa)
go

--Agricultor e contrato
drop view IF EXISTS AgroTrack.AgriculConquinta
go
create view AgroTrack.AgriculConquinta as
	select C.ID, A.Id_Trabalhador, A.Pessoa_N_CartaoCidadao, C.[Date_str], C.[Date_end], Salario, C.Descricao
	from  ((AgroTrack_Agricultor as A join AgroTrack_Contrato as C on A.Pessoa_N_CartaoCidadao=C.Agricultor_Pessoa_N_CartaoCidadao)
	inner join AgroTrack_Quinta as Q on A.Quinta_Empresa_Id_Empresa=Q.Empresa_Id_Empresa)
go

--Agricultor e Quinta e contrato
drop view IF EXISTS AgroTrack.AgriculQuintaContrato
go
create view AgroTrack.AgriculQuintaContrato as
	select C.ID, A.Id_Trabalhador, A.Pessoa_N_CartaoCidadao, C.[Date_str], C.[Date_end], Salario, C.Descricao, Q.Empresa_Id_Empresa, E.Nome as NomeQuinta, P.Nome, P.Contacto
	from  (((AgroTrack_Agricultor as A join AgroTrack_Contrato as C on A.Pessoa_N_CartaoCidadao=C.Agricultor_Pessoa_N_CartaoCidadao)
	inner join AgroTrack_Quinta as Q on A.Quinta_Empresa_Id_Empresa=Q.Empresa_Id_Empresa)
	inner join AgroTrack_Empresa as E on Q.Empresa_Id_Empresa=E.Id_Empresa)
	inner join AgroTrack_Pessoa as P on A.Pessoa_N_CartaoCidadao=P.N_CartaoCidadao
go
--retalhistas e Empresa
drop view IF EXISTS AgroTrack.RetalhistasE
go
create view AgroTrack.RetalhistasE as
	select R.Empresa_Id_Empresa, E.Nome, E.Morada,E.Contacto
	from  (AgroTrack_Retalhistas as R join AgroTrack_Empresa as E on R.Empresa_Id_Empresa=E.Id_Empresa)
go

--Empresa de transportes e Empresa
drop view IF EXISTS AgroTrack.TransportesE
go
create view AgroTrack.TransportesE as
	select T.Empresa_Id_Empresa, E.Nome,E.Morada,E.Contacto
	from  (AgroTrack_Empresa_De_Transportes as T join AgroTrack_Empresa as E on T.Empresa_Id_Empresa=E.Id_Empresa)
go


--Encomenda e items
drop view IF EXISTS AgroTrack.EncomendaItems
go
create view AgroTrack.EncomendaItems as
	select Enc.Codigo, Enc.prazo_entrega, Enc.Morada_entrega, Enc.Entrega, Enc.Retalhista_Empresa_Id_Empresa,
	Enc.Empresa_De_Transportes_Id_Empresa,Enc.Quinta_Empresa_Id, I.Quantidade, I.ProdutoCodigo, I.Encomenda_Codigo, P.Nome as NomeProduto
	from (((AgroTrack_Encomenda as Enc
	join AgroTrack_Item as I on Enc.Codigo=I.Encomenda_Codigo)
	inner join AgroTrack_Produto as P on I.ProdutoCodigo=P.Codigo))
go


--Clientes e compra e produtos
drop view IF EXISTS AgroTrack.ClienteCompra
go
create view AgroTrack.ClienteCompra as
	select Cli.Pessoa_N_CartaoCidadao, Com.ID_Quinta, Com.Produto_codigo, Com.Quantidade, Com.Preco, Com.Metodo_de_pagamento,Pro.Nome, Pro.Tipo_de_Produto, Com.DataCompra
	from  ((AgroTrack_Cliente as Cli
	join AgroTrack_Compra as Com on Cli.Pessoa_N_CartaoCidadao=Com.Cliente_Pessoa_N_CartaoCidadao)
	inner join AgroTrack_Produto as Pro on Com.Produto_codigo=Pro.Codigo)
go
drop view IF EXISTS AgroTrack.ClienteCompraQuinta
go
create view AgroTrack.ClienteCompraQuinta as
	select Cli.Pessoa_N_CartaoCidadao, Com.ID_Quinta, Com.Produto_codigo, Com.Quantidade, Com.Preco, Com.Metodo_de_pagamento,Pro.Nome, Pro.Tipo_de_Produto, Com.DataCompra, Q.Empresa_Id_Empresa, E.Nome as QuintaN, E.Morada, E.Contacto
	from  (((AgroTrack_Cliente as Cli
	join AgroTrack_Compra as Com on Cli.Pessoa_N_CartaoCidadao=Com.Cliente_Pessoa_N_CartaoCidadao)
	inner join AgroTrack_Produto as Pro on Com.Produto_codigo=Pro.Codigo)
	inner join AgroTrack_Quinta as Q on Com.ID_Quinta=Q.Empresa_Id_Empresa)
	inner join AgroTrack_Empresa as E on Q.Empresa_Id_Empresa=E.Id_Empresa
go

--Produto
drop view IF EXISTS AgroTrack.Produto
go
create view AgroTrack.Produto as
	select Pro.Codigo,Pro.Nome, Pro.Preco,Pro.Taxa_de_iva,Pro.Unidade_medida,Pro.Tipo_de_Produto
	from  AgroTrack_Produto as Pro 
go

--Produto e item
drop view IF EXISTS AgroTrack.ProdutoItem
go
create view AgroTrack.ProdutoItem as
	select Pro.Codigo, Pro.Nome, I.Quantidade, I.Encomenda_Codigo
	from  (AgroTrack_Produto as Pro join AgroTrack_Item as I on Pro.Codigo=I.ProdutoCodigo)
go

--Produto e  Quinta e contem e Empresa
drop view IF EXISTS AgroTrack.QuintaProduto
go
create view AgroTrack.QuintaProduto as
	select Q.Empresa_Id_Empresa, E.Nome, E.Morada, E.Contacto, C.Quantidade, C.Data_de_validade, P.Nome as NomeProduto, P.Tipo_de_Produto, P.Codigo, P.Unidade_medida, P.Preco, P.Taxa_de_iva
	from  (((AgroTrack_Quinta as Q join AgroTrack_Empresa as E on Q.Empresa_Id_Empresa=E.Id_Empresa)
	inner join AgroTrack_Contem as C on Q.Empresa_Id_Empresa=C.Quinta_Empresa_Id_Empresa)
	inner join AgroTrack_Produto as P on C.Produto_codigo=P.Codigo)
go

--Agricultor e Colhe
drop view IF EXISTS AgroTrack.Agriculcolhe
go
create view AgroTrack.Agriculcolhe as
	select A.Id_Trabalhador, A.Pessoa_N_CartaoCidadao,A.Quinta_Empresa_Id_Empresa,colhe.Duracao_colheita,colhe.Quantidade,colhe.Produto_codigo, colhe.DataColheita, Pro.Nome as NomeProduto, Pro.Codigo, Pro.Unidade_medida
	from  (AgroTrack_Agricultor as A join AgroTrack_Colhe as colhe on A.Pessoa_N_CartaoCidadao=colhe.Agricultor_Pessoa_N_CartaoCidadao Join AgroTrack_Produto as Pro on colhe.Produto_codigo=Pro.Codigo)
go

--Empresa e Encomenda
drop view IF EXISTS AgroTrack.EmpresaEncomenda
go
create view AgroTrack.EmpresaEncomenda as
	select E.Nome, E.Morada, E.Contacto, Enc.Codigo, Enc.prazo_entrega, Enc.Morada_entrega, Enc.Entrega, Enc.Retalhista_Empresa_Id_Empresa,Enc.Empresa_De_Transportes_Id_Empresa, Enc.Quinta_Empresa_Id, Enc.PrecoTotal
	from  (AgroTrack_Empresa as E join AgroTrack_Encomenda as Enc on E.Id_Empresa=Enc.Retalhista_Empresa_Id_Empresa)
go	
--Encomenda
drop view IF EXISTS AgroTrack.Encomenda
go
create view AgroTrack.Encomenda as
	select Enc.Codigo, Enc.prazo_entrega, Enc.Morada_entrega, Enc.Entrega, Enc.Retalhista_Empresa_Id_Empresa,Enc.Empresa_De_Transportes_Id_Empresa, Enc.Quinta_Empresa_Id, Enc.PrecoTotal
	from  AgroTrack_Encomenda as Enc
go

drop view IF EXISTS AgroTrack.planta
go
create view AgroTrack.planta as
	select AgroTrack_Planta.Id_planta, AgroTrack_Planta.Tipo,AgroTrack_Planta.Estacao
	from  AgroTrack_Planta
go

drop view IF EXISTS AgroTrack.Animal
go
create view AgroTrack.Animal as
	Select AgroTrack_Animal.Id_Animal, AgroTrack_Animal.Tipo_de_Animal
	From AgroTrack_Animal
go

--Contem
drop view IF EXISTS AgroTrack.Contem
go
create view AgroTrack.Contem as
	select Contem.Quinta_Empresa_Id_Empresa, Contem.Produto_codigo, Contem.Quantidade
	from  AgroTrack_Contem as Contem
go

--Encomenda e Retalhista
drop view IF EXISTS AgroTrack.EncomendaRetalhista
go
create view AgroTrack.EncomendaRetalhista as
	select R.Empresa_Id_Empresa, E.Nome,E.Morada,E.Contacto, Enc.Codigo, Enc.prazo_entrega, Enc.Morada_entrega, Enc.Entrega, Enc.Retalhista_Empresa_Id_Empresa, Enc.PrecoTotal
	from  ((AgroTrack_Retalhistas as R join AgroTrack_Empresa as E on R.Empresa_Id_Empresa=E.Id_Empresa) inner join AgroTrack_Encomenda as Enc on R.Empresa_Id_Empresa=Enc.Retalhista_Empresa_Id_Empresa)
go

--Retalhista e empresa
drop view IF EXISTS AgroTrack.RetalhistaEmpresa
go
create view AgroTrack.RetalhistaEmpresa as
	select R.Empresa_Id_Empresa, E.Nome,E.Morada,E.Contacto
	from  (AgroTrack_Retalhistas as R join AgroTrack_Empresa as E on R.Empresa_Id_Empresa=E.Id_Empresa)
go

--Encomenda e Empresa de transportes
drop view IF EXISTS AgroTrack.EncomendaTransportes
go
create view AgroTrack.EncomendaTransportes as
	select T.Empresa_Id_Empresa, E.Nome,E.Morada,E.Contacto, Enc.Codigo, Enc.prazo_entrega, Enc.Morada_entrega, Enc.Entrega, Enc.Empresa_De_Transportes_Id_Empresa, Enc.PrecoTotal
	from  ((AgroTrack_Empresa_De_Transportes as T join AgroTrack_Empresa as E on T.Empresa_Id_Empresa=E.Id_Empresa) inner join AgroTrack_Encomenda as Enc on T.Empresa_Id_Empresa=Enc.Empresa_De_Transportes_Id_Empresa)
go 
--Cliente
drop view IF EXISTS AgroTrack.Cliente
go
create view AgroTrack.Cliente as
	select Cliente.Pessoa_N_CartaoCidadao, Pessoa.Nome, Pessoa.Contacto
	from  (AgroTrack_Cliente as Cliente join AgroTrack_Pessoa as Pessoa on Cliente.Pessoa_N_CartaoCidadao=Pessoa.N_CartaoCidadao)
go

--Produto e Info de produtos na quinta
DROP VIEW IF EXISTS AgroTrack.ProdutoContem;
GO
CREATE VIEW AgroTrack.ProdutoContem AS
WITH RankedProducts AS (
    SELECT P.Codigo, P.Nome, P.Preco, P.Taxa_de_iva, P.Unidade_medida, P.Tipo_de_Produto, C.Quantidade, C.Quinta_Empresa_Id_Empresa, ROW_NUMBER() OVER (PARTITION BY P.Codigo ORDER BY P.Codigo) AS RowNum
    FROM AgroTrack_Produto AS P
    LEFT JOIN AgroTrack_Contem AS C ON P.Codigo = C.Produto_codigo
    LEFT JOIN AgroTrack_Quinta AS Q ON C.Quinta_Empresa_Id_Empresa = Q.Empresa_Id_Empresa
)
SELECT Codigo, Nome, Preco, Taxa_de_iva, Unidade_medida, Tipo_de_Produto, Quantidade, Quinta_Empresa_Id_Empresa
FROM RankedProducts
WHERE RowNum = 1;
GO

-- Produto Empresa e quinta produto
drop view IF EXISTS AgroTrack.ProdutoEmpresaQuinta
go
create view AgroTrack.ProdutoEmpresaQuinta as
	select P.Codigo, E.Nome, P.Preco, P.Taxa_de_iva, P.Unidade_medida, P.Tipo_de_Produto, C.Quantidade, C.Quinta_Empresa_Id_Empresa, E.Morada, E.Contacto
	from  ((((AgroTrack_Produto as P join AgroTrack_Contem as C on P.Codigo=C.Produto_codigo) inner join AgroTrack_Empresa as E on C.Quinta_Empresa_Id_Empresa=E.Id_Empresa)) inner join AgroTrack_Quinta as Q on C.Quinta_Empresa_Id_Empresa=Q.Empresa_Id_Empresa)
go