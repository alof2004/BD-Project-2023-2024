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
create TRIGGER CalculatePriceOfEncomendaWithItems
ON AgroTrack_Item
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @EncomendaCodigo INT;
    DECLARE @TotalPrice DECIMAL(10, 2);

    -- Get the Encomenda_Codigo from the inserted rows
    SELECT @EncomendaCodigo = Encomenda_Codigo
    FROM inserted;

    -- Calculate the total price for the Encomenda
    SELECT @TotalPrice = SUM(Quantidade * Preco * (1 + Taxa_de_iva))
    FROM inserted i
    JOIN AgroTrack_Produto p ON i.ProdutoCodigo = p.Codigo
    WHERE Encomenda_Codigo = @EncomendaCodigo;

    -- Update the AgroTrack_Encomenda table with the total price
    UPDATE AgroTrack_Encomenda
    SET PrecoTotal = ISNULL(PrecoTotal, 0) + @TotalPrice
    WHERE Codigo = @EncomendaCodigo;
END;
GO
IF OBJECT_ID('check_product_quantity', 'TR') IS NOT NULL
    DROP TRIGGER check_product_quantity;
GO
CREATE TRIGGER check_product_quantity
ON AgroTrack_Item
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;

    -- Declare variables
    DECLARE @ProductCodigo INT;
    DECLARE @EncomendaCodigo INT;
    DECLARE @Quantidade INT;

    -- Get data from inserted table
    SELECT 
        @ProductCodigo = i.ProdutoCodigo, 
        @EncomendaCodigo = i.Encomenda_Codigo,
        @Quantidade = i.Quantidade
    FROM inserted i;

    -- Check available quantity
    DECLARE @AvailableQuantity INT;
    SELECT @AvailableQuantity = Quantidade 
    FROM AgroTrack_Contem 
    WHERE Produto_codigo = @ProductCodigo;

    -- Check if available quantity is sufficient
    IF @AvailableQuantity < @Quantidade
    BEGIN
        RAISERROR ('Not enough quantity available in the farm for product %d. Available: %d, Requested: %d', 16, 1, @ProductCodigo, @AvailableQuantity, @Quantidade);
        DELETE FROM AgroTrack_Encomenda WHERE Codigo = @EncomendaCodigo;
        ROLLBACK TRANSACTION;
        RETURN;
    END

    -- Decrease the quantity in AgroTrack_Contem table
    UPDATE AgroTrack_Contem
    SET Quantidade = Quantidade - @Quantidade
    WHERE Produto_codigo = @ProductCodigo;

    -- Insert the item into AgroTrack_Item table
    INSERT INTO AgroTrack_Item (ProdutoCodigo, Quantidade, Encomenda_Codigo)
    SELECT ProdutoCodigo, Quantidade, Encomenda_Codigo
    FROM inserted;
END;
GO
IF OBJECT_ID('AgroTrack_AddDataEntrega', 'TR') IS NOT NULL
    DROP TRIGGER AgroTrack_AddDataEntrega;
GO
CREATE TRIGGER AgroTrack_AddDataEntrega
ON AgroTrack_Encomenda
AFTER INSERT
AS
BEGIN
    UPDATE e
    SET Entrega = DATEADD(DAY, prazo_entrega, GETDATE())
    FROM AgroTrack_Encomenda e
    JOIN inserted i ON e.Codigo = i.Codigo;
END;