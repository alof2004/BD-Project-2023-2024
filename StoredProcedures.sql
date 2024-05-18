USE AgroTrack;
GO;

CREATE PROCEDURE AddQuinta
    @Nome VARCHAR(64),
    @Morada VARCHAR(64)
AS
BEGIN
    DECLARE @NextCodigoQuinta INT;

    -- Find the maximum ID currently in the AgroTrack_Empresa table
    SELECT @NextCodigoEmpresa = COALESCE(MAX(Id_Empresa), 0) + 1 FROM AgroTrack_Empresa;
    SELECT @NextCodigoQuinta = COALESCE(MAX(Codigo_quinta), 0) + 1 FROM AgroTrack_Quinta;

    INSERT INTO AgroTrack_Empresa (Id_Empresa, Nome, Morada)
    VALUES (@NextCodigoEmpresa, @Nome, @Morada);
    -- Insert the new Quinta using the next available ID
    INSERT INTO AgroTrack_Quinta (Codigo_quinta, Empresa_Id_Empresa)
    VALUES (@NextCodigoEmpresa, @NextCodigoQuinta); -- Assuming Empresa_Id_Empresa is the same as Codigo_quinta

    PRINT 'New Quinta added successfully.';
END
GO;


CREATE PROCEDURE AddAnimalToQuinta
    @Tipo_de_Animal VARCHAR(64),
    @Idade INT,
    @Brinco VARCHAR(16),
    @NomeQuinta VARCHAR(64)
AS
BEGIN
    DECLARE @Quinta_Id INT;

    -- Get the ID of the Quinta based on the Nome
    SELECT @Quinta_Id = Codigo_quinta
        FROM AgroTrack_Quinta
    WHERE Nome = @NomeQuinta;

    -- Check if the Quinta exists
    IF @Quinta_Id IS NULL
    BEGIN
        RAISERROR ('Quinta with the provided name does not exist', 16, 1);
        RETURN;
    END

    -- Insert the new animal into AgroTrack_Quinta_Animal
    INSERT INTO AgroTrack_Quinta_Animal (Empresa_Id_Empresa, Idade, Brinco, Id_Animal)
    VALUES (@Quinta_Id, @Idade, @Brinco, (SELECT Id_Animal FROM AgroTrack_Animal WHERE Tipo_de_Animal = @Tipo_de_Animal));

    PRINT 'New animal added to the Quinta successfully.';
END
GO;

CREATE PROCEDURE AddPlantaToQuinta
    @Tipo VARCHAR(32),
    @Estacao VARCHAR(32),
    @Lote VARCHAR(32),
    @NomeQuinta VARCHAR(64)
AS
BEGIN
    DECLARE @QuintaId INT;

    -- Obtém o ID da Quinta baseado no Nome
    SELECT @QuintaId = Codigo_quinta
    FROM AgroTrack_Quinta
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
GO;

CREATE PROCEDURE AddAgricultor
    @Nome VARCHAR(64),
    @N_CartaoCidadao INT,
    @Salario FLOAT,
    @DescricaoContrato VARCHAR(64),
    @NomeQuinta VARCHAR(64)
    @Contacto INT
    @ContractStartDate DATE
    @ContractEndDate DATE
AS
BEGIN
    DECLARE @QuintaId INT;
    DECLARE @PessoaId INT;
    DECLARE @ContratoId INT;

    -- Verifica se a pessoa já é um agricultor
    IF EXISTS (SELECT 1 FROM AgroTrack_Agricultor WHERE Pessoa_N_CartaoCidadao = @N_CartaoCidadao)
    BEGIN
        RAISERROR ('Esta pessoa já está registada como agricultor', 16, 1);
        RETURN;
    END

    -- Obtém o ID da Quinta baseado no Nome
    SELECT @QuintaId = Codigo_quinta
    FROM AgroTrack_Quinta
    WHERE Nome = @NomeQuinta;

    -- Verifica se a Quinta existe
    IF @QuintaId IS NULL
    BEGIN
        RAISERROR ('A Quinta com o nome fornecido não existe', 16, 1);
        RETURN;
    END

    -- Insere a pessoa na tabela AgroTrack_Pessoa, se ainda não existir
    IF NOT EXISTS (SELECT 1 FROM AgroTrack_Pessoa WHERE N_CartaoCidadao = @N_CartaoCidadao)
    BEGIN
        INSERT INTO AgroTrack_Pessoa (Nome, N_CartaoCidadao)
        VALUES (@Nome, @N_CartaoCidadao, @Contacto);
    END

    -- Obtém o ID da pessoa
    SELECT @PessoaId = N_CartaoCidadao FROM AgroTrack_Pessoa WHERE N_CartaoCidadao = @N_CartaoCidadao;

    -- Insere o contrato na tabela AgroTrack_Contrato
    SELECT @ContratoID = MAX(ID) + 1 FROM AgroTrack_Contrato;
    INSERT INTO AgroTrack_Contrato ([Date_str], [Date_end], Descricao, Salario, ID, Agricultor_Pessoa_N_CartaoCidadao)
    VALUES (@ContractStartDate, @ContractEndDate, @DescricaoContrato, @ContratoID, @Salario, @PessoaId);

    -- Insere o agricultor na quinta
    @IDAgricultor = SELECT MAX(Id_Trabalhador) + 1 FROM AgroTrack_Agricultor WHERE Quinta_Empresa_Id_Empresa = @QuintaId;
    INSERT INTO AgroTrack_Agricultor (Id_Trabalhador,Pessoa_N_CartaoCidadao, Quinta_Empresa_Id_Empresa)
    VALUES (@IDAgricultor, @PessoaId, @QuintaId);

    PRINT 'Novo agricultor adicionado à Quinta com sucesso.';
END
GO;

CREATE PROCEDURE AddProduto
    @Nome VARCHAR(64),
    @Id_origem INT,
    @Tipo_de_Produto INT,
    @Preco FLOAT,
    @Taxa_de_iva FLOAT,
    @Unidade_medida VARCHAR(16)
AS
BEGIN
    -- Verifica se o produto já existe
    @Codigo = MAX(Codigo) + 1 FROM AgroTrack_Produto;
    -- Insere o novo produto
    INSERT INTO AgroTrack_Produto (Nome, Id_origem, Tipo_de_Produto, Codigo, Preco, Taxa_de_iva, Unidade_medida)
    VALUES (@Nome, @Id_origem, @Tipo_de_Produto, @Codigo, @Preco, @Taxa_de_iva, @Unidade_medida);

    PRINT 'Novo produto adicionado com sucesso.';
END
GO;
    
CREATE PROCEDURE AddProdutoToQuinta
    @NomeProduto VARCHAR(64),
    @NomeQuinta VARCHAR(64),
    @Quantidade INT
AS
BEGIN
    DECLARE @ProdutoId INT;
    DECLARE @QuintaId INT;

    -- Obtém o ID do Produto baseado no Nome
    SELECT @ProdutoId = Codigo
    FROM AgroTrack_Produto
    WHERE Nome = @NomeProduto;

    -- Verifica se o Produto existe
    IF @ProdutoId IS NULL
    BEGIN
        RAISERROR ('O Produto com o nome fornecido não existe', 16, 1);
        RETURN;
    END

    -- Obtém o ID da Quinta baseado no Nome
    SELECT @QuintaId = Codigo_quinta
    FROM AgroTrack_Quinta
    WHERE Nome = @NomeQuinta;

    -- Verifica se a Quinta existe
    IF @QuintaId IS NULL
    BEGIN
        RAISERROR ('A Quinta com o nome fornecido não existe', 16, 1);
        RETURN;
    END

    -- Insere o produto na tabela AgroTrack_Quinta_Produto
    INSERT INTO AgroTrack_Quinta_Produto (Produto_Codigo, Quinta_Codigo_quinta, Quantidade)
    VALUES (@ProdutoId, @QuintaId, @Quantidade);

    PRINT 'Novo produto adicionado à Quinta com sucesso.';
END
GO;

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
GO;

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

        @Quinta_Empresa_Id_Empresa = SELECT Quinta_Empresa_Id_Empresa FROM AgroTrack_Agricultor WHERE Pessoa_N_CartaoCidadao = @Agricultor_Pessoa_N_CartaoCidadao;
        -- Check if the product already exists in the AgroTrack_Contem table for the specified farm
        IF EXISTS (SELECT 1 FROM AgroTrack_Contem WHERE Produto_codigo = @Produto_codigo AND Quinta_Empresa_Id_Empresa = @Quinta_Empresa_Id_Empresa)
        BEGIN
            -- Update the existing record with the new quantity
            UPDATE AgroTrack_Contem
            SET Quantidade = Quantidade + @Quantidade, 
                [Data_de_validade] = @Data_de_validade
            WHERE Produto_codigo = @Produto_codigo AND Quinta_Empresa_Id_Empresa = @Quinta_Empresa_Id_Empresa;
        END
        ELSE
        BEGIN
            -- Insert a new record into AgroTrack_Contem
            INSERT INTO AgroTrack_Contem (
                Produto_codigo,
                Quinta_Empresa_Id_Empresa,
                [Data_de_validade],
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
