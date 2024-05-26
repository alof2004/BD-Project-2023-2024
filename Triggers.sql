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
CREATE TRIGGER AgroTrack_CalculatePriceEncomenda
ON AgroTrack_Encomenda
AFTER INSERT
AS
BEGIN
    UPDATE e
    SET PrecoTotal = (SELECT SUM(AgroTrack_Produto.Preco * AgroTrack_Item.Quantidade * AgroTrack_Produto.Taxa_de_iva)
                      FROM AgroTrack_Item
                      JOIN AgroTrack_Produto ON AgroTrack_Item.ProdutoCodigo = AgroTrack_Produto.Codigo
                      WHERE AgroTrack_Item.Encomenda_Codigo = e.Codigo)
END;
GO
IF OBJECT_ID('AgroTrack_AtualizarContemAposEncomenda', 'TR') IS NOT NULL
    DROP TRIGGER AgroTrack_AtualizarContemAposEncomenda;
GO
CREATE TRIGGER AgroTrack_AtualizarContemAposEncomenda
ON AgroTrack_Item
AFTER INSERT
AS
BEGIN
    -- Update the quantity in the AgroTrack_Contem table based on the inserted Encomenda
    UPDATE C
    SET C.Quantidade = C.Quantidade - I.Quantidade
    FROM AgroTrack_Contem C
    JOIN inserted I ON C.Produto_codigo = I.ProdutoCodigo
    JOIN AgroTrack_Encomenda E ON I.Encomenda_Codigo = E.Codigo
END;
GO

