IF OBJECT_ID('AgroTrack.AddRetalhista', 'P') IS NOT NULL
    DROP PROCEDURE AgroTrack.AddRetalhista;
GO

IF OBJECT_ID('ApagarTransporte', 'P') IS NOT NULL
    DROP PROCEDURE ApagarTransporte;
GO

IF OBJECT_ID('ApagarRetalhista', 'P') IS NOT NULL
    DROP PROCEDURE ApagarRetalhista;
GO


IF OBJECT_ID('ApagarProduto', 'P') IS NOT NULL
    DROP PROCEDURE ApagarProduto;
GO

IF OBJECT_ID('AddProdutoToQuinta', 'P') IS NOT NULL
    DROP PROCEDURE AddProdutoToQuinta;
GO

IF OBJECT_ID('AddQuinta', 'P') IS NOT NULL
    DROP PROCEDURE AddQuinta;
GO
CREATE PROCEDURE AddQuinta
    @Nome VARCHAR(64),
    @Morada VARCHAR(64),
    @Contacto INT
AS
BEGIN
	DECLARE @NEXTCodigoEmpresa  INT;

    -- Find the maximum ID currently in the AgroTrack_Empresa table
    SELECT @NextCodigoEmpresa = COALESCE(MAX(Id_Empresa), 0) + 1 FROM AgroTrack_Empresa;

    INSERT INTO AgroTrack_Empresa (Id_Empresa, Nome, Morada, Contacto, Tipo_De_Empresa)
    VALUES (@NextCodigoEmpresa, @Nome, @Morada, @Contacto, 'Quinta');
    -- Insert the new Quinta using the next available ID
    INSERT INTO AgroTrack_Quinta (Empresa_Id_Empresa)
    VALUES (@NextCodigoEmpresa); -- Assuming Empresa_Id_Empresa is the same as 

    PRINT 'New Quinta added successfully.';
END
GO
IF OBJECT_ID('AddAnimalToQuinta', 'P') IS NOT NULL
    DROP PROCEDURE AddAnimalToQuinta;
GO
CREATE PROCEDURE AddAnimalToQuinta
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

    -- Insert the new animal into AgroTrack_Quinta_Animal
    INSERT INTO AgroTrack_Quinta_Animal (Empresa_Id_Empresa, Idade, Brinco, Id_Animal)
    VALUES (@Quinta_Id, @Idade, @Brinco, @Id_Animal);
    PRINT 'New animal added to the Quinta successfully.';
END
GO
IF OBJECT_ID('AddPlantaToQuinta', 'P') IS NOT NULL
    DROP PROCEDURE AddPlantaToQuinta;
GO
CREATE PROCEDURE AddPlantaToQuinta
    @Tipo VARCHAR(32),
    @Estacao VARCHAR(32),
    @Lote VARCHAR(32),
    @NomeQuinta VARCHAR(64)
AS
BEGIN
    DECLARE @QuintaId INT;

    -- Obtém o ID da Quinta baseado no Nome
    SELECT @QuintaId = Empresa_Id_Empresa
    FROM AgroTrack_Quinta, AgroTrack_Empresa
    WHERE Nome = @NomeQuinta;

    -- Verifica se a Quinta existe
    IF @QuintaId IS NULL
    BEGIN
        RAISERROR ('A Quinta com o nome fornecido não existe', 16, 1);
        RETURN;
    END

    -- Insere a nova planta na tabela AgroTrack_Quinta_Planta
    INSERT INTO AgroTrack_Quinta_Planta (Empresa_Id_Empresa, Lote, Id_Planta)
    VALUES (@QuintaId, @Lote, (SELECT Id_planta FROM AgroTrack_Planta WHERE Tipo = @Tipo AND Estacao = @Estacao));

    PRINT 'Nova planta adicionada à Quinta com sucesso.';
END
GO
IF OBJECT_ID('AddAgricultor', 'P') IS NOT NULL
    DROP PROCEDURE AddAgricultor;
GO
CREATE PROCEDURE AddAgricultor
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

    -- Verifica se a pessoa já é um agricultor
    IF EXISTS (SELECT 1 FROM AgroTrack_Agricultor WHERE Pessoa_N_CartaoCidadao = @N_CartaoCidadao)
    BEGIN
        RAISERROR ('Esta pessoa já está registada como agricultor', 16, 1);
        RETURN;
    END

    -- Verifica se a Quinta existe
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

    SELECT @PessoaId = N_CartaoCidadao FROM AgroTrack_Pessoa WHERE N_CartaoCidadao = @N_CartaoCidadao;

    -- Insere o contrato na tabela AgroTrack_Contrato
    SELECT @ContratoID = ISNULL(MAX(ID), 0) + 1 FROM AgroTrack_Contrato;
    INSERT INTO AgroTrack_Contrato ([Date_str], [Date_end], Descricao, Salario, ID, Agricultor_Pessoa_N_CartaoCidadao)
    VALUES (@ContractStartDate, @ContractEndDate, @DescricaoContrato, @Salario, @ContratoID, @PessoaId);

    -- Insere o agricultor na quinta
    SELECT @IDAgricultor = ISNULL(MAX(Id_Trabalhador), 0) + 1 FROM AgroTrack_Agricultor WHERE Quinta_Empresa_Id_Empresa = @QuintaId;
    INSERT INTO AgroTrack_Agricultor (Id_Trabalhador, Pessoa_N_CartaoCidadao, Quinta_Empresa_Id_Empresa)
    VALUES (@IDAgricultor, @PessoaId, @QuintaId);

    PRINT 'Novo agricultor adicionado à Quinta com sucesso.';
END
GO
IF OBJECT_ID('AddProduto', 'P') IS NOT NULL
    DROP PROCEDURE AddProduto;
GO

CREATE PROCEDURE AddProduto
    @NomeProduto VARCHAR(64),
    @Id_origem INT,
    @Tipo_de_Produto VARCHAR(64),
    @Preco FLOAT,
    @Taxa_de_iva FLOAT,
    @Unidade_medida VARCHAR(16)
AS
BEGIN
    DECLARE @Codigo INT;
    
    -- Calculate the new Codigo value
    SELECT @Codigo = ISNULL(MAX(Codigo), 0) + 1 FROM AgroTrack_Produto;
    
    -- Insert the new product into the AgroTrack_Produto table
    INSERT INTO AgroTrack_Produto (Codigo, Nome, Id_origem, Tipo_de_Produto, Preco, Taxa_de_iva, Unidade_medida)
    VALUES (@Codigo, @NomeProduto, @Id_origem, @Tipo_de_Produto, @Preco, @Taxa_de_iva, @Unidade_medida);
    PRINT 'Novo produto adicionado com sucesso.';
END
GO




IF OBJECT_ID('AddProdutoToQuinta', 'P') IS NOT NULL
    DROP PROCEDURE AddProdutoToQuinta;
GO
CREATE PROCEDURE AddProdutoToQuinta
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

    -- Insere o produto na tabela AgroTrack_Contem
    IF EXISTS (SELECT 1 FROM AgroTrack_Contem WHERE Produto_Codigo = @ProdutoId AND Quinta_Empresa_Id_Empresa = @QuintaId)
    BEGIN
        -- Atualiza o registo existente com a nova quantidade
        UPDATE AgroTrack_Contem
        SET Quantidade = Quantidade + @Quantidade, 
            Data_de_validade = @DataDeValidade
        WHERE Produto_Codigo = @ProdutoId AND Quinta_Empresa_Id_Empresa = @QuintaId;
    END
    ELSE
    INSERT INTO AgroTrack_Contem (Produto_Codigo, Quinta_Empresa_Id_Empresa, Quantidade, Data_de_validade)
    VALUES (@ProdutoId, @QuintaId, @Quantidade, @DataDeValidade);

    PRINT 'Novo produto adicionado à Quinta com sucesso.';
END;
GO



IF OBJECT_ID('AddEncomenda', 'P') IS NOT NULL
    DROP PROCEDURE AddEncomenda;
GO
CREATE PROCEDURE AddEncomenda
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
        -- Insert the new Encomenda
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
        -- Handle errors
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

IF OBJECT_ID('AddItemToEncomenda', 'P') IS NOT NULL
    DROP PROCEDURE AddItemToEncomenda;
GO
CREATE PROCEDURE AddItemToEncomenda
    @ProdutoCodigo INT,
    @Quantidade INT,
    @EncomendaCodigo INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Insert the new Item
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
        -- Handle errors
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

IF OBJECT_ID('AddColheita', 'P') IS NOT NULL
    DROP PROCEDURE AddColheita;
GO

CREATE PROCEDURE AddColheita
    @Agricultor_Pessoa_N_CartaoCidadao INT,
    @Duracao_colheita FLOAT,
    @Quantidade INT,
    @Produto_codigo INT,
    @DataColheita DATE,
    @Data_de_validade DATE
AS
BEGIN
    -- Start a transaction
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Insert into AgroTrack_Colhe
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

        -- Declare the variable
        DECLARE @Quinta_Empresa_Id_Empresa INT;

        -- Assign value to the variable
        SELECT @Quinta_Empresa_Id_Empresa = Quinta_Empresa_Id_Empresa 
        FROM AgroTrack_Agricultor 
        WHERE Pessoa_N_CartaoCidadao = @Agricultor_Pessoa_N_CartaoCidadao;

        -- Check if the product already exists in the AgroTrack_Contem table for the specified farm
        IF EXISTS (SELECT 1 FROM AgroTrack_Contem WHERE Produto_codigo = @Produto_codigo AND Quinta_Empresa_Id_Empresa = @Quinta_Empresa_Id_Empresa)
        BEGIN
            -- Update the existing record with the new quantity
            UPDATE AgroTrack_Contem
            SET Quantidade = Quantidade + @Quantidade, 
                Data_de_validade = @Data_de_validade
            WHERE Produto_codigo = @Produto_codigo AND Quinta_Empresa_Id_Empresa = @Quinta_Empresa_Id_Empresa;
        END
        ELSE
        BEGIN
            -- Insert a new record into AgroTrack_Contem
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

        -- Commit the transaction
        COMMIT TRANSACTION;

        PRINT 'Colheita and product addition completed successfully.';
    END TRY
    BEGIN CATCH
        -- Rollback the transaction in case of error
        ROLLBACK TRANSACTION;

        -- Get the error details
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Raise the error again to propagate it
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
GO

IF OBJECT_ID('RemoveProdutosForaDeValidadeFromQuinta', 'P') IS NOT NULL
    DROP PROCEDURE RemoveProdutosForaDeValidadeFromQuinta;
GO
CREATE PROCEDURE RemoveProdutosForaDeValidadeFromQuinta @QuintaId INT
AS
BEGIN
    -- Start a transaction
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Delete the products that are past their expiration date
        DELETE FROM AgroTrack_Contem
        WHERE Quinta_Empresa_Id_Empresa = @QuintaId AND [Data_de_validade] < GETDATE();

        -- Commit the transaction
        COMMIT TRANSACTION;

        PRINT 'Produtos fora de validade removidos com sucesso.';
    END TRY
    BEGIN CATCH
        -- Rollback the transaction in case of error
        ROLLBACK TRANSACTION;

        -- Get the error details
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Raise the error again to propagate it
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

IF OBJECT_ID('ApagarQuinta', 'P') IS NOT NULL
    DROP PROCEDURE ApagarQuinta;
GO
CREATE PROCEDURE ApagarQuinta
    @Empresa_Id_Empresa INT
AS
BEGIN
    -- Start a transaction
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Delete the Quinta from the AgroTrack_Quinta table
        DELETE FROM AgroTrack_Quinta
        WHERE Empresa_Id_Empresa = @Empresa_Id_Empresa;

        -- Delete the Empresa from the AgroTrack_Empresa table
        DELETE FROM AgroTrack_Empresa
        WHERE Id_Empresa = @Empresa_Id_Empresa;

        DELETE FROM AgroTrack_Agricultor
        WHERE Quinta_Empresa_Id_Empresa = @Empresa_Id_Empresa;

        DELETE FROM AgroTrack_Quinta_Animal
        WHERE Empresa_Id_Empresa = @Empresa_Id_Empresa;

        DELETE FROM AgroTrack_Quinta_Planta
        WHERE Empresa_Id_Empresa = @Empresa_Id_Empresa;

        DELETE FROM AgroTrack_Contem
        WHERE Quinta_Empresa_Id_Empresa = @Empresa_Id_Empresa;

        -- Commit the transaction
        COMMIT TRANSACTION;

        PRINT 'Quinta deleted successfully.';
    END TRY
    BEGIN CATCH
        -- Rollback the transaction in case of error
        ROLLBACK TRANSACTION;

        -- Get the error details
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Raise the error again to propagate it
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;

IF OBJECT_ID('ApagarProduto', 'P') IS NOT NULL
    DROP PROCEDURE ApagarProduto;
GO
CREATE PROCEDURE ApagarProduto
    @Codigo INT
AS
BEGIN
    -- Start a transaction
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Debug: Print the Codigo to be deleted
        PRINT 'Deleting Produto with Codigo = ' + CAST(@Codigo AS NVARCHAR(10));

        -- Delete from AgroTrack_Colhe table
        DELETE FROM AgroTrack_Colhe
        WHERE Produto_codigo = @Codigo;

        -- Debug: Check if rows are deleted from AgroTrack_Colhe
        DECLARE @ColheCount INT;
        SELECT @ColheCount = COUNT(*) FROM AgroTrack_Colhe WHERE Produto_codigo = @Codigo;
        PRINT 'Remaining rows in AgroTrack_Colhe for Produto_codigo = ' + CAST(@Codigo AS NVARCHAR(10)) + ': ' + CAST(@ColheCount AS NVARCHAR(10));

        -- Delete from AgroTrack_Contem table
        DELETE FROM AgroTrack_Contem
        WHERE Produto_codigo = @Codigo;

        -- Debug: Check if rows are deleted from AgroTrack_Contem
        DECLARE @ContemCount INT;
        SELECT @ContemCount = COUNT(*) FROM AgroTrack_Contem WHERE Produto_codigo = @Codigo;
        PRINT 'Remaining rows in AgroTrack_Contem for Produto_codigo = ' + CAST(@Codigo AS NVARCHAR(10)) + ': ' + CAST(@ContemCount AS NVARCHAR(10));

        -- Delete from AgroTrack_Item table
        DELETE FROM AgroTrack_Item
        WHERE ProdutoCodigo = @Codigo;

        -- Debug: Check if rows are deleted from AgroTrack_Item
        DECLARE @ItemCount INT;
        SELECT @ItemCount = COUNT(*) FROM AgroTrack_Item WHERE ProdutoCodigo = @Codigo;
        PRINT 'Remaining rows in AgroTrack_Item for ProdutoCodigo = ' + CAST(@Codigo AS NVARCHAR(10)) + ': ' + CAST(@ItemCount AS NVARCHAR(10));

        -- Delete from AgroTrack_Produto table
        DELETE FROM AgroTrack_Produto
        WHERE Codigo = @Codigo;

        -- Commit the transaction
        COMMIT TRANSACTION;

        PRINT 'Produto deleted successfully.';
    END TRY
    BEGIN CATCH
        -- Rollback the transaction in case of error
        ROLLBACK TRANSACTION;

        -- Get the error details
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Print the error details for debugging
        PRINT 'Error: ' + @ErrorMessage;
        PRINT 'Severity: ' + CAST(@ErrorSeverity AS NVARCHAR(10));
        PRINT 'State: ' + CAST(@ErrorState AS NVARCHAR(10));

        -- Raise the error again to propagate it
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
GO
IF OBJECT_ID('ApagarAgricultor', 'P') IS NOT NULL
    DROP PROCEDURE ApagarAgricultor;
GO
CREATE PROCEDURE ApagarAgricultor
    @Pessoa_N_CartaoCidadao INT
AS
BEGIN
    -- Start a transaction
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Delete the Agricultor from the AgroTrack_Agricultor table
        DELETE FROM AgroTrack_Agricultor
        WHERE Pessoa_N_CartaoCidadao = @Pessoa_N_CartaoCidadao;

        -- Delete the Agricultor from the AgroTrack_Contrato table
        DELETE FROM AgroTrack_Contrato
        WHERE Agricultor_Pessoa_N_CartaoCidadao = @Pessoa_N_CartaoCidadao;

        -- Commit the transaction
        COMMIT TRANSACTION;

        PRINT 'Agricultor deleted successfully.';
    END TRY
    BEGIN CATCH
        -- Rollback the transaction in case of error
        ROLLBACK TRANSACTION;

        -- Get the error details
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Raise the error again to propagate it
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
GO
IF OBJECT_ID('ApagarColheita', 'P') IS NOT NULL
    DROP PROCEDURE ApagarColheita;
GO
CREATE PROCEDURE ApagarColheita
    @Agricultor_Pessoa_N_CartaoCidadao INT,
    @Produto_codigo INT,
    @DataColheita DATE
AS
BEGIN
    -- Start a transaction
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Delete the Colheita from the AgroTrack_Colhe table
        DELETE FROM AgroTrack_Colhe
        WHERE Agricultor_Pessoa_N_CartaoCidadao = @Agricultor_Pessoa_N_CartaoCidadao
        AND Produto_codigo = @Produto_codigo
        AND DataColheita = @DataColheita;

        -- Commit the transaction
        COMMIT TRANSACTION;

        PRINT 'Colheita deleted successfully.';
    END TRY
    BEGIN CATCH
        -- Rollback the transaction in case of error
        ROLLBACK TRANSACTION;

        -- Get the error details
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Raise the error again to propagate it
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
    -- Start a transaction
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

        -- Commit the transaction
        COMMIT TRANSACTION;

        PRINT 'Cliente added successfully.';
    END TRY
    BEGIN CATCH
        -- Rollback the transaction in case of error
        ROLLBACK TRANSACTION;

        -- Get the error details
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Raise the error again to propagate it
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
    -- Start a transaction
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Delete the Cliente from the AgroTrack_Cliente table
        DELETE FROM AgroTrack_Cliente
        WHERE Pessoa_N_CartaoCidadao = @Pessoa_N_CartaoCidadao;

        -- Delete the Cliente from the AgroTrack_Compra table
        DELETE FROM AgroTrack_Compra
        WHERE Cliente_Pessoa_N_CartaoCidadao = @Pessoa_N_CartaoCidadao;

        -- Delete the Cliente from the AgroTrack_Pessoa table
        DELETE FROM AgroTrack_Pessoa
        WHERE N_CartaoCidadao = @Pessoa_N_CartaoCidadao;

        -- Commit the transaction
        COMMIT TRANSACTION;

        PRINT 'Cliente deleted successfully.';
    END TRY
    BEGIN CATCH
        -- Rollback the transaction in case of error
        ROLLBACK TRANSACTION;

        -- Get the error details
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Raise the error again to propagate it
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
    -- Start a transaction
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Insert into AgroTrack_Compra
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

        -- Commit the transaction
        COMMIT TRANSACTION;

        PRINT 'Compra added successfully.';
    END TRY
    BEGIN CATCH
        -- Rollback the transaction in case of error
        ROLLBACK TRANSACTION;

        -- Get the error details
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Raise the error again to propagate it
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
        ORDER BY Nome ASC; -- Order by Nome (Name) in ascending order
    END
    ELSE IF @Order = 'NomeDecrescente'
    BEGIN
        SELECT Pessoa_N_CartaoCidadao, Nome, Contacto
        FROM AgroTrack.Cliente
        ORDER BY Nome DESC; -- Order by Nome (Name) in descending order
    END
    ELSE IF @Order = 'TelefoneCrescente'
    BEGIN
        SELECT Pessoa_N_CartaoCidadao, Nome, Contacto
        FROM AgroTrack.Cliente
        ORDER BY Contacto ASC; -- Order by Contacto (Telephone) in ascending order
    END
    ELSE IF @Order = 'TelefoneDecrescente'
    BEGIN
        SELECT Pessoa_N_CartaoCidadao, Nome, Contacto
        FROM AgroTrack.Cliente
        ORDER BY Contacto DESC; -- Order by Contacto (Telephone) in descending order
    END
    ELSE
    BEGIN
        -- Invalid order parameter, return an empty result set
        SELECT NULL AS Pessoa_N_CartaoCidadao, NULL AS Nome, NULL AS Contacto
        WHERE 1 = 0; -- Returns no rows
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

    -- Determine the ORDER BY clause based on the @Order parameter
    DECLARE @OrderByClause NVARCHAR(MAX) = 
        CASE 
            WHEN @Order = 'NomeCrescente' THEN 'Nome ASC'
            WHEN @Order = 'NomeDecrescente' THEN 'Nome DESC'
            WHEN @Order = 'IDCrescente' THEN 'Pessoa_N_CartaoCidadao ASC'
            WHEN @Order = 'IDDecrescente' THEN 'Pessoa_N_CartaoCidadao DESC'
            WHEN @Order = 'SalarioCrescente' THEN 'Salario ASC'
            WHEN @Order = 'SalarioDecrescente' THEN 'Salario DESC'
            ELSE 'Nome ASC' -- Default ordering by Name in ascending order
        END;

    -- Construct the dynamic SQL query
    SET @SqlQuery = '
        SELECT Pessoa_N_CartaoCidadao, Nome, Contacto, Empresa_Id_Empresa, Salario, NomeQuinta, Id_Trabalhador
        FROM AgroTrack.AgriculQuintaContrato
        ORDER BY ' + @OrderByClause;

    -- Execute the dynamic SQL query
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
    -- Start a transaction
    BEGIN TRANSACTION;

    BEGIN TRY
    
    -- Calculate the new Id_Empresa value
    DECLARE @Id_Empresa INT;
    SELECT @Id_Empresa = ISNULL(MAX(Id_Empresa), 0) + 1 FROM AgroTrack_Empresa;
        -- Insert into AgroTrack_Empresa
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
        Insert into AgroTrack_Empresa_De_Transportes (Empresa_Id_Empresa,Nome)
        VALUES (@Id_Empresa,@Nome);


        -- Commit the transaction
        COMMIT TRANSACTION;

        PRINT 'Empresa de Transportes added successfully.';
    END TRY
    BEGIN CATCH
        -- Rollback the transaction in case of error
        ROLLBACK TRANSACTION;

        -- Get the error details
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Raise the error again to propagate it
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;




IF OBJECT_ID('ApagarTransporte', 'P') IS NOT NULL
    DROP PROCEDURE ApagarTransporte;
GO
CREATE PROCEDURE ApagarTransporte
    @Empresa_Id_Empresa INT
AS
BEGIN
    -- Start a transaction
    BEGIN TRANSACTION;

    BEGIN TRY

        Delete From AgroTrack_Encomenda
        WHERE Empresa_De_Transportes_Id_Empresa = @Empresa_Id_Empresa;

        DELETE FROM AgroTrack_Empresa_De_Transportes
        WHERE Empresa_Id_Empresa = @Empresa_Id_Empresa;

        -- Delete the Empresa from the AgroTrack_Empresa table
        DELETE FROM AgroTrack_Empresa
        WHERE Id_Empresa = @Empresa_Id_Empresa;

        
        
        -- Commit the transaction
        COMMIT TRANSACTION;

        PRINT 'Empresa deleted successfully.';
    END TRY
    BEGIN CATCH
        -- Rollback the transaction in case of error
        ROLLBACK TRANSACTION;

        -- Get the error details
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Raise the error again to propagate it
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;

IF OBJECT_ID('ApagarRetalhista', 'P') IS NOT NULL
    DROP PROCEDURE ApagarRetalhista;
GO
CREATE PROCEDURE ApagarRetalhista
    @Empresa_Id_Empresa INT
AS
BEGIN
    -- Start a transaction
    BEGIN TRANSACTION;

    BEGIN TRY

        DELETE FROM AgroTrack_Retalhistas
        WHERE Empresa_Id_Empresa = @Empresa_Id_Empresa;

        -- Delete the Empresa from the AgroTrack_Empresa table
        DELETE FROM AgroTrack_Empresa
        WHERE Id_Empresa = @Empresa_Id_Empresa;

        
        
        -- Commit the transaction
        COMMIT TRANSACTION;

        PRINT 'Empresa deleted successfully.';
    END TRY
    BEGIN CATCH
        -- Rollback the transaction in case of error
        ROLLBACK TRANSACTION;

        -- Get the error details
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Raise the error again to propagate it
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
    -- Start a transaction
    BEGIN TRANSACTION;

    BEGIN TRY
    
    -- Calculate the new Id_Empresa value
    DECLARE @Id_Empresa INT;
    SELECT @Id_Empresa = ISNULL(MAX(Id_Empresa), 0) + 1 FROM AgroTrack_Empresa;
        -- Insert into AgroTrack_Empresa
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
        

        -- Commit the transaction
        COMMIT TRANSACTION;

        PRINT 'Retalhista added successfully.';
    END TRY
    BEGIN CATCH
        -- Rollback the transaction in case of error
        ROLLBACK TRANSACTION;

        -- Get the error details
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Raise the error again to propagate it
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
IF OBJECT_ID('RemoveProdutoFromQuinta', 'P') IS NOT NULL
    DROP PROCEDURE RemoveProdutoFromQuinta;
GO
CREATE PROCEDURE RemoveProdutoFromQuinta
    @ProdutoId INT,
    @QuintaId INT
AS
BEGIN
    -- Start a transaction
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Delete the product from the AgroTrack_Contem table
        DELETE FROM AgroTrack_Contem
        WHERE Produto_Codigo = @ProdutoId AND Quinta_Empresa_Id_Empresa = @QuintaId;

        -- Commit the transaction
        COMMIT TRANSACTION;

        PRINT 'Produto removido da Quinta com sucesso.';
    END TRY
    BEGIN CATCH
        -- Rollback the transaction in case of error
        ROLLBACK TRANSACTION;

        -- Get the error details
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Raise the error again to propagate it
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;