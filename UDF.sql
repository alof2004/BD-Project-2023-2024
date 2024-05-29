-- Create the UDF to get the total number of products in a farm considering their quantities
DROP FUNCTION IF EXISTS AgroTrack.GetTotalNumberOfProductsInFarm;
GO
CREATE FUNCTION AgroTrack.GetTotalNumberOfProductsInFarm(@FarmId INT)
RETURNS INT
AS
BEGIN
    DECLARE @TotalProductCount INT;

    SELECT @TotalProductCount = SUM(Quantidade) -- vamos somar a quantidade de produtos que cada quinta contem
    FROM AgroTrack_Contem
    WHERE Quinta_Empresa_Id_Empresa = @FarmId;
    RETURN @TotalProductCount;
END;
GO

DROP FUNCTION IF EXISTS AgroTrack.GetNumberOfColheitasOfFarmer;
GO
CREATE FUNCTION AgroTrack.GetNumberOfColheitasOfFarmer(@MinColheitas INT)
RETURNS TABLE
AS
RETURN
(
    SELECT a.Pessoa_N_CartaoCidadao, COUNT(*) AS NumberOfColheitas
    FROM AgroTrack_Agricultor a
    JOIN AgroTrack_Colhe c ON a.Pessoa_N_CartaoCidadao = c.Agricultor_Pessoa_N_CartaoCidadao
    GROUP BY a.Pessoa_N_CartaoCidadao
    HAVING COUNT(*) >= @MinColheitas
);
GO

DROP FUNCTION IF EXISTS AgroTrack.FilterFarmByProduct;
GO
CREATE FUNCTION AgroTrack.FilterFarmByProduct(@ProductId INT)
RETURNS TABLE
AS
RETURN
(
    SELECT Quinta_Empresa_Id_Empresa
    FROM AgroTrack_Contem
    WHERE Produto_Codigo = @ProductId
);
GO

DROP FUNCTION IF EXISTS AgroTrack.FilterFarmByAnimal;
GO
CREATE FUNCTION AgroTrack.FilterFarmByAnimal(@AnimalType NVARCHAR(64))
RETURNS TABLE
AS
RETURN
(
    SELECT DISTINCT q.Empresa_Id_Empresa
    FROM AgroTrack_Quinta q
    JOIN AgroTrack_Quinta_Animal qa ON q.Empresa_Id_Empresa = qa.Empresa_Id_Empresa
    JOIN AgroTrack_Animal a ON qa.Id_Animal = a.Id_Animal
    WHERE a.Tipo_de_Animal = @AnimalType
);
GO

DROP FUNCTION IF EXISTS AgroTrack.FilterFarmByPlant;
GO
CREATE FUNCTION AgroTrack.FilterFarmByPlant(@PlantType NVARCHAR(64))
RETURNS TABLE
AS
RETURN
(
    SELECT q.Empresa_Id_Empresa
    FROM AgroTrack_Quinta q
    JOIN AgroTrack_Quinta_Planta qp ON q.Empresa_Id_Empresa = qp.Empresa_Id_Empresa
    JOIN AgroTrack_Planta p ON qp.Id_Planta = p.Id_Planta
    WHERE p.Tipo = @PlantType
);

GO
DROP FUNCTION IF EXISTS AgroTrack.FilterFarmerByFarm;
GO
CREATE FUNCTION AgroTrack.FilterFarmerByFarm(@FarmId INT)
RETURNS TABLE
AS
RETURN
(
    SELECT a.Pessoa_N_CartaoCidadao, q.Empresa_Id_Empresa
    FROM AgroTrack_Agricultor a
    JOIN AgroTrack_Quinta q ON a.Quinta_Empresa_Id_Empresa = q.Empresa_Id_Empresa
    WHERE q.Empresa_Id_Empresa = @FarmId
);
GO
DROP FUNCTION IF EXISTS AgroTrack.FilterEncomendaByFarm;
GO
CREATE FUNCTION AgroTrack.FilterEncomendaByFarm(@FarmId INT)
RETURNS TABLE
AS
RETURN
(
    SELECT e.Codigo, e.Entrega, e.Quinta_Empresa_Id, e.Retalhista_Empresa_Id_Empresa
    FROM AgroTrack_Encomenda e
    JOIN AgroTrack_Quinta q ON e.Quinta_Empresa_Id = q.Empresa_Id_Empresa
    WHERE q.Empresa_Id_Empresa = @FarmId
);
GO
DROP FUNCTION IF EXISTS AgroTrack.FilterFarmByMinimumNumberOfFarmers;
GO
CREATE FUNCTION AgroTrack.FilterFarmByMinimumNumberOfFarmers(@NumberOfFarmers INT)
RETURNS TABLE
AS
RETURN
(
    SELECT q.Empresa_Id_Empresa
    FROM AgroTrack_Quinta q
    JOIN AgroTrack_Agricultor a ON q.Empresa_Id_Empresa = a.Quinta_Empresa_Id_Empresa
    GROUP BY q.Empresa_Id_Empresa
    HAVING COUNT(a.Pessoa_N_CartaoCidadao) >= @NumberOfFarmers
);
GO
DROP FUNCTION IF EXISTS AgroTrack.FilterFarmerByProduct;
GO
CREATE FUNCTION AgroTrack.FilterFarmerByProduct(@ProductId INT)
RETURNS TABLE
AS
RETURN
(
    SELECT a.Pessoa_N_CartaoCidadao
    FROM AgroTrack_Agricultor a
    JOIN AgroTrack_Colhe c ON a.Pessoa_N_CartaoCidadao = c.Agricultor_Pessoa_N_CartaoCidadao
    WHERE c.Produto_codigo = @ProductId
);
GO
DROP FUNCTION IF EXISTS AgroTrack.FiltrarClientes;
GO
CREATE FUNCTION AgroTrack.FiltrarClientes
(
    @ProdutoCodigo INT = NULL,
    @QuintaId INT = NULL,
    @NumeroCompras INT = NULL
)
RETURNS @Result TABLE 
(
    N_CartaoCidadao INT,
    Nome VARCHAR(255),
    Contacto INT
)
AS
BEGIN
    -- Inserir os resultados na tabela retornada
    INSERT INTO @Result
    SELECT DISTINCT p.N_CartaoCidadao, p.Nome, p.Contacto
    FROM AgroTrack_Pessoa p
    INNER JOIN AgroTrack_Cliente c ON p.N_CartaoCidadao = c.Pessoa_N_CartaoCidadao
    INNER JOIN AgroTrack_Compra ac ON c.Pessoa_N_CartaoCidadao = ac.Cliente_Pessoa_N_CartaoCidadao
    WHERE (@ProdutoCodigo IS NULL OR ac.Produto_codigo = @ProdutoCodigo)
    AND (@QuintaId IS NULL OR ac.ID_Quinta = @QuintaId)
    AND c.Pessoa_N_CartaoCidadao IN (
        SELECT Cliente_Pessoa_N_CartaoCidadao
        FROM AgroTrack_Compra
        GROUP BY Cliente_Pessoa_N_CartaoCidadao
        HAVING COUNT(Cliente_Pessoa_N_CartaoCidadao) >= ISNULL(@NumeroCompras, 0)
    );

    RETURN;
END;
GO
DROP FUNCTION IF EXISTS AgroTrack.GetTotalNumberOfProductInAllFarms;
GO
CREATE FUNCTION AgroTrack.GetTotalNumberOfProductInAllFarms(@ProductId INT)
RETURNS INT
AS
BEGIN
    DECLARE @TotalProductCount INT;

    SELECT @TotalProductCount = SUM(Quantidade)
    FROM AgroTrack_Contem
    WHERE Produto_Codigo = @ProductId;
    IF @TotalProductCount IS NULL
        SET @TotalProductCount = 0;
    RETURN @TotalProductCount;
END;
GO
DROP FUNCTION IF EXISTS AgroTrack.CalcularQuantidadeProdutoVendido;
GO
CREATE FUNCTION AgroTrack.CalcularQuantidadeProdutoVendido(@ProductId INT)
RETURNS INT
AS
BEGIN
    DECLARE @TotalProductCountCompra INT;
    DECLARE @TotalProductCountEncomenda INT;

    SELECT @TotalProductCountCompra = SUM(Quantidade)
    FROM AgroTrack_Compra
    WHERE Produto_codigo = @ProductId;
    
    SELECT @TotalProductCountEncomenda = SUM(Quantidade)
    FROM AgroTrack_Item
    WHERE ProdutoCodigo = @ProductId;

    IF @TotalProductCountCompra IS NULL
        SET @TotalProductCountCompra = 0;
    IF @TotalProductCountEncomenda IS NULL
        SET @TotalProductCountEncomenda = 0;
    RETURN @TotalProductCountCompra + @TotalProductCountEncomenda;
END;