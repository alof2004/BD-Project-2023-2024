IF AgroTrack_PreventNegativeStock IS NOT NULL DROP TRIGGER trg_PreventNegativeStock;
CREATE TRIGGER AgroTrack_PreventNegativeStock
ON AgroTrack_Contem
BEFORE UPDATE, INSERT
AS
BEGIN
    IF EXISTS (SELECT * FROM inserted WHERE Quantidade < 0)
    BEGIN
        RAISERROR ('Stock quantity cannot be negative', 16, 1);
        ROLLBACK;
    END
END;

CREATE TRIGGER trg_CalculateTotalPrice
ON AgroTrack_Compra
AFTER INSERT
AS
BEGIN
    UPDATE AgroTrack_Compra
    SET Preco = Preco * (1 + (SELECT Taxa_de_iva FROM AgroTrack_Produto WHERE AgroTrack_Produto.Codigo = inserted.Produto_codigo))
    FROM inserted
    WHERE AgroTrack_Compra.Produto_codigo = inserted.Produto_codigo
      AND AgroTrack_Compra.Cliente_Pessoa_N_CartaoCidadao = inserted.Cliente_Pessoa_N_CartaoCidadao;
END;

CREATE TRIGGER trgCheckStockBeforeInsert
ON AgroTrack_Item
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @ProdutoCodigo int, @Quantidade int, @Encomenda_Codigo int;

    -- Get values from the inserted row
    SELECT @ProdutoCodigo = i.ProdutoCodigo, 
           @Quantidade = i.Quantidade, 
           @Encomenda_Codigo = i.Encomenda_Codigo
    FROM inserted i;

    -- Check stock availability
    IF EXISTS (SELECT 1 FROM AgroTrack_Contem 
               WHERE Produto_codigo = @ProdutoCodigo 
               AND Quantidade >= @Quantidade)
    BEGIN
        -- There is enough stock, proceed with the insert
        INSERT INTO AgroTrack_Item (ProdutoCodigo, Quantidade, Encomenda_Codigo)
        SELECT ProdutoCodigo, Quantidade, Encomenda_Codigo
        FROM inserted;

        -- Update stock to reflect the new order
        UPDATE AgroTrack_Contem
        SET Quantidade = Quantidade - @Quantidade
        WHERE Produto_codigo = @ProdutoCodigo;
    END
    ELSE
    BEGIN
        -- Not enough stock, raise an error
        RAISERROR ('Not enough stock available for the product', 16, 1);
    END
END;
GO

