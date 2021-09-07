# Web API #

## Requisitos ##

1.  Visual Studio Code [Link](https://code.visualstudio.com/)

2.  .NET 5.0 [Link](https://dotnet.microsoft.com/download/dotnet-core)

3.  Chrome, Firefox o Edge

4.  Fork (cliente GIT) [Link](https://git-fork.com/)

## Uso ##

1.  Abrir archivo `curso-net.code-workspace` con Visual Studio Code

2.  Instalar las extensiones que VS Code solicite.

3.  Abrir Pestaña de Depuracion  ``Ctrl + Mayus + D``

4.  Seleccionar opcion `.NET Core Launch (web) (Web API)`

5.  Seleccionar Iniciar depiracion (flecha verde)

## Convencion de Nombres ##

### Reglas ###

* No incluir el tipo en el nombre de la variable (No usar notación húngara)

* Evitar usar nombres de variable poco descriptivas (ej. ``x``, ``y``, ``mon``, etc...)

* Evitar usar nombres que que no sean de ayuda, confusos, vagos o ambiguos

* El nombre de la variable debe describir la informacion que va a contener de forma clara

* El codigo va a ser leido mas que lo que va a ser escrito, prioriza la legibilidad.

* Al definir constantes, puede que exista el escenario donde existan valores de constantes iguales con un significado distinto, poner nombre que defina exactamente el rol que realiza. (ej. ``const aprovado = 'A'`` y ``const anulado = 'A'``)

* Usar numbres en singular cuando se trata de 1 dato y plural cuando es una lista/arreglo (ej. ``cuenta``, ``cuentas[]``)

### Case Styles usadas en el proyecto ###

1.  Camel Case - ``variableNumeroUno``

2.  Pascal Case - ``VariableNumeroDos``

3.  Snake Case - ``variable_numero_tres``

4.  Kebab Case - ``variable-numero-cuatro``

### C# ###

* Camel Case - (variables, parametros)

* Pascal Case - (Metodos, MiembrosDeClases, IInterfaces, Clases, Namespaces, Enum)

### Reglas internas de C# ###

* No usar tipos de datos ``dynamic`` usar siempre tipos definidos en variables y retornos de metodos

* no usar ``var`` para definir variables. Las variables deben ser definidas con el tipo de forma explicita y no de forma implicita.

* Los controller no deben tener logica de negocio. Para eso debe haber un servicio (o varios) que provea de la logica a realizarse.

## Entity Framework ##

Instalar Herramientas de EF Core
``dotnet tool install --global dotnet-ef``

Actualizar Herramientas de EF Core a la ultima versión
``dotnet tool update --global dotnet-ef``

### Importar Tabla desde Base de datos usando **Darabase First** sin usar EF Core Power Tools (Extension para Visual Studio) ###

1.  Importar tablas a un contexto especifico. [Link](https://docs.microsoft.com/en-us/ef/core/managing-schemas/scaffolding?tabs=dotnet-core-cli) y [Parametros](https://docs.microsoft.com/en-us/ef/core/cli/dotnet#dotnet-ef-dbcontext-scaffold)
    
	``dotnet ef dbcontext scaffold "Server=(localdb)\MSSQLLocalDB;Database=AppDb;Trusted_Connection=True;Connection Timeout=60;" Microsoft.EntityFrameworkCore.SqlServer --output-dir Data/AppDb/Model --context-dir Data/AppDb/Context --context AppdbContext -t Tabla1, Tabla2, Tabla3 -f``

2.  En el contexto se reescribira la funcion `AppdbContext()`

    Hay que reemplazarla nuevamente por:

	```
	public AppdbContext(DbContextOptions<AppdbContext> options): base(options)
	{
	}
	```

### Crear Tabla en Base de Datos usando **Code First** ###

1.  Crear Clase con las columnas necesarias:

2.  Agregar la Clase al contexto como un **DbSet**:

    ``public DbSet<ZurichApp.Data.SolicitudSociedad> SolicitudSociedad { get; set; }``
	
3.  Agregar una Migracion al contexto AppDbContext [Link](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/#adding-another-migration):
    
	``dotnet ef migrations add "SolicitudSociedad" --context AppDbContext --output-dir "Migrations/App"``
	
4.  Aplicar las Migraciones a la base de datos:

	``dotnet ef database update -c AppDbContext``
	
Otros:

*  Eliminar la ultima Migracion del contexto [Link](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/#removing-a-migration):

	``dotnet ef migrations remove -c AppDbContext``

*  Rollback de las Migraciones a una migracion en especifico [Link](hhttps://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying?tabs=dotnet-core-cli#command-line-tools):

	``dotnet ef database update "Nombre de la ultima migración buena" -c AppDbContext``
	
*  Generar Scripts de las migraciones [Link](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying?tabs=dotnet-core-cli#sql-scripts):

	``dotnet ef migrations script -c AppdbContext``
