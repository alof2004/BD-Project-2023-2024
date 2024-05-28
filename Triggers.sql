IF OBJECT_ID('AgroTrack_PreventNegativeStock', 'TR') IS NOT NULL
    DROP TRIGGER AgroTrack_PreventNegativeStock;
GO
CREATE TRIGGER AgroTrack_PreventNegativeStock
ON AgroTrack_Contem
AFTER UPDATE, INSERT
AS
BEGIN
    IF EXISTS (SELECT * FROM inserted WHERE Quantidade < 0)
    BEGIN
        RAISERROR ('Stock quantity cannot be negative', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO
IF OBJECT_ID('AgroTrack_CalculateTotalPrice', 'TR') IS NOT NULL
    DROP TRIGGER AgroTrack_CalculateTotalPrice;
GO
CREATE TRIGGER AgroTrack_CalculateTotalPrice
ON AgroTrack_Compra
AFTER INSERT
AS
BEGIN
    UPDATE ac
    SET Preco = ac.Preco * ac.Quantidade * (1 + p.Taxa_de_iva)
    FROM AgroTrack_Compra ac
    JOIN inserted i ON ac.Produto_codigo = i.Produto_codigo
                   AND ac.Cliente_Pessoa_N_CartaoCidadao = i.Cliente_Pessoa_N_CartaoCidadao
    JOIN AgroTrack_Produto p ON p.Codigo = i.Produto_codigo;
END;
GO

IF OBJECT_ID('AgroTrack_CalculatePriceEncomenda', 'TR') IS NOT NULL
    DROP TRIGGER AgroTrack_CalculatePriceEncomenda;
GO
-- Drop the trigger if it already exists
IF OBJECT_ID('AgroTrack.CalculatePriceOfEncomendaWithItems', 'TR') IS NOT NULL
    DROP TRIGGER AgroTrack.CalculatePriceOfEncomendaWithItems;
GO

-- Drop the trigger if it already exists
IF OBJECT_ID('CalculatePriceOfEncomendaWithItems', 'TR') IS NOT NULL
    DROP TRIGGER CalculatePriceOfEncomendaWithItems;
GO

-- Create the trigger on the correct schema
CREATE TRIGGER CalculatePriceOfEncomendaWithItems
ON AgroTrack_Item
AFTER INSERT
AS
BEGIN
    -- Update the price of the Encomenda based on the inserted items
    UPDATE E
    SET PrecoTotal = PrecoTotal + (
        SELECT SUM(P.Preco * I.Quantidade * (1 + P.Taxa_de_iva))
        FROM AgroTrack_Item I
        JOIN AgroTrack_Produto P ON I.ProdutoCodigo = P.Codigo
        WHERE I.Encomenda_Codigo = E.Codigo
    )
    FROM AgroTrack_Encomenda E
    JOIN inserted I ON E.Codigo = I.Encomenda_Codigo;
END;
GO
