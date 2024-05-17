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
GO

CREATE PROCEDURE AddAgricultorToQuinta
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
GO
