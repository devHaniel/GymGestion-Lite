# 📥 Guía de Instalación - GymGestion-Lite

---

## 🚀 Instalación Rápida (Recomendado)

### Requisitos Previos:
- Windows 7 SP1 o superior
- 500 MB de espacio en disco
- Conexión a Internet (solo para descarga)

### Pasos:

1. **Descargar el instalador**
   - Obtener `GymGestion-Lite-Setup.exe` del proveedor

2. **Ejecutar como administrador**
   ```
   Click derecho → Ejecutar como administrador
   ```

3. **Seguir el asistente**
   - Aceptar términos de licencia
   - Seleccionar ruta de instalación (recomendado: `C:\Program Files\GymGestion-Lite\`)
   - El instalador descargará e instalará automáticamente:
     - .NET Framework 4.7.2
     - SQL Server Express
     - Base de datos inicial

4. **Crear acceso directo**
   - El instalador crea un acceso directo en el escritorio
   - Hacer doble click para iniciar

5. **Primer inicio**
   - Usuario: `admin`
   - Contraseña: `admin123`
   - ⚠️ **Cambiar contraseña inmediatamente**

---

## 🔧 Instalación Manual

Si prefieres instalar manualmente o tienes problemas con el instalador automático.

### Paso 1: Instalar .NET Framework 4.7.2

1. Descargar desde: https://dotnet.microsoft.com/download/dotnet-framework/net472
2. Ejecutar como administrador
3. Reiniciar la computadora

### Paso 2: Instalar SQL Server Express

1. Descargar desde: https://www.microsoft.com/sql-server/sql-server-express
2. Seleccionar instalación **"Basic"** (recomendado para usar localmente)
3. Configuración:
   - Nombre de instancia: `SQLEXPRESS` (por defecto está bien)
   - Habilitar TCP/IP
   - Puerto: `1433`

4. Permitir acceso de Windows Firewall:
   - Windows Firewall → Permitir aplicación → SQL Server

### Paso 3: Crear Base de Datos

1. Descargar script: `gym_db.sql` (incluido en instalador)

2. Abrir **SQL Server Management Studio**

3. Conectar con:
   - Servidor: `(local)\SQLEXPRESS` o `localhost`
   - Autenticación: Windows
   - Click en "Conectar"

4. Abrir archivo `gym_db.sql`:
   - File → Open → Select gym_db.sql
   - Click en "Execute" (F5)
   - Esperar a que termine (aparecerá "Commands completed successfully")

5. Verificar tablas creadas:
   - Expandir: `Databases` → `SIS_GYM` → `Tables`
   - Deben aparecer todas las tablas

### Paso 4: Instalar GymGestion-Lite

1. Extraer archivos ejecutables de GymGestion-Lite

2. Copiar a carpeta: `C:\Program Files\GymGestion-Lite\`

3. Crear acceso directo en escritorio:
   - Click derecho → Nuevo → Acceso directo
   - Ruta: `C:\Program Files\GymGestion-Lite\GymGestion-Lite.exe`

### Paso 5: Configurar Conexión (si es necesario)

Si hay error de conexión a BD:

1. Abrir con editor de texto:
   ```
   C:\Program Files\GymGestion-Lite\GymGestion-Lite.exe.config
   ```

2. Buscar sección: `<connectionStrings>`

3. Si usas SQLEXPRESS local, debe ser:
   ```xml
   <connectionStrings>
     <add name="DataAccess.Properties.Settings.ConnectionString" 
          connectionString="Data Source=localhost;Initial Catalog=SIS_GYM;Integrated Security=True;TrustServerCertificate=True"
          providerName="System.Data.SqlClient" />
   </connectionStrings>
   ```

4. Guardar y reiniciar aplicación

---

## 📊 Verificar Instalación

### Paso 1: Probar conexión a BD

1. Abrir SQL Server Management Studio
2. Conectar a: `localhost\SQLEXPRESS` o `(local)\SQLEXPRESS`
3. Expandir → Databases → Buscar `SIS_GYM`
4. Debe tener tablas visibles

### Paso 2: Iniciar GymGestion-Lite

1. Hacer doble click en el acceso directo
2. Debe aparecer pantalla de Login
3. Ingresar:
   - Usuario: `admin`
   - Contraseña: `admin123`
4. Click en "Iniciar Sesión"

### Paso 3: Si todo funciona

✅ Debe abrir pantalla principal con menús y botones

---

## ⚠️ Solución de Problemas

### Problema 1: "No se puede conectar al servidor"

**Solución:**
1. Verificar SQL Server está corriendo:
   - Windows Start → SQL Server Configuration Manager
   - SQL Server Services → SQL Server (SQLEXPRESS) → Estado: Running

2. Si no está corriendo:
   - Click derecho → Start
   - Esperar 30 segundos

3. Verificar puerto:
   - SQL Server Configuration Manager → Protocols for SQLEXPRESS
   - TCP/IP debe estar habilitado
   - Verificar puerto 1433

### Problema 2: ".NET Framework 4.7.2 no está instalado"

**Solución:**
1. Descargar desde: https://dotnet.microsoft.com/download/dotnet-framework/net472
2. Instalar como administrador
3. Reiniciar computadora
4. Reintentar

### Problema 3: "Error: Database 'SIS_GYM' does not exist"

**Solución:**
1. Abrir SQL Server Management Studio
2. Conectar a servidor
3. Ejecutar script `gym_db.sql` nuevamente
4. Verificar que aparece base de datos `SIS_GYM`

### Problema 4: Acceso denegado al conectar

**Solución:**
1. Verificar que SQL Server permite autenticación de Windows:
   - SQL Server Management Studio → Properties
   - Security → Server authentication: "SQL Server and Windows Authentication mode"

2. Reiniciar SQL Server:
   - SQL Server Configuration Manager → Restart

### Problema 5: La aplicación se abre pero se ve muy pequeña/grande

**Solución:**
- Reiniciar aplicación
- El escalado DPI se ajusta automáticamente
- Si persiste: Actualizar drivers de pantalla

### Problema 6: Error al iniciar sesión

**Solución:**
1. Verificar usuario/contraseña (por defecto: `admin` / `admin123`)
2. Revisar tablas de usuarios en BD:
   - SQL Server Management Studio
   - Ejecutar: `SELECT * FROM USUARIOS;`
   - Debe existir usuario `admin`

---

## 💾 Backup de Base de Datos

### Hacer Backup Manual

1. Abrir SQL Server Management Studio

2. Conectar a servidor

3. Click derecho en `SIS_GYM` → Tasks → Back Up...

4. Configurar:
   - Destination: Guardar en disco
   - Ruta recomendada: `C:\Backups\`
   - Nombre: `SIS_GYM_[FECHA].bak`

5. Click en "OK"

### Restaurar Backup

1. Abrir SQL Server Management Studio

2. Conectar a servidor

3. Click derecho en Databases → Restore Database...

4. Seleccionar archivo `.bak`

5. Click en "OK"

---

## 🔐 Cambiar Contraseña de Administrador

⚠️ **IMPORTANTE:** Hacer esto en el primer inicio

1. Iniciar sesión con usuario: `admin` / `admin123`

2. Ir a: Usuarios → Cambiar Contraseña

3. Ingresar:
   - Contraseña actual: `admin123`
   - Contraseña nueva: (tu nueva contraseña segura)
   - Confirmar: (repetir contraseña)

4. Click en "Guardar"

---

## 🌍 Configuración de Red (Opcional)

Si necesitas acceder desde otra PC en la red:

1. En la PC donde está el servidor SQL:
   - Anotar IP: Cmd → `ipconfig` → buscar "IPv4 Address"

2. En la PC cliente:
   - Editar `App.config`
   - Cambiar: `Data Source=localhost` por `Data Source=[IP_DEL_SERVIDOR]`

3. Verificar que Windows Firewall permite SQL Server:
   - Windows Firewall → Allow apps → SQL Server

---

## 📞 Soporte

Si tienes problemas:

1. Revisar esta guía (sección "Solución de Problemas")
2. Verificar que todos los requisitos están instalados
3. Contactar al soporte técnico

---

**Versión:** 1.0  
**Última actualización:** 2024-2025
