Use NeptunoDB

--	Procedimiento para ver los productos
ALTER PROCEDURE sp_GetProdcuts
AS
BEGIN
    SELECT idproducto, nombreProducto, cantidadPorUnidad, precioUnidad, categoriaProducto 
    FROM dbo.productos
    ORDER BY idproducto DESC;
END;


-- Store Procedure para registrar nuevo producto
ALTER PROCEDURE sp_AddProduct
    @nombreProducto VARCHAR(40),
    @idProveedor INT,
    @idCategoria INT,
    @cantidadPorUnidad VARCHAR(20),
    @precioUnidad DECIMAL(18, 0),
    @unidadesEnExistencia SMALLINT,
    @unidadesEnPedido SMALLINT,
    @nivelNuevoPedido SMALLINT,
    @suspendido SMALLINT,
    @categoriaProducto VARCHAR(20)
AS
BEGIN

	DECLARE @nuevoId INT;
    -- Obtener el próximo valor para la clave primaria
    SELECT @nuevoId = ISNULL(MAX(idproducto), 0) + 1 FROM dbo.productos;

    INSERT INTO dbo.productos
    (
        idproducto,
		nombreProducto,
        idProveedor,
        idCategoria,
        cantidadPorUnidad,
        precioUnidad,
        unidadesEnExistencia,
        unidadesEnPedido,
        nivelNuevoPedido,
        suspendido,
        categoriaProducto
    )
    VALUES
    (
		@nuevoId,
        @nombreProducto,
        @idProveedor,
        @idCategoria,
        @cantidadPorUnidad,
        @precioUnidad,
        @unidadesEnExistencia,
        @unidadesEnPedido,
        @nivelNuevoPedido,
        @suspendido,
        @categoriaProducto
    );
END;

--ejecutar
EXEC sp_AddProduct
    @nombreProducto = 'Salsa de maiz',
    @idProveedor = 1,
    @idCategoria = 2,
    @cantidadPorUnidad = '10 unidades por caja',
    @precioUnidad = 25,
    @unidadesEnExistencia = 100,
    @unidadesEnPedido = 20,
    @nivelNuevoPedido = 5,
    @suspendido = 0,
    @categoriaProducto = 'CATEGORIA C';


-- Store Procedure para actualizar un row
CREATE PROCEDURE sp_UpdateProduct
    @IdProducto INT,
    @NuevoNombreProducto NVARCHAR(40),
    @NuevoIdProveedor INT,
    @NuevoIdCategoria INT,
    @NuevaCantidadPorUnidad NVARCHAR(20),
    @NuevoPrecioUnidad DECIMAL(18, 0),
    @NuevasUnidadesEnExistencia SMALLINT,
    @NuevasUnidadesEnPedido SMALLINT,
    @NuevoNivelNuevoPedido SMALLINT,
    @NuevoSuspendido SMALLINT,
    @NuevaCategoriaProducto NVARCHAR(20)
AS
BEGIN
    UPDATE dbo.productos
    SET
        nombreProducto = @NuevoNombreProducto,
        idProveedor = @NuevoIdProveedor,
        idCategoria = @NuevoIdCategoria,
        cantidadPorUnidad = @NuevaCantidadPorUnidad,
        precioUnidad = @NuevoPrecioUnidad,
        unidadesEnExistencia = @NuevasUnidadesEnExistencia,
        unidadesEnPedido = @NuevasUnidadesEnPedido,
        nivelNuevoPedido = @NuevoNivelNuevoPedido,
        suspendido = @NuevoSuspendido,
        categoriaProducto = @NuevaCategoriaProducto
    WHERE idproducto = @IdProducto;
END


--Store procedure para eliminar un producto
CREATE PROCEDURE DeleteProduct
    @IdProducto INT
AS
BEGIN
    DELETE FROM productos
    WHERE idproducto = @IdProducto
END

-- ejemplo
EXEC DeleteProduct @IdProducto=79


--Store procedure para obtener un producto
CREATE PROCEDURE GetOneProduct
@IdProducto INT
AS
BEGIN 
SELECT * FROM productos WHERE idproducto = @IdProducto;
END;
