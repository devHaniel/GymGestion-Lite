# 🔐 Política de Seguridad - GymGestion-Lite

---

## 1. Seguridad de Acceso

### 1.1 Autenticación

- **Sistema de Login:** Usuario + Contraseña
- **Encriptación:** Contraseñas se almacenan encriptadas en base de datos
- **Sesión:** Timeout automático después de 30 minutos de inactividad
- **Intentos fallidos:** Máximo 3 intentos antes de bloquear cuenta por 5 minutos

### 1.2 Roles y Permisos

| Rol | Permisos | Casos de Uso |
|-----|----------|------------|
| **Admin** | Acceso total | Configuración sistema, usuarios, reportes |
| **Cajero** | Ventas, Compras, Corte | Operaciones diarias de caja |
| **Recepcionista** | Clientes, Membresías, Ventas | Atención al público |

### 1.3 Cambiar Contraseña

**Recomendación:** Cambiar contraseña cada 90 días

1. Ir a: Usuarios → Cambiar Contraseña
2. Ingresar contraseña actual
3. Nueva contraseña (mínimo 8 caracteres, incluir números y letras)
4. Click en "Guardar"

---

## 2. Seguridad de Datos

### 2.1 Protección de Base de Datos

- **SQL Server:** Requiere autenticación de Windows
- **Backup:** Se recomienda diario
- **Ubicación:** Base de datos local en el servidor
- **Puerto:** 1433 (SQL Server) - no exposer en Internet

### 2.2 Copias de Seguridad

**Backup Automático (Recomendado):**
```
Programar tarea en Windows para ejecutar backup diariamente
Horario sugerido: 21:00 horas (fuera de operaciones)
```

**Backup Manual:**
```
Ver sección "Backup" en INSTALL.md
```

**Almacenamiento:**
- Guardar backups en unidad externa/USB
- Almacenar copia en lugar seguro fuera de la oficina
- Probar restauración mensualmente

### 2.3 Datos de Clientes

El gimnasio es responsable de proteger datos personales:
- Cumplir leyes de protección de datos locales
- Acceso restringido a información sensible
- No compartir datos con terceros sin consentimiento

---

## 3. Seguridad de Red

### 3.1 En Red Local (Recomendado)

- ✅ SQL Server solo accesible desde PC local
- ✅ No exponer puerto 1433 en Internet
- ✅ Usar autenticación de Windows

### 3.2 En Red Remota (NO Recomendado)

Si absolutamente necesario acceder desde otra PC:

⚠️ **NO hacer esto sin implementar:**

1. **VPN:** Conexión cifrada
2. **Firewall:** Restringir IPs permitidas
3. **Autenticación SQL:** Usar credenciales SQL Server (no Windows)
4. **Encriptación:** SSL/TLS en conexión

**Comando para habilitar encriptación:**
```sql
-- En SQL Server Configuration Manager
-- Protocols for SQLEXPRESS → TCP/IP → Force Encryption: YES
```

---

## 4. Seguridad de Aplicación

### 4.1 Inyección SQL

✅ **GymGestion-Lite usa Dapper ORM** que previene inyección SQL:
- Parámetros compilados
- No concatenación de strings
- Validación de entrada

### 4.2 Validaciones

- Email: Formato válido
- Números: Solo dígitos permitidos
- Fechas: Formato DD/MM/YYYY
- Importes: Solo decimales positivos

### 4.3 Auditoría

Todas las operaciones quedan registradas:
- Usuario que realizó cambio
- Fecha y hora exacta
- Tipo de operación (Insert/Update/Delete)
- Valores antiguos y nuevos

Para consultar auditoría:
```sql
SELECT * FROM AUDITORIA ORDER BY fecha DESC;
```

---

## 5. Seguridad de Transacciones

### 5.1 Integridad de Datos

- **Transacciones:** Uso de BEGIN TRANSACTION / COMMIT / ROLLBACK
- **Validación cruzada:** Stock se verifica antes de venta
- **Cálculos automáticos:** Subtotales se calculan en BD

### 5.2 Prevención de Duplicados

```sql
-- Las transacciones se protegen contra duplicados
CREATE UNIQUE CONSTRAINT uq_venta_numero ON VENTAS(numero, corte_id)
```

---

## 6. Actualizaciones de Seguridad

### 6.1 Actualizaciones del Sistema

- Verificar mensuales en: Settings → Acerca de → Verificar actualizaciones
- Las actualizaciones son opcionales pero recomendadas
- Respaldar BD antes de aplicar actualización

### 6.2 Parches de Windows

Mantener actualizado:
- Windows Update: Automático
- SQL Server: Actualizaciones de seguridad
- .NET Framework: Actualizaciones críticas

---

## 7. Incidentes de Seguridad

### 7.1 Si Detectas Actividad Sospechosa

1. **Documentar:**
   - Fecha/hora del incidente
   - Usuario involucrado
   - Acción realizada
   - Datos afectados

2. **Contactar soporte:**
   - Email: [tu-email@ejemplo.com]
   - Teléfono: [tu-número]

3. **Medidas inmediatas:**
   - Cambiar contraseña del usuario
   - Revisar logs de auditoría
   - Restaurar desde backup si es necesario

### 7.2 Pérdida de Datos

**Protocolo de recuperación:**
```
1. Detener la aplicación
2. Hacer backup de archivos actuales
3. Restaurar desde backup anterior
4. Informar a usuario admin
5. Verificar integridad de datos
```

---

## 8. Checklist de Seguridad

### Diario:
- [ ] Verificar no hay accesos no autorizados
- [ ] Revisar reportes de venta/compra

### Semanal:
- [ ] Hacer backup manual (además del automático)
- [ ] Revisar usuarios conectados
- [ ] Verificar no hay errores en logs

### Mensual:
- [ ] Auditar cambios en usuarios
- [ ] Revisar log completo de auditoría
- [ ] Probar restauración desde backup
- [ ] Cambiar contraseña admin

### Trimestral:
- [ ] Forzar cambio de contraseñas de usuarios
- [ ] Revisar permisos de acceso
- [ ] Auditoría completa de datos

### Anual:
- [ ] Revisar política de seguridad
- [ ] Capacitar usuarios sobre seguridad
- [ ] Prueba completa de recuperación ante desastres

---

## 9. Contraseñas Seguras

### Requisitos:
- ✅ Mínimo 8 caracteres
- ✅ Combinar mayúsculas y minúsculas
- ✅ Incluir números
- ✅ Incluir símbolos especiales (@#$%^&*)

### Ejemplos Segura:
```
✅ Gym2024!Seguro
✅ CajaGym@2024
❌ admin123 (demasiado simple)
❌ 12345678 (solo números)
❌ password (palabra común)
```

---

## 10. Respaldo ante Desastres

### Escenario: Fallo Total de Computadora

**Procedimiento de recuperación:**

1. Instalar SO en nueva PC
2. Instalar .NET Framework 4.7.2
3. Instalar SQL Server Express
4. Instalar GymGestion-Lite
5. Restaurar base de datos desde último backup
6. Verificar integridad de datos
7. Probar operaciones básicas

**Tiempo estimado:** 2-4 horas

---

## 11. Contacto de Seguridad

En caso de incidente de seguridad, contactar a:

📧 **Email:** [tu-email@ejemplo.com]  
📱 **Emergencias:** [tu-número]  
🕐 **Disponibilidad:** Lunes-Viernes 9:00-17:00  

---

## 12. Disclaimer de Seguridad

- GymGestion-Lite se proporciona con medidas de seguridad estándar
- El usuario es responsable de mantener la seguridad física de la computadora
- Los backups son responsabilidad del usuario
- No somos responsables por pérdida de datos por negligencia

---

**Versión:** 1.0  
**Última actualización:** 2024-2025  
**Próxima revisión recomendada:** 2025
