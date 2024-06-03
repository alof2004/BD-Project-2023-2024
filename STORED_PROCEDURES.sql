IF OBJECT_ID('AgroTrack.AddEncomendaTransportes', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.AddEncomendaTransportes;
GO
IF OBJECT_ID('AgroTrack.AddEncomendaRetalhistas', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.AddEncomendaRetalhistas;
GO

IF OBJECT_ID('AgroTrack.RemovePlantFromQuinta', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.RemovePlantFromQuinta;
GO

IF OBJECT_ID('AgroTrack.AddRetalhista', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.AddRetalhista;
GO
IF OBJECT_ID('AgroTrack.ApagarTransporte', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.ApagarTransporte;
GO

IF OBJECT_ID('AgroTrack.ApagarRetalhista', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.ApagarRetalhista;
GO

IF OBJECT_ID('AgroTrack.ApagarProduto', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.ApagarProduto;
GO

IF OBJECT_ID('AgroTrack.AddProdutoToQuinta', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.AddProdutoToQuinta;
GO

IF OBJECT_ID('AgroTrack.AddQuinta', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.AddQuinta;
GO
CREATE PROCEDURE AgroTrack.AddQuinta
    @Nome VARCHAR(64),
    @Morada VARCHAR(64),
    @Contacto INT
AS
BEGIN
	DECLARE @NEXTCodigoEmpresa  INT;

    SELECT @NextCodigoEmpresa = COALESCE(MAX(Id_Empresa), 0) + 1 FROM AgroTrack_Empresa;

    INSERT INTO AgroTrack_Empresa (Id_Empresa, Nome, Morada, Contacto, Tipo_De_Empresa)
    VALUES (@NextCodigoEmpresa, @Nome, @Morada, @Contacto, 'Quinta');
    INSERT INTO AgroTrack_Quinta (Empresa_Id_Empresa)
    VALUES (@NextCodigoEmpresa); 

    PRINT 'New Quinta added successfully.';
END
GO
IF OBJECT_ID('AgroTrack.AddAnimalToQuinta', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.AddAnimalToQuinta;
GO
CREATE PROCEDURE AgroTrack.AddAnimalToQuinta
    @Id_Animal INT,
    @Idade INT,
    @Brinco VARCHAR(16),
    @Quinta_Id VARCHAR(64)
AS
BEGIN
    IF @Quinta_Id IS NULL
    BEGIN
        RAISERROR ('Quinta with the provided name does not exist', 16, 1);
        RETURN;
    END

    INSERT INTO AgroTrack_Quinta_Animal (Empresa_Id_Empresa, Idade, Brinco, Id_Animal)
    VALUES (@Quinta_Id, @Idade, @Brinco, @Id_Animal);
    PRINT 'New animal added to the Quinta successfully.';
END
GO
IF OBJECT_ID('AgroTrack.AddPlantaToQuinta', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.AddPlantaToQuinta;
GO
CREATE PROCEDURE AgroTrack.AddPlantaToQuinta
    @IdPlanta int,
    @Estacao VARCHAR(32),
    @Lote VARCHAR(32),
    @QuintaId INT
AS
BEGIN
    IF @QuintaId IS NULL
    BEGIN
        RAISERROR ('A Quinta com o nome fornecido não existe', 16, 1);
        RETURN;
    END

    INSERT INTO AgroTrack_Quinta_Planta (Empresa_Id_Empresa, Lote, Id_Planta)
    VALUES (@QuintaId, @Lote, (SELECT Id_planta FROM AgroTrack_Planta WHERE Id_planta = @IdPlanta));

    PRINT 'Nova planta adicionada à Quinta com sucesso.';
END
GO
IF OBJECT_ID('AgroTrack.AddAgricultor', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.AddAgricultor;
GO
CREATE PROCEDURE AgroTrack.AddAgricultor
    @Nome VARCHAR(64),
    @N_CartaoCidadao INT,
    @Salario FLOAT,
    @DescricaoContrato VARCHAR(64),
    @QuintaId INT,
    @Contacto INT,
    @ContractStartDate DATE,
    @ContractEndDate DATE
AS
BEGIN
    DECLARE @PessoaId INT;
    DECLARE @ContratoId INT;
    DECLARE @IDAgricultor INT;

    IF EXISTS (SELECT 1 FROM AgroTrack_Agricultor WHERE Pessoa_N_CartaoCidadao = @N_CartaoCidadao)
    BEGIN
        RAISERROR ('Esta pessoa já está registada como agricultor', 16, 1);
        RETURN;
    END

    IF @QuintaId IS NULL
    BEGIN
        RAISERROR ('A Quinta com o nome fornecido não existe', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM AgroTrack_Pessoa WHERE N_CartaoCidadao = @N_CartaoCidadao)
    BEGIN
        INSERT INTO AgroTrack_Pessoa (Nome, N_CartaoCidadao, Contacto)
        VALUES (@Nome, @N_CartaoCidadao, @Contacto);
    END

    BEGIN TRANSACTION;
    BEGIN TRY;
    SELECT @PessoaId = N_CartaoCidadao FROM AgroTrack_Pessoa WHERE N_CartaoCidadao = @N_CartaoCidadao;

    SELECT @IDAgricultor = ISNULL(MAX(Id_Trabalhador), 0) + 1 FROM AgroTrack_Agricultor WHERE Quinta_Empresa_Id_Empresa = @QuintaId;
    INSERT INTO AgroTrack_Agricultor (Id_Trabalhador, Pessoa_N_CartaoCidadao, Quinta_Empresa_Id_Empresa)
    VALUES (@IDAgricultor, @PessoaId, @QuintaId);

    SELECT @ContratoID = ISNULL(MAX(ID), 0) + 1 FROM AgroTrack_Contrato;
    INSERT INTO AgroTrack_Contrato ([Date_str], [Date_end], Descricao, Salario, ID, Agricultor_Pessoa_N_CartaoCidadao)
    VALUES (@ContractStartDate, @ContractEndDate, @DescricaoContrato, @Salario, @ContratoID, @PessoaId);

    PRINT 'Novo agricultor adicionado à Quinta com sucesso.';
    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END
GO
IF OBJECT_ID('AgroTrack.AddProduto', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.AddProduto;
GO

CREATE PROCEDURE AgroTrack.AddProduto
    @NomeProduto VARCHAR(64),
    @Tipo_de_Produto VARCHAR(64),
    @Preco FLOAT,
    @Taxa_de_iva VARCHAR(16),
    @Unidade_medida VARCHAR(16)
AS
BEGIN
    DECLARE @Codigo INT;
    
    SELECT @Codigo = ISNULL(MAX(Codigo), 0) + 1 FROM AgroTrack_Produto;
    
    INSERT INTO AgroTrack_Produto (Codigo, Nome, Tipo_de_Produto, Preco, Taxa_de_iva, Unidade_medida)
    VALUES (@Codigo, @NomeProduto, @Tipo_de_Produto, @Preco, @Taxa_de_iva, @Unidade_medida);
    PRINT 'Novo produto adicionado com sucesso.';
END
GO

IF OBJECT_ID('AgroTrack.AddProdutoToQuinta', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.AddProdutoToQuinta;
GO
CREATE PROCEDURE AgroTrack.AddProdutoToQuinta
    @ProdutoId VARCHAR(64),
    @QuintaId VARCHAR(64),
    @Quantidade INT,
    @DataDeValidade DATE
AS
BEGIN
    IF @QuintaId IS NULL
    BEGIN
        RAISERROR ('A Quinta com o nome fornecido não existe', 16, 1);
        RETURN;
    END

    BEGIN TRANSACTION;
    
    IF EXISTS (SELECT 1 FROM AgroTrack_Contem WHERE Produto_Codigo = @ProdutoId AND Quinta_Empresa_Id_Empresa = @QuintaId)
    BEGIN
        UPDATE AgroTrack_Contem
        SET Quantidade = Quantidade + @Quantidade, 
            Data_de_validade = @DataDeValidade
        WHERE Produto_Codigo = @ProdutoId AND Quinta_Empresa_Id_Empresa = @QuintaId;
    END
    ELSE
    BEGIN
        INSERT INTO AgroTrack_Contem (Produto_Codigo, Quinta_Empresa_Id_Empresa, Quantidade, Data_de_validade)
        VALUES (@ProdutoId, @QuintaId, @Quantidade, @DataDeValidade);
    END

    COMMIT TRANSACTION;

    PRINT 'Novo produto adicionado à Quinta com sucesso.';
END;
GO

IF OBJECT_ID('AgroTrack.AddEncomenda', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.AddEncomenda;
GO
CREATE PROCEDURE AgroTrack.AddEncomenda
    @Codigo INT,
    @Prazo_entrega INT,
    @Morada_entrega VARCHAR(64),
    @Entrega DATE,
    @Retalhista_Empresa_Id_Empresa INT,
    @Empresa_De_Transportes_Id_Empresa INT,
    @Quinta_Empresa_Id INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        INSERT INTO AgroTrack_Encomenda (
            Codigo,
            prazo_entrega,
            Morada_entrega,
            Entrega,
            Retalhista_Empresa_Id_Empresa,
            Empresa_De_Transportes_Id_Empresa,
            Quinta_Empresa_Id
        )
        VALUES (
            @Codigo,
            @Prazo_entrega,
            @Morada_entrega,
            @Entrega,
            @Retalhista_Empresa_Id_Empresa,
            @Empresa_De_Transportes_Id_Empresa,
            @Quinta_Empresa_Id
        );

        PRINT 'Encomenda added successfully.';
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO

IF OBJECT_ID('AgroTrack.AddItemToEncomenda', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.AddItemToEncomenda;
GO
CREATE PROCEDURE AgroTrack.AddItemToEncomenda
    @ProdutoCodigo INT,
    @Quantidade INT,
    @EncomendaCodigo INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        INSERT INTO AgroTrack_Item (
            ProdutoCodigo,
            Quantidade,
            Encomenda_Codigo
        )
        VALUES (
            @ProdutoCodigo,
            @Quantidade,
            @EncomendaCodigo
        );

        PRINT 'Item added to Encomenda successfully.';
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO

IF OBJECT_ID('AgroTrack.AddColheita', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.AddColheita;
GO

CREATE PROCEDURE AgroTrack.AddColheita
    @Agricultor_Pessoa_N_CartaoCidadao INT,
    @Duracao_colheita FLOAT,
    @Quantidade INT,
    @Produto_codigo INT,
    @DataColheita DATE,
    @Data_de_validade DATE
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
        INSERT INTO AgroTrack_Colhe (
            Agricultor_Pessoa_N_CartaoCidadao,
            Duracao_colheita,
            Quantidade,
            Produto_codigo,
            DataColheita
        )
        VALUES (
            @Agricultor_Pessoa_N_CartaoCidadao,
            @Duracao_colheita,
            @Quantidade,
            @Produto_codigo,
            @DataColheita
        );

        DECLARE @Quinta_Empresa_Id_Empresa INT;

        SELECT @Quinta_Empresa_Id_Empresa = Quinta_Empresa_Id_Empresa 
        FROM AgroTrack_Agricultor 
        WHERE Pessoa_N_CartaoCidadao = @Agricultor_Pessoa_N_CartaoCidadao;

        IF EXISTS (SELECT 1 FROM AgroTrack_Contem WHERE Produto_codigo = @Produto_codigo AND Quinta_Empresa_Id_Empresa = @Quinta_Empresa_Id_Empresa)
        BEGIN
            UPDATE AgroTrack_Contem
            SET Quantidade = Quantidade + @Quantidade, 
                Data_de_validade = @Data_de_validade
            WHERE Produto_codigo = @Produto_codigo AND Quinta_Empresa_Id_Empresa = @Quinta_Empresa_Id_Empresa;
        END
        ELSE
        BEGIN
            INSERT INTO AgroTrack_Contem (
                Produto_codigo,
                Quinta_Empresa_Id_Empresa,
                Data_de_validade,
                Quantidade
            )
            VALUES (
                @Produto_codigo,
                @Quinta_Empresa_Id_Empresa,
                @Data_de_validade,
                @Quantidade
            );
        END

        COMMIT TRANSACTION;

        PRINT 'Colheita and product addition completed successfully.';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
GO

IF OBJECT_ID('AgroTrack.RemoveProdutosForaDeValidadeFromQuinta', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.RemoveProdutosForaDeValidadeFromQuinta;
GO
CREATE PROCEDURE AgroTrack.RemoveProdutosForaDeValidadeFromQuinta @QuintaId INT
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
        DELETE FROM AgroTrack_Contem
        WHERE Quinta_Empresa_Id_Empresa = @QuintaId AND [Data_de_validade] < GETDATE();

        COMMIT TRANSACTION;

        PRINT 'Produtos fora de validade removidos com sucesso.';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
GO
IF OBJECT_ID('AgroTrack.PesquisarPorNomeEmpresa', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.PesquisarPorNomeEmpresa;
GO
CREATE PROCEDURE AgroTrack.PesquisarPorNomeEmpresa
    @Nome VARCHAR(64),
    @esquema VARCHAR(50),
    @tabela VARCHAR(50)
as
	begin
		declare @query nvarchar(MAX);

		set @query = 'select * from ' + QUOTENAME(@esquema) + '.' + QUOTENAME(@tabela) + ' where [Nome] like ''%' + REPLACE(@Nome, '''', '''''') + '%''';

		execute sp_executesql @query;
	end;
GO
IF OBJECT_ID('AgroTrack.PesquisarPorNome', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.PesquisarPorNome;
GO
CREATE PROCEDURE AgroTrack.PesquisarPorNome
    @Nome VARCHAR(64),
    @esquema VARCHAR(50),
    @tabela VARCHAR(50)
AS
	begin
		declare @query nvarchar(MAX);

		set @query = 'select * from ' + QUOTENAME(@esquema) + '.' + QUOTENAME(@tabela) + ' where [Nome] like ''%' + REPLACE(@Nome, '''', '''''') + '%''';

		execute sp_executesql @query;
	end;
GO

IF OBJECT_ID('AgroTrack.ApagarQuinta', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.ApagarQuinta;
GO
CREATE PROCEDURE AgroTrack.ApagarQuinta
    @Empresa_Id_Empresa INT
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
        DELETE FROM AgroTrack_Contem
        WHERE Quinta_Empresa_Id_Empresa = @Empresa_Id_Empresa; 

        DELETE FROM AgroTrack_Quinta
        WHERE Empresa_Id_Empresa = @Empresa_Id_Empresa;

        DELETE FROM AgroTrack_Empresa
        WHERE Id_Empresa = @Empresa_Id_Empresa;

        DELETE FROM AgroTrack_Agricultor
        WHERE Quinta_Empresa_Id_Empresa = @Empresa_Id_Empresa;

        DELETE FROM AgroTrack_Quinta_Animal
        WHERE Empresa_Id_Empresa = @Empresa_Id_Empresa;

        DELETE FROM AgroTrack_Quinta_Planta
        WHERE Empresa_Id_Empresa = @Empresa_Id_Empresa;

        COMMIT TRANSACTION;

        PRINT 'Quinta deleted successfully.';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
GO
IF OBJECT_ID('AgroTrack.CalcularPrecoEncomenda') IS NOT NULL
    DROP FUNCTION AgroTrack.CalcularPrecoEncomenda;
GO
CREATE FUNCTION AgroTrack.CalcularPrecoEncomenda(@EncomendaCodigo INT)
RETURNS FLOAT
AS
BEGIN
    DECLARE @Preco FLOAT;

    SELECT @Preco = SUM(Quantidade * Preco * (1 + p.Taxa_de_iva))
    FROM AgroTrack_Item i
    JOIN AgroTrack_Produto p ON i.ProdutoCodigo = p.Codigo
    WHERE Encomenda_Codigo = @EncomendaCodigo;

    RETURN @Preco;
END;
GO
IF OBJECT_ID('AgroTrack.ApagarAgricultor', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.ApagarAgricultor;
GO
CREATE PROCEDURE AgroTrack.ApagarAgricultor
    @Pessoa_N_CartaoCidadao INT
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
        DELETE FROM AgroTrack_Colhe
        WHERE Agricultor_Pessoa_N_CartaoCidadao = @Pessoa_N_CartaoCidadao;

        DELETE FROM AgroTrack_Contrato
        WHERE Agricultor_Pessoa_N_CartaoCidadao = @Pessoa_N_CartaoCidadao;

        DELETE FROM AgroTrack_Agricultor
        WHERE Pessoa_N_CartaoCidadao = @Pessoa_N_CartaoCidadao;

        COMMIT TRANSACTION;

        PRINT 'Agricultor deleted successfully.';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
GO
IF OBJECT_ID('AgroTrack.ApagarColheita', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.ApagarColheita;
GO
CREATE PROCEDURE AgroTrack.ApagarColheita
    @Agricultor_Pessoa_N_CartaoCidadao INT,
    @Produto_codigo INT,
    @DataColheita DATE
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
        DELETE FROM AgroTrack_Colhe
        WHERE Agricultor_Pessoa_N_CartaoCidadao = @Agricultor_Pessoa_N_CartaoCidadao
        AND Produto_codigo = @Produto_codigo
        AND DataColheita = @DataColheita;

        COMMIT TRANSACTION;

        PRINT 'Colheita deleted successfully.';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
GO

IF OBJECT_ID('AgroTrack.AdicionarCliente', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.AdicionarCliente;
GO
CREATE PROCEDURE AgroTrack.AdicionarCliente
    @Nome VARCHAR(64),
    @N_CartaoCidadao INT,
    @Contacto INT
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
        INSERT INTO AgroTrack_Pessoa (
            Nome,
            N_CartaoCidadao,
            Contacto
        )
        VALUES (
            @Nome,
            @N_CartaoCidadao,
            @Contacto
        );
        INSERT INTO AgroTrack_Cliente (
            Pessoa_N_CartaoCidadao
        )
        VALUES (
            @N_CartaoCidadao
        );

        COMMIT TRANSACTION;

        PRINT 'Cliente added successfully.';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
GO
IF OBJECT_ID('AgroTrack.ApagarCliente', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.ApagarCliente;
GO
CREATE PROCEDURE AgroTrack.ApagarCliente
    @Pessoa_N_CartaoCidadao INT
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
        DELETE FROM AgroTrack_Compra
        WHERE Cliente_Pessoa_N_CartaoCidadao = @Pessoa_N_CartaoCidadao;
        
        DELETE FROM AgroTrack_Cliente
        WHERE Pessoa_N_CartaoCidadao = @Pessoa_N_CartaoCidadao;

        DELETE FROM AgroTrack_Pessoa
        WHERE N_CartaoCidadao = @Pessoa_N_CartaoCidadao;

        COMMIT TRANSACTION;

        PRINT 'Cliente deleted successfully.';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
GO
IF OBJECT_ID('AgroTrack.AdicionarCompra', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.AdicionarCompra;
GO
CREATE PROCEDURE AgroTrack.AdicionarCompra
    @Cliente_Pessoa_N_CartaoCidadao INT,
    @Produto_codigo INT,
    @ID_Quinta INT,
    @Quantidade INT,
    @Data DATE,
    @Metodo_de_pagamento VARCHAR(32)
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
        INSERT INTO AgroTrack_Compra (
            Cliente_Pessoa_N_CartaoCidadao,
            Produto_codigo,
            ID_Quinta,
            Quantidade,
            DataCompra,
            Metodo_de_pagamento
        )
        VALUES (
            @Cliente_Pessoa_N_CartaoCidadao,
            @Produto_codigo,
            @ID_Quinta,
            @Quantidade,
            @Data,
            @Metodo_de_pagamento
        );

        COMMIT TRANSACTION;

        PRINT 'Compra added successfully.';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
GO
if OBJECT_ID('AgroTrack.OrdenarClientes', 'P') is not null
    drop procedure AgroTrack.OrdenarClientes;
GO
CREATE PROCEDURE AgroTrack.OrdenarClientes
    @Order VARCHAR(50)
AS
BEGIN
    IF @Order = 'NomeCrescente'
    BEGIN
        SELECT Pessoa_N_CartaoCidadao, Nome, Contacto
        FROM AgroTrack.Cliente
        ORDER BY Nome ASC; 
    END
    ELSE IF @Order = 'NomeDecrescente'
    BEGIN
        SELECT Pessoa_N_CartaoCidadao, Nome, Contacto
        FROM AgroTrack.Cliente
        ORDER BY Nome DESC;
    END
    ELSE IF @Order = 'TelefoneCrescente'
    BEGIN
        SELECT Pessoa_N_CartaoCidadao, Nome, Contacto
        FROM AgroTrack.Cliente
        ORDER BY Contacto ASC;
    END
    ELSE IF @Order = 'TelefoneDecrescente'
    BEGIN
        SELECT Pessoa_N_CartaoCidadao, Nome, Contacto
        FROM AgroTrack.Cliente
        ORDER BY Contacto DESC;
    END
    ELSE
    BEGIN
        SELECT NULL AS Pessoa_N_CartaoCidadao, NULL AS Nome, NULL AS Contacto
        WHERE 1 = 0;
    END
END;
GO
IF OBJECT_ID('AgroTrack.OrdenarAgricultores', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.OrdenarAgricultores;
GO
CREATE PROCEDURE AgroTrack.OrdenarAgricultores
    @Order VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @SqlQuery NVARCHAR(MAX);

    DECLARE @OrderByClause NVARCHAR(MAX) = 
        CASE 
            WHEN @Order = 'NomeCrescente' THEN 'Nome ASC'
            WHEN @Order = 'NomeDecrescente' THEN 'Nome DESC'
            WHEN @Order = 'IDCrescente' THEN 'Id_Trabalhador ASC'
            WHEN @Order = 'IDDecrescente' THEN 'Id_Trabalhador DESC'
            WHEN @Order = 'SalarioCrescente' THEN 'Salario ASC'
            WHEN @Order = 'SalarioDecrescente' THEN 'Salario DESC'
            ELSE 'Nome ASC' -- por default iremos ordenar por nome
        END;

    SET @SqlQuery = '
        SELECT Pessoa_N_CartaoCidadao, Nome, Contacto, Empresa_Id_Empresa, Salario, NomeQuinta, Id_Trabalhador
        FROM AgroTrack.AgriculQuintaContrato
        ORDER BY ' + @OrderByClause;

    EXEC sp_executesql @SqlQuery;
END
GO

-- Calculate the new Id_Empresa value
DECLARE @Id_Empresa INT;
SELECT @Id_Empresa = ISNULL(MAX(Id_Empresa), 0) + 1 FROM AgroTrack_Empresa;

-- AddEmpresaTransportes
IF OBJECT_ID('AgroTrack.AddEmpresaTransportes', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.AddEmpresaTransportes;
GO
CREATE PROCEDURE AgroTrack.AddEmpresaTransportes
    @Nome VARCHAR(64),
    @Morada VARCHAR(64),
    @Contacto INT
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
    
    DECLARE @Id_Empresa INT;
    SELECT @Id_Empresa = ISNULL(MAX(Id_Empresa), 0) + 1 FROM AgroTrack_Empresa;
        INSERT INTO AgroTrack_Empresa (
            Id_Empresa,
            Nome,
            Morada,
            Contacto,
            Tipo_De_Empresa
        )
        VALUES (
            @Id_Empresa,
            @Nome,
            @Morada,
            @Contacto,
            'Empresa de Transportes'
        );
        Insert into AgroTrack_Empresa_De_Transportes (Empresa_Id_Empresa)
        VALUES (@Id_Empresa);


        COMMIT TRANSACTION;

        PRINT 'Empresa de Transportes added successfully.';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;

IF OBJECT_ID('AgroTrack.ApagarTransporte', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.ApagarTransporte;
GO
CREATE PROCEDURE AgroTrack.ApagarTransporte
    @Empresa_Id_Empresa INT
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY

        DELETE FROM AgroTrack_Item
        WHERE Encomenda_Codigo IN (
            SELECT Codigo
            FROM AgroTrack_Encomenda
            WHERE Empresa_De_Transportes_Id_Empresa = @Empresa_Id_Empresa
        );
        
        Delete From AgroTrack_Encomenda
        WHERE Empresa_De_Transportes_Id_Empresa = @Empresa_Id_Empresa;

        DELETE FROM AgroTrack_Empresa_De_Transportes
        WHERE Empresa_Id_Empresa = @Empresa_Id_Empresa;

        DELETE FROM AgroTrack_Empresa
        WHERE Id_Empresa = @Empresa_Id_Empresa;

        
        
        COMMIT TRANSACTION;

        PRINT 'Empresa deleted successfully.';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;

IF OBJECT_ID('AgroTrack.ApagarRetalhista', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.ApagarRetalhista;
GO
CREATE PROCEDURE AgroTrack.ApagarRetalhista
    @Empresa_Id_Empresa INT
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY

        DELETE FROM AgroTrack_Retalhistas
        WHERE Empresa_Id_Empresa = @Empresa_Id_Empresa;

        DELETE FROM AgroTrack_Empresa
        WHERE Id_Empresa = @Empresa_Id_Empresa;

        
        
        COMMIT TRANSACTION;

        PRINT 'Empresa deleted successfully.';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;



-- AddRetalhista
IF OBJECT_ID('AgroTrack.AddRetalhista', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.AddRetalhista;
GO
CREATE PROCEDURE AgroTrack.AddRetalhista
    @Nome VARCHAR(64),
    @Morada VARCHAR(64),
    @Contacto INT
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
    
    DECLARE @Id_Empresa INT;
    SELECT @Id_Empresa = ISNULL(MAX(Id_Empresa), 0) + 1 FROM AgroTrack_Empresa;
        INSERT INTO AgroTrack_Empresa (
            Id_Empresa,
            Nome,
            Morada,
            Contacto,
            Tipo_De_Empresa
        )
        VALUES (
            @Id_Empresa,
            @Nome,
            @Morada,
            @Contacto,
            'Retalhista'
        );
        INSERT INTO AgroTrack_Retalhistas (Empresa_Id_Empresa)
        VALUES (@Id_Empresa);
        

        COMMIT TRANSACTION;

        PRINT 'Retalhista added successfully.';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
GO
IF OBJECT_ID('AgroTrack.RemoveProdutoFromQuinta', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.RemoveProdutoFromQuinta;
GO
CREATE PROCEDURE AgroTrack.RemoveProdutoFromQuinta
    @ProdutoId INT,
    @QuintaId INT
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
        DELETE FROM AgroTrack_Contem
        WHERE Produto_Codigo = @ProdutoId AND Quinta_Empresa_Id_Empresa = @QuintaId;

        COMMIT TRANSACTION;

        PRINT 'Produto removido da Quinta com sucesso.';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;

-- AddEncomendaTransportes
IF OBJECT_ID('AgroTrack.AddEncomendaTransportes', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.AddEncomendaTransportes;
GO
CREATE PROCEDURE AgroTrack.AddEncomendaTransportes
    @Prazo_entrega INT,
    @Morada_entrega VARCHAR(64),
    @Entrega DATE,
    @Retalhista_Empresa_Id_Empresa INT,
    @Empresa_De_Transportes_Id_Empresa INT,
    @Quinta_Empresa_Id INT

AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM AgroTrack_Retalhistas WHERE Empresa_Id_Empresa = @Retalhista_Empresa_Id_Empresa)
        BEGIN
            RAISERROR('O ID do retalhista não existe.', 16, 1);
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM AgroTrack_Empresa_De_Transportes WHERE Empresa_Id_Empresa = @Empresa_De_Transportes_Id_Empresa)
        BEGIN
            RAISERROR('O ID da empresa de transportes não existe.', 16, 1);
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM AgroTrack_Quinta WHERE Empresa_Id_Empresa = @Quinta_Empresa_Id)
        BEGIN
            RAISERROR('O ID da quinta não existe.', 16, 1);
            RETURN;
        END

        DECLARE @Codigo INT;
        SELECT @Codigo = ISNULL(MAX(Codigo), 0) + 1 FROM AgroTrack_Encomenda;

        INSERT INTO AgroTrack_Encomenda (
            Codigo,
            prazo_entrega,
            Morada_entrega,
            Entrega,
            Retalhista_Empresa_Id_Empresa,
            Empresa_De_Transportes_Id_Empresa,
            Quinta_Empresa_Id
        )
        VALUES (
            @Codigo,
            @Prazo_entrega,
            @Morada_entrega,
            @Entrega,
            @Retalhista_Empresa_Id_Empresa,
            @Empresa_De_Transportes_Id_Empresa,
            @Quinta_Empresa_Id
        );

        PRINT 'Encomenda adicionada com sucesso.';
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;


IF OBJECT_ID('AgroTrack.AddEncomendaRetalhistas', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.AddEncomendaRetalhistas;
GO
CREATE PROCEDURE AgroTrack.AddEncomendaRetalhistas
    @Prazo_entrega INT,
    @Morada_entrega VARCHAR(64),
    @Retalhista_Empresa_Id_Empresa INT,
    @Empresa_De_Transportes_Id_Empresa INT,
    @Quinta_Empresa_Id INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        DECLARE @Codigo INT;
        SELECT @Codigo = ISNULL(MAX(Codigo), 0) + 1 FROM AgroTrack_Encomenda;
        INSERT INTO AgroTrack_Encomenda (
            Codigo,
            prazo_entrega,
            Morada_entrega,
            Retalhista_Empresa_Id_Empresa,
            Empresa_De_Transportes_Id_Empresa,
            Quinta_Empresa_Id
        )
        VALUES (
            @Codigo,
            @Prazo_entrega,
            @Morada_entrega,
            @Retalhista_Empresa_Id_Empresa,
            @Empresa_De_Transportes_Id_Empresa,
            @Quinta_Empresa_Id
        );

        PRINT 'Encomenda added successfully.';
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;

IF OBJECT_ID('AgroTrack.RemovePlantFromQuinta', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.RemovePlantFromQuinta;
GO
CREATE PROCEDURE AgroTrack.RemovePlantFromQuinta
    @PlantaId INT,
    @QuintaId INT,
    @Lote varchar(32)
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
        DELETE FROM AgroTrack_Quinta_Planta
        WHERE Id_Planta = @PlantaId AND Empresa_Id_Empresa = @QuintaId AND Lote = @Lote;

        COMMIT TRANSACTION;

        PRINT 'Planta removida da Quinta com sucesso.';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        PRINT 'Ocorreu um erro ao remover a planta da Quinta.';
        THROW;
    END CATCH
END
GO

IF OBJECT_ID('AgroTrack.RemoveAnimalFromQuinta', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.RemoveAnimalFromQuinta;
GO
CREATE PROCEDURE AgroTrack.RemoveAnimalFromQuinta
    @AnimalId INT,
    @QuintaId INT,
    @Brinco varchar(32)
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
        DELETE FROM AgroTrack_Quinta_Animal
        WHERE Id_Animal = @AnimalId AND Empresa_Id_Empresa = @QuintaId AND Brinco = @Brinco;

        COMMIT TRANSACTION;

        PRINT 'Animal removido da Quinta com sucesso.';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        PRINT 'Ocorreu um erro ao remover o animal da Quinta.';
        THROW;
    END CATCH
END
GO

--RemoveEncomenda TRANSPORTES
IF OBJECT_ID('AgroTrack.ApagarEncomendaTransportes', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.ApagarEncomendaTransportes;
GO
CREATE PROCEDURE AgroTrack.ApagarEncomendaTransportes
    @Codigo INT
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
        DELETE FROM AgroTrack_Encomenda
        WHERE Codigo = @Codigo;

        COMMIT TRANSACTION;

        PRINT 'Encomenda deleted successfully.';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
go

--RemoveEncomenda RETALHISTAS
IF OBJECT_ID('AgroTrack.ApagarEncomendaRetalhista', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.ApagarEncomendaRetalhista;
GO
CREATE PROCEDURE AgroTrack.ApagarEncomendaRetalhista
    @Codigo INT
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY

        DELETE FROM AgroTrack_Item
        WHERE Encomenda_Codigo = @Codigo;
        
        DELETE FROM AgroTrack_Encomenda
        WHERE Codigo = @Codigo;


        COMMIT TRANSACTION;

        PRINT 'Encomenda deleted successfully.';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
GO
IF OBJECT_ID('AgroTrack.AddProductToEncomenda', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.AddProductToEncomenda;
GO
CREATE PROCEDURE AgroTrack.AddProductToEncomenda
    @Codigo INT,
    @Produto_codigo INT,
    @Quantidade INT
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
        INSERT INTO AgroTrack_Item (
            Encomenda_Codigo,
            ProdutoCodigo,
            Quantidade
        )
        VALUES (
            @Codigo,
            @Produto_codigo,
            @Quantidade
        );

        COMMIT TRANSACTION;

        PRINT 'Produto added to Encomenda successfully.';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
go
IF OBJECT_ID('AgroTrack.OrdenarProdutos', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.OrdenarProdutos;
GO
CREATE PROCEDURE AgroTrack.OrdenarProdutos
    @Order VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @SqlQuery NVARCHAR(MAX);

    DECLARE @OrderByClause NVARCHAR(MAX) = 
        CASE 
            WHEN @Order = 'NomeCrescente' THEN 'Nome ASC'
            WHEN @Order = 'NomeDecrescente' THEN 'Nome DESC'
            WHEN @Order = 'CodigoCrescente' THEN 'Codigo ASC'
            WHEN @Order = 'CodigoDecrescente' THEN 'Codigo DESC'
            WHEN @Order = 'PrecoCrescente' THEN 'Preco ASC'
            WHEN @Order = 'PrecoDecrescente' THEN 'Preco DESC'
            WHEN @Order = 'IvaCrescente' THEN 'Taxa_de_iva ASC'
            WHEN @Order = 'IvaDecrescente' THEN 'Taxa_de_iva DESC'
            ELSE 'Codigo ASC'
        END;

    SET @SqlQuery = '
        SELECT Codigo, Nome, Preco, Tipo_de_Produto, Taxa_de_iva, Unidade_medida
        FROM AgroTrack.Produto
        ORDER BY ' + @OrderByClause;

    EXEC sp_executesql @SqlQuery;
END
IF OBJECT_ID('AgroTrack.ApagarProduto', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.ApagarProduto;
GO
CREATE PROCEDURE AgroTrack.ApagarProduto
    @Codigo INT
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
        PRINT 'Deleting Produto with Codigo = ' + CAST(@Codigo AS NVARCHAR(10));

        DELETE FROM AgroTrack_Colhe
        WHERE Produto_codigo = @Codigo;

        DECLARE @ColheCount INT;
        SELECT @ColheCount = COUNT(*) FROM AgroTrack_Colhe WHERE Produto_codigo = @Codigo;
        PRINT 'Remaining rows in AgroTrack_Colhe for Produto_codigo = ' + CAST(@Codigo AS NVARCHAR(10)) + ': ' + CAST(@ColheCount AS NVARCHAR(10));

        DELETE FROM AgroTrack_Contem
        WHERE Produto_codigo = @Codigo;

        DECLARE @ContemCount INT;
        SELECT @ContemCount = COUNT(*) FROM AgroTrack_Contem WHERE Produto_codigo = @Codigo;
        PRINT 'Remaining rows in AgroTrack_Contem for Produto_codigo = ' + CAST(@Codigo AS NVARCHAR(10)) + ': ' + CAST(@ContemCount AS NVARCHAR(10));

        DELETE FROM AgroTrack_Item
        WHERE ProdutoCodigo = @Codigo;

        DECLARE @ItemCount INT;
        SELECT @ItemCount = COUNT(*) FROM AgroTrack_Item WHERE ProdutoCodigo = @Codigo;
        PRINT 'Remaining rows in AgroTrack_Item for ProdutoCodigo = ' + CAST(@Codigo AS NVARCHAR(10)) + ': ' + CAST(@ItemCount AS NVARCHAR(10));

        DELETE FROM AgroTrack_Produto
        WHERE Codigo = @Codigo;

        COMMIT TRANSACTION;

        PRINT 'Produto deleted successfully.';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        PRINT 'Error: ' + @ErrorMessage;
        PRINT 'Severity: ' + CAST(@ErrorSeverity AS NVARCHAR(10));
        PRINT 'State: ' + CAST(@ErrorState AS NVARCHAR(10));

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
GO
IF OBJECT_ID('AgroTrack.AlterarQuantidadeProdutoQuinta', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.AlterarQuantidadeProdutoQuinta;
GO
CREATE PROCEDURE AgroTrack.AlterarQuantidadeProdutoQuinta
    @IdProduto INT,
    @QuintaId INT,
    @NovaQuantidade INT
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
        UPDATE AgroTrack_Contem
        SET Quantidade = @NovaQuantidade
        WHERE Produto_Codigo = @IdProduto AND Quinta_Empresa_Id_Empresa = @QuintaId;

        COMMIT TRANSACTION;

        PRINT 'Quantidade do produto alterada com sucesso.';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;