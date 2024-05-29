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
    SET Preco = (p.Preco * ac.Quantidade * (1 + p.Taxa_de_iva))
    FROM AgroTrack_Compra ac
    JOIN inserted i ON ac.Produto_codigo = i.Produto_codigo
                   AND ac.Cliente_Pessoa_N_CartaoCidadao = i.Cliente_Pessoa_N_CartaoCidadao
    JOIN AgroTrack_Produto p ON p.Codigo = i.Produto_codigo;
END;

IF OBJECT_ID('CalculatePriceOfEncomendaWithItems', 'TR') IS NOT NULL
    DROP TRIGGER CalculatePriceOfEncomendaWithItems;
GO
CREATE TRIGGER AgroTrack_CalculatePriceOfEncomendaWithItems
ON AgroTrack_Item
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @EncomendaCodigo INT;
    DECLARE @TotalPrice DECIMAL(10, 2);

    SELECT @EncomendaCodigo = Encomenda_Codigo
    FROM inserted;

    SELECT @TotalPrice = SUM(Quantidade * Preco * (1 + Taxa_de_iva))
    FROM inserted i
    JOIN AgroTrack_Produto p ON i.ProdutoCodigo = p.Codigo
    WHERE Encomenda_Codigo = @EncomendaCodigo;

    UPDATE AgroTrack_Encomenda
    SET PrecoTotal = ISNULL(PrecoTotal, 0) + @TotalPrice
    WHERE Codigo = @EncomendaCodigo;
END;
GO
IF OBJECT_ID('check_product_quantity', 'TR') IS NOT NULL
    DROP TRIGGER check_product_quantity;
GO
CREATE TRIGGER AgroTrack_CheckProductQuantityEncomenda
ON AgroTrack_Item
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ProductCodigo INT;
    DECLARE @EncomendaCodigo INT;
    DECLARE @Quantidade INT;

    SELECT 
        @ProductCodigo = i.ProdutoCodigo, 
        @EncomendaCodigo = i.Encomenda_Codigo,
        @Quantidade = i.Quantidade
    FROM inserted i;

    DECLARE @AvailableQuantity INT;
    SELECT @AvailableQuantity = Quantidade 
    FROM AgroTrack_Contem 
    WHERE Produto_codigo = @ProductCodigo;

    IF @AvailableQuantity < @Quantidade
    BEGIN
        RAISERROR ('Not enough quantity available in the farm for product %d. Available: %d, Requested: %d', 16, 1, @ProductCodigo, @AvailableQuantity, @Quantidade);
        DELETE FROM AgroTrack_Encomenda WHERE Codigo = @EncomendaCodigo;
        ROLLBACK TRANSACTION;
        RETURN;
    END

    UPDATE AgroTrack_Contem
    SET Quantidade = Quantidade - @Quantidade
    WHERE Produto_codigo = @ProductCodigo;

    INSERT INTO AgroTrack_Item (ProdutoCodigo, Quantidade, Encomenda_Codigo)
    SELECT ProdutoCodigo, Quantidade, Encomenda_Codigo
    FROM inserted;
END;
GO
IF OBJECT_ID('AgroTrack_CheckProductQuantityCompra', 'TR') IS NOT NULL
    DROP TRIGGER AgroTrack_CheckProductQuantityCompra;
GO
CREATE TRIGGER AgroTrack_CheckProductQuantityCompra
ON AgroTrack_Compra
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ProductCodigo INT;
    DECLARE @ClienteN_CartaoCidadao INT;
    DECLARE @Quantidade INT;

    SELECT 
        @ProductCodigo = i.Produto_codigo, 
        @ClienteN_CartaoCidadao = i.Cliente_Pessoa_N_CartaoCidadao,
        @Quantidade = i.Quantidade
    FROM inserted i;

    DECLARE @AvailableQuantity INT;
    SELECT @AvailableQuantity = Quantidade 
    FROM AgroTrack_Contem 
    WHERE Produto_codigo = @ProductCodigo;

    IF @AvailableQuantity < @Quantidade
    BEGIN
        RAISERROR ('Not enough quantity available in the farm for product %d. Available: %d, Requested: %d', 16, 1, @ProductCodigo, @AvailableQuantity, @Quantidade);
        DELETE FROM AgroTrack_Compra WHERE Produto_codigo = @ProductCodigo AND Cliente_Pessoa_N_CartaoCidadao = @ClienteN_CartaoCidadao;
        ROLLBACK TRANSACTION;
        RETURN;
    END

    UPDATE AgroTrack_Contem
    SET Quantidade = Quantidade - @Quantidade
    WHERE Produto_codigo = @ProductCodigo;

    INSERT INTO AgroTrack_Compra (Produto_codigo, Cliente_Pessoa_N_CartaoCidadao, Quantidade)
    SELECT Produto_codigo, Cliente_Pessoa_N_CartaoCidadao, Quantidade
    FROM inserted;
END;
IF OBJECT_ID('AgroTrack_AddDataEntrega', 'TR') IS NOT NULL
    DROP TRIGGER AgroTrack_AddDataEntrega;
GO
CREATE TRIGGER AgroTrack_AddDataEntrega
ON AgroTrack_Encomenda
AFTER INSERT
AS
BEGIN
    UPDATE e
    SET Entrega = DATEADD(DAY, e.prazo_entrega, GETDATE())
    FROM AgroTrack_Encomenda e
    JOIN inserted i ON e.Codigo = i.Codigo;
END;
GO
IF OBJECT_ID('AgroTrack_PreventIncorrectEncomendaData', 'TR') IS NOT NULL
    DROP TRIGGER AgroTrack_PreventIncorrectEncomendaData;
GO
CREATE TRIGGER AgroTrack_PreventIncorrectEncomendaData
ON AgroTrack_Encomenda
AFTER UPDATE
AS
BEGIN
    IF UPDATE(Entrega)
    BEGIN
        IF EXISTS (SELECT 1 FROM inserted WHERE Entrega < GETDATE())
        BEGIN
            RAISERROR ('Delivery date cannot be in the past', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        IF EXISTS (
            SELECT 1 
            FROM inserted i
            JOIN deleted d ON i.Codigo = d.Codigo
            WHERE d.Entrega IS NOT NULL AND i.Entrega > d.Entrega
        )
        BEGIN
            RAISERROR ('New delivery date cannot be after the existing delivery date', 16, 1);
            ROLLBACK TRANSACTION;
        END
    END
END;
GO
