# Prueba técnica — Migración .NET Framework 4.7 → .NET 8

## Contexto

`PortfolioOrderApp` es una pequeña aplicación ASP.NET MVC 5 sobre **.NET
Framework 4.7** que forma parte de un sistema de gestión de portafolios de
inversión. Es representativa (a escala reducida) de los problemas reales
que nos vamos a encontrar en la migración del sistema completo a
**.NET 8**.

La app permite:

- Ver un catálogo de instrumentos (acciones, bonos, ETFs) con su precio.
- Armar una orden (ticket) de compra/venta agregando líneas antes de
  confirmarla — la orden en curso se guarda por sesión de usuario.
- Confirmar la orden, lo que genera un registro de auditoría.
- Solicitar un depósito a la cuenta de inversión, adjuntando el
  comprobante de la transferencia (imagen o PDF).


## Objetivo

Migra esta aplicación a **ASP.NET Core 8**, conservando el mismo
comportamiento funcional:

1. Se puede listar el catálogo de instrumentos.
2. Se puede armar una orden agregando líneas (instrumento, lado
   compra/venta, cantidad) — la orden en curso persiste por sesión de
   usuario mientras no se confirma.
3. Al confirmar la orden, queda un registro de auditoría persistido en
   disco (no tiene que ser el mismo formato de archivo, pero sí debe
   seguir quedando un registro consultable de cada orden confirmada).
4. Se puede solicitar un depósito adjuntando un comprobante (JPEG, PNG o
   PDF, máx. 5 MB), y ese depósito queda listado con estado "Pendiente de
   validación".
5. Se debe seguir contando con alguna forma de medir cuánto tarda cada
   request (lo que hoy hace `ComplianceTraceModule`), como parte de los
   requisitos de trazabilidad.

No es necesario que uses MVC en Core: puedes usar Razor Pages, MVC clásico
sobre Core, o Minimal API + una vista simple — **justifica tu elección**
en las notas de migración.

## Lo que tienes que entregar

1. El proyecto migrado y funcionando en **.NET 8** (`dotnet run` debe
   levantar la app sin errores).
2. Un archivo `MIGRATION_NOTES.md` donde expliques, qué cambiaste y por qué
3. Si detectas algo en el código original que consideras mala práctica
   (más allá de lo específico de la migración), coméntalo en las notas.

## Lo que NO es necesario

- No necesitas conectar una base de datos real (el catálogo y las
  órdenes pueden seguir viviendo en memoria/seed).
- No necesitas implementar lógica financiera real (cálculo de
  rendimientos, tasas, comisiones, etc.) — el foco es la migración
  técnica, no el dominio de negocio.

## Extras opcionales (suman, no son obligatorios)

- Tests unitarios para `OrderTicketService` (o su equivalente migrado).
- Inyección de dependencias correctamente aplicada donde antes había
  `new ServicioX()` directo en los controllers.

## Tiempo sugerido

Estimamos entre 3 y 5 horas de trabajo efectivo. No es una prueba de
velocidad: preferimos una migración bien razonada, aunque cubra menos
extras, a una apurada.

## Cómo entregar

Sube tu solución a un repositorio (privado) y compártenos el acceso, o
envíanos un `.zip` con el proyecto migrado + `MIGRATION_NOTES.md`.

##Uso de IA
Si utilizas LLMs, está bien, pero debes compartir las conversaciones.