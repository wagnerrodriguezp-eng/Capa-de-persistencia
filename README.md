# SGA-ITLA — Capa de Persistencia

Capa de persistencia del **Sistema de Gestión de Autobuses del ITLA (SGA-ITLA)**,
implementada en **C# / .NET 8** con **Entity Framework Core** sobre **SQL Server**,
siguiendo los principios de **Clean Architecture** y **SOLID**.

**Autor:** Wagner Francisco Rodríguez Pimentel — 2025-1959
**Asignatura:** Programación II — Facilitador: Francis Ramírez

## Estructura

```
SGA.Dominio        -> Entidades, bases, enumeraciones e IRepositorioBase.
SGA.Modelo         -> Modelos de lectura para consultas (joins).
SGA.Persistencia   -> Base, Context, Interfaces y Repositories (ENTREGA).
```

### SGA.Persistencia
- **Base/** `RepositorioBase.cs` — operaciones genéricas (CRUD asíncrono).
- **Context/** `SGAContexto.cs` — DbContext con los DbSet de todos los schemas.
- **Interfaces/** contratos por schema: Seguridad, Transporte, Acceso, Operaciones, Auditoria.
- **Repositories/** implementaciones por schema (sub-folders por schema de BD).
- **IOC/** `RegistroDependenciasPersistencia.cs` — registro de repositorios en DI.

## Schemas y módulos

| Schema       | Repositorios |
|--------------|--------------|
| Seguridad    | Estudiante, Empleado, Conductor, Administrador |
| Transporte   | Autobus, Ruta, Parada, Horario, Viaje |
| Acceso       | TicketMensual, TarjetaRecargable, RegistroUso |
| Operaciones  | Incidencia |
| Auditoria    | RegistroAuditoria |

## Puesta en marcha
1. Abrir `SGA.sln` en Visual Studio 2022.
2. Restaurar los paquetes NuGet (EF Core 8).
3. Configurar la cadena de conexión a SQL Server en la capa de presentación.
