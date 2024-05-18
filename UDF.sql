-- Create the UDF to get the total number of products in a farm considering their quantities
DROP FUNCTION IF EXISTS AgroTrack.GetTotalNumberOfProductsInFarm;
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

DROP FUNCTION IF EXISTS AgroTrack.FilterFarmByProduct;
CREATE FUNCTION AgroTrack.FilterFarmByProduct(@ProductId INT)
RETURNS TABLE
AS
RETURN
(
    SELECT Quinta_Empresa_Id_Empresa
    FROM AgroTrack_Contem
    WHERE Produto_Id_Produto = @ProductId
);
GO


IF OBJECT_ID('AgroTrack.FilterFarmByAnimal', 'IF') IS NOT NULL DROP FUNCTION AgroTrack.FilterFarmByAnimal; GO
CREATE FUNCTION AgroTrack.FilterFarmByAnimal(@AnimalType NVARCHAR(64))
RETURNS TABLE
AS
RETURN
(
    SELECT q.Codigo_quinta, q.Empresa_Id_Empresa
    FROM AgroTrack_Quinta q
    JOIN AgroTrack_Quinta_Animal qa ON q.Empresa_Id_Empresa = qa.Empresa_Id_Empresa
    JOIN AgroTrack_Animal a ON qa.Id_Animal = a.Id_Animal
    WHERE a.Tipo_de_Animal = @AnimalType
);
GO

IF OBJECT_ID('AgroTrack.FilterFarmByPlant', 'IF') IS NOT NULL DROP FUNCTION AgroTrack.FilterFarmByPlant; GO
CREATE FUNCTION AgroTrack.FilterFarmByPlant(@PlantType NVARCHAR(64))
RETURNS TABLE
AS
RETURN
(
    SELECT q.Codigo_quinta, q.Empresa_Id_Empresa
    FROM AgroTrack_Quinta q
    JOIN AgroTrack_Quinta_Planta qp ON q.Empresa_Id_Empresa = qp.Empresa_Id_Empresa
    JOIN AgroTrack_Planta p ON qp.Id_Planta = p.Id_Planta
    WHERE p.Tipo_de_Planta = @PlantType
);
GO;

IF OBJECT_ID('AgroTrack.FilterFarmerByFarm', 'IF') IS NOT NULL DROP FUNCTION AgroTrack.FilterFarmerByFarm; GO
CREATE FUNCTION AgroTrack.FilterFarmerByFarm(@FarmId INT)
RETURNS TABLE
AS
RETURN
(
    SELECT a.N_CartaoCidadao, a.Nome, a.Contacto
    FROM AgroTrack_Agricultor a
    JOIN AgroTrack_Quinta q ON a.N_CartaoCidadao = q.Codigo_quinta
    WHERE q.Empresa_Id_Empresa = @FarmId
);

IF OBJECT_ID('AgroTrack.FilterEncomendaByFarm', 'IF') IS NOT NULL DROP FUNCTION AgroTrack.FilterEncomendaByFarm; GO
CREATE FUNCTION AgroTrack.FilterEncomendaByFarm(@FarmId INT)
RETURNS TABLE
AS
RETURN
(
    SELECT e.Id_Encomenda, e.Data, e.Quinta_Empresa_Id_Empresa, e.Cliente_N_CartaoCidadao
    FROM AgroTrack_Encomenda e
    JOIN AgroTrack_Quinta q ON e.Quinta_Empresa_Id_Empresa = q.Empresa_Id_Empresa
    WHERE q.Empresa_Id_Empresa = @FarmId
);
