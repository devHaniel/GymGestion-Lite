# GYMGESTION

Sistema de gestión para gimnasio desarrollado en **C# WinForms (.NET 8)** con arquitectura de 3 capas y SQL Server como motor de base de datos.

---

## Tecnologías

| Capa | Tecnología |
|------|-----------|
| Presentación | C# WinForms (.NET 8) |
| Negocio | C# Class Library (.NET 8) |
| Datos | Dapper + Microsoft.Data.SqlClient |
| Base de datos | SQL Server |

---

## Arquitectura

```
SIS_GYM.sln
├── Gimnasio.Entities        → Modelos y ViewModels
├── Gimnasio.DataAccess      → Repositorios con Dapper
├── Gimnasio.BusinessLogic   → Servicios y validaciones
└── Gimnasio.UI              → Formularios WinForms
```

### Flujo de dependencias
```
UI → BusinessLogic → DataAccess → Entities
```
## Módulos

- Corte de caja
- Clientes
- Planes de membresía
- Membresías
- Productos y proveedores
- Compras
- Ventas
- Reportes y panel admin

---

## Flujo de corte de caja

```
Login
  └─ ¿Hay corte abierto?
        ├─ Sí → FrmPrincipal
        └─ No
              ├─ Admin / Cajero → FrmCorteAbrir → FrmPrincipal
              └─ Recepcionista  → Aviso y cierre de sesión
```

Durante el día cada venta y compra se registra con el `corte_id` activo. Al cerrar el día el sistema calcula todos los totales desde las transacciones reales y los persiste en el registro del corte.

---

## Tipos de venta

| Tipo | Descripción |
|------|-------------|
| `producto` | Solo venta de productos del inventario |
| `membresia` | Solo pago de membresía |
| `mixta` | Membresía + productos en una sola transacción |

---

## Roles de usuario

| Rol | Permisos |
|-----|----------|
| `admin` | Acceso total — usuarios, reportes, configuración |
| `cajero` | Abrir/cerrar corte, ventas, compras |
| `recepcionista` | Ventas, clientes, membresías |

---


---

## Notas importantes

- `VentaRepository.Insertar()` y `CompraRepository.Insertar()` usan transacciones SQL — si algo falla, el stock y la venta hacen rollback automáticamente.
- Las eliminaciones son **lógicas** (`Activo = 0`) en clientes, usuarios y productos para no perder historial de ventas.
- `subtotal` en `DETALLE_VENTAS` y `DETALLE_COMPRAS` es una columna calculada persistida (`AS cantidad * precio_unitario PERSISTED`).
- `VW_VENTAS_DETALLE` usa `UNION ALL` para separar líneas de productos y membresías sin duplicar filas en ventas mixtas.
- El `gran_total` del corte representa el total de ventas — las compras se muestran como información referencial separada.

---

## Instalación

1. Clonar el repositorio.
2. Ejecutar en SQL Server en orden:
   - `gym_db.sql` — tablas
3. Configurar la cadena de conexión en `App.config` del proyecto `Gimnasio.UI`.
4. Instalar paquetes NuGet en `Gimnasio.DataAccess`:
   - `Dapper`
   - `Microsoft.Data.SqlClient`
5. Compilar y ejecutar `Gimnasio.UI`.

---

## Convenciones de código

- Propiedades en `PascalCase`.
- Parámetros y variables locales en `camelCase`.
- Repositorios solo acceden a datos — sin lógica de negocio.
- Servicios validan antes de llamar al repositorio.
- La UI nunca referencia `DataAccess` directamente.
