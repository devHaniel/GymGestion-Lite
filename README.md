# 💪 GymGestion-Lite

**Software de gestión integral para gimnasios pequeños y medianos**

Sistema de escritorio desarrollado en **C# WinForms** con arquitectura de 3 capas, diseñado específicamente para optimizar las operaciones diarias de un gimnasio con una única estación de trabajo.

---

## 🎯 Descripción

GymGestion-Lite es una solución completa y fácil de usar para administrar todas las operaciones de un gimnasio moderno. Desde la gestión de clientes y membresías hasta el control de inventario y reportes de caja, todo en una interfaz intuitiva y rápida.

**Perfecto para:** Gimnasios pequeños, estudios de fitness, centros de entrenamiento personal

---

## ✨ Funcionalidades Principales

### 👥 Gestión de Clientes
- Registro completo de miembros
- Histórico de actividad
- Búsqueda y filtrado rápido
- Datos de contacto y emergencia

### 🎫 Membresías
- Creación de planes personalizados
- Asignación de membresías a clientes
- Seguimiento de vencimientos
- Alertas de renovación automáticas

### 🛍️ Inventario de Productos
- Catálogo de productos (proteínas, accesorios, etc.)
- Control de stock en tiempo real
- Alertas de stock bajo
- Gestión de proveedores

### 📦 Compras a Proveedores
- Registro de compras
- Detalles de facturas
- Seguimiento de pagos
- Histórico de transacciones

### 💳 Ventas y Punto de Venta
- Venta de productos
- Emisión de recibos
- Descuentos y promociones
- Histórico de ventas

### 📊 Corte de Caja
- Cierre diario de operaciones
- Reconciliación de dinero
- Reportes de movimientos
- Auditoría financiera

### 📈 Reportes
- Panel administrativo
- Estadísticas de membresías
- Análisis de ventas
- Reportes financieros

### 👤 Categorías de Productos
- Organización flexible
- Búsqueda por categoría
- Gestión de tipos

---

## 🛠️ Tecnologías

| Capa | Tecnología |
|------|-----------|
| **Presentación** | C# WinForms (.NET Framework 4.7.2) |
| **Lógica de Negocio** | C# Class Library |
| **Acceso a Datos** | Dapper ORM |
| **Base de Datos** | SQL Server 2019+ |
| **Autenticación** | Usuario/Contraseña con encriptación |

---

## 📋 Requisitos del Sistema

### Mínimos:
- **Sistema Operativo:** Windows 7 SP1 o superior
- **Procesador:** Intel/AMD 2.0 GHz o superior
- **Memoria RAM:** 2 GB
- **Disco Duro:** 500 MB disponibles

### Recomendados:
- **Sistema Operativo:** Windows 10/11 (64-bit)
- **Procesador:** Intel/AMD 2.5 GHz o superior
- **Memoria RAM:** 4 GB o más
- **Disco Duro:** SSD con 1 GB disponible
- **Base de Datos:** SQL Server 2019 o superior

### Software Requerido:
- **.NET Framework 4.7.2** (incluido en instalador)
- **SQL Server 2019 Express** (versión gratuita disponible)

---

## 📥 Instalación

### Opción 1: Instalador Ejecutable (Recomendado)

1. Descargar el instalador `GymGestion-Lite-Setup.exe`
2. Ejecutar como administrador
3. Seguir el asistente de instalación
4. El instalador configura automáticamente:
   - .NET Framework 4.7.2
   - SQL Server Express (si no está instalado)
   - Base de datos inicial
   - Acceso directo en escritorio

### Opción 2: Instalación Manual

Ver archivo `INSTALL.md` para instrucciones detalladas.

---

## 🚀 Primeros Pasos

### 1. Primer Inicio

```
1. Ejecutar GymGestion-Lite.exe
2. Pantalla de Login aparecerá
3. Usuario por defecto: admin
4. Contraseña por defecto: admin123
```

⚠️ **IMPORTANTE:** Cambiar la contraseña del administrador en la primera sesión.

### 2. Configuración Inicial

1. **Crear categorías de productos**
   - Ir a: Datos → Categorías
   - Crear tipos según tu gimnasio

2. **Agregar proveedores**
   - Ir a: Datos → Proveedores
   - Registrar proveedores principales

3. **Crear planes de membresía**
   - Ir a: Operaciones → Planes Membresía
   - Definir planes (mensual, trimestral, anual)

4. **Registrar clientes**
   - Ir a: Operaciones → Clientes
   - Comenzar a cargar la base de datos

### 3. Operaciones Diarias

```
Inicio de Día:
  ↓
Abrir Caja (Cortes → Abrir Corte)
  ↓
Registrar operaciones (Ventas, Membresías, etc.)
  ↓
Cierre de Día:
  Cortes → Cerrar Corte
  Reconciliar dinero
  Generar reporte
```

---

## 📊 Arquitectura

```
GymGestion-Lite.sln
├── Entities/              ← Modelos y ViewModels
│   ├── Cliente.cs
│   ├── Membresia.cs
│   ├── Producto.cs
│   └── VistaModelos/      ← Modelos para UI
│
├── DataAccess/            ← Capa de datos
│   ├── Conexion.cs        ← Configuración BD
│   ├── ClienteRepository.cs
│   ├── MembresiaRepository.cs
│   └── ...
│
├── BusinessLogic/         ← Lógica de negocio
│   ├── ClienteService.cs
│   ├── MembresiaService.cs
│   ├── Validaciones/
│   └── Utils/             ← Encriptación, helpers
│
└── UI/                    ← Interfaz gráfica WinForms
    ├── FmrMain.cs         ← Pantalla principal
    ├── Clientes/
    ├── Productos/
    ├── Ventas/
    ├── Cortes/
    └── Login/
```

### Flujo de Dependencias:
```
UI ← BusinessLogic ← DataAccess ← Entities
```

---

## 🔐 Seguridad

- **Autenticación:** Login con usuario y contraseña encriptada
- **Roles:** Sistema de permisos por usuario (Admin, Vendedor, etc.)
- **Auditoria:** Todos los cambios quedan registrados
- **Respaldo:** Se recomienda backup diario de la base de datos

Ver `SECURITY.md` para más detalles.

---

## 📄 Licencia

Este software es **software propietario** bajo licencia comercial.

**Prohibido:**
- ❌ Copiar, modificar o distribuir sin autorización
- ❌ Usar en múltiples gimnasios sin licencia adicional
- ❌ Revender o sublicenciar

**Permitido (con licencia):**
- ✅ Instalar y usar en UNA ubicación
- ✅ Usar por tiempo indefinido (compra única)
- ✅ Recibir actualizaciones de seguridad

Ver `LICENSE.md` para términos completos.

---

## 🆘 Soporte Técnico

### Problemas Comunes

**P: "Error de conexión a la base de datos"**
```
R: 1. Verificar que SQL Server está ejecutándose
   2. Revisar cadena de conexión en App.config
   3. Confirmar permisos de acceso
```

**P: "La interfaz se ve muy pequeña/grande"**
```
R: El sistema se ajusta automáticamente al DPI de la pantalla
   Reiniciar la aplicación si persiste el problema
```

**P: "¿Cómo hacer backup de datos?"**
```
R: Ver INSTALL.md sección "Backup de Base de Datos"
```

---

## 👨‍💻 Desarrollado por

**Haniel Hernández**

---

## 📜 Términos Legales

Este producto se proporciona "tal cual" sin garantías explícitas o implícitas. El usuario asume toda responsabilidad sobre el uso y los datos almacenados.

---

**© 2024-2025 GymGestion-Lite. Todos los derechos reservados.**
