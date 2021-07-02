-- Clientes
IF NOT EXISTS (SELECT *
FROM Cliente)
BEGIN
    INSERT INTO Cliente
        (Nombre, Apellidos, Direccion, Telefono)
    VALUES
        ('Juan', 'Perez', 'Calle asd 123', '+56 9 7834 2167')

    INSERT INTO Cliente
        (Nombre, Apellidos, Direccion, Telefono)
    VALUES
        ('Lorena', 'Gomez', 'Calle dsa 213', '+56 9 7843 2617')

    INSERT INTO Cliente
        (Nombre, Apellidos, Direccion, Telefono)
    VALUES
        ('Fernando', 'Lopez', 'Calle ads 321', '+56 9 7384 2176')
END
