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
