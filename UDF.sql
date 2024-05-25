-- Create the UDF to get the total number of products in a farm considering their quantities
DROP FUNCTION IF EXISTS AgroTrack.GetTotalNumberOfProductsInFarm;
GO
CREATE FUNCTION AgroTrack.GetTotalNumberOfProductsInFarm(@FarmId INT)
RETURNS INT
AS
BEGIN
    DECLARE @TotalProductCount INT;

    SELECT @TotalProductCount = SUM(Quantidade) -- vamos somar a quantidade de produtos que a quinta tem
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

