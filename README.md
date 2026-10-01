# GymMaster API

## Autores

Sebastián Rendón Giraldo  
Mariana Villegas


## Descripción

GymMaster API es el backend del sistema **GymMaster**, una plataforma para la gestión integral de gimnasios desarrollada con **ASP.NET Core Web API**. La aplicación permite administrar usuarios, rutinas de entrenamiento, ejercicios, perfiles físicos mediante una API REST segura y escalable.

---

## Características

* Autenticación mediante JWT.
* Gestión de usuarios y perfiles.
* Administración de rutinas de entrenamiento.
* Gestión de ejercicios asociados a cada rutina.
* Integración con PostgreSQL mediante Entity Framework Core.
* Arquitectura REST para facilitar la integración con aplicaciones frontend.

---

## Tecnologías Utilizadas

* ASP.NET Core Web API
* C#
* Entity Framework Core
* PostgreSQL
* JWT Authentication
* OpenAPI / Scalar
* LINQ
* Git y GitHub

---

## Requisitos Previos

Antes de ejecutar el proyecto, asegúrate de tener instalado:

* .NET SDK
* PostgreSQL
* pgAdmin (opcional)
* Git

Puedes verificar las instalaciones con:

```bash
dotnet --version
psql --version
git --version
```

---

# Instalación

## 1. Clonar el repositorio

```bash
git clone https://github.com/SebasG789/GymMaster-API.git
cd GymMaster-API
```

---

# Configuración de la Base de Datos

Dentro del proyecto encontrarás el archivo:

```text
GymSystemApi/GymMaster_Backup.sql
```

Este archivo contiene la estructura de la base de datos y los datos iniciales necesarios para ejecutar correctamente la aplicación.

> **Importante:** Se recomienda restaurarlo sobre una base de datos vacía para evitar conflictos con tablas existentes.

Puedes restaurarlo utilizando cualquiera de los siguientes métodos.

---

## Opción 1: Restaurar usando pgAdmin

### Paso 1. Abrir pgAdmin

Inicia pgAdmin y conéctate a tu servidor PostgreSQL.

### Paso 2. Crear una base de datos

1. Haz clic derecho sobre **Databases**.
2. Selecciona **Create → Database...**
3. Asigna el siguiente nombre:

```text
Gym-SystemDB
```

4. Presiona **Save**.

---

### Paso 3. Abrir el Query Tool

Selecciona la base de datos creada y luego:

```text
Tools → Query Tool
```

---

### Paso 4. Abrir el archivo SQL

Haz clic en el icono **Open File** y selecciona:

```text
GymSystemApi/GymMaster_Backup.sql
```

---

### Paso 5. Ejecutar el script

Presiona el botón **Execute** o utiliza la tecla:

```text
F5
```

Espera a que finalice la importación.

---

### Paso 6. Verificar la restauración

Una vez finalizado el proceso, actualiza la base de datos y ejecuta:

```sql
SELECT COUNT(*) FROM "Usuarios";
```

Si el resultado es mayor que cero, la restauración fue realizada correctamente.

---

## Opción 2: Restaurar usando la terminal (psql)

### Paso 1. Crear una base de datos vacía

```bash
createdb -U postgres "Gym-SystemDB"
```

Si la base de datos ya existe, elimínala previamente o utiliza una nueva.

---

### Paso 2. Restaurar el archivo SQL

Si estás ubicado en la raíz del repositorio:

```bash
psql -U postgres -d "Gym-SystemDB" -f GymSystemApi/GymMaster_Backup.sql
```

O indicando la ruta completa:

**Windows**

```bash
psql -U postgres -d "Gym-SystemDB" -f "C:\ruta\completa\GymMaster-API\GymSystemApi\GymMaster_Backup.sql"
```

**Linux / macOS**

```bash
psql -U postgres -d "Gym-SystemDB" -f /ruta/completa/GymMaster-API/GymSystemApi/GymMaster_Backup.sql
```

Ingresa la contraseña del usuario `postgres` cuando sea solicitada.

Una vez termine el proceso, todas las tablas y registros quedarán restaurados automáticamente.

---

# Configuración del Backend

Por seguridad, las credenciales de PostgreSQL y la clave utilizada para firmar los tokens JWT no se almacenan directamente en el repositorio.

El proyecto utiliza la configuración estándar de ASP.NET Core, por lo que estos valores pueden configurarse localmente mediante **User Secrets**.

Ubícate en la carpeta:

```text
GymSystemApi
```

```bash
cd GymSystemApi
```

Inicializa User Secrets:

```bash
dotnet user-secrets init
```

Configura la conexión a PostgreSQL:

```bash
dotnet user-secrets set "ConnectionStrings:PostSqlConnection" "Server=127.0.0.1;Port=5432;Database=Gym-SystemDB;User Id=postgres;Password=TU_CONTRASEÑA"
```

Configura una clave privada para JWT:

```bash
dotnet user-secrets set "Jwt:Key" "TU_CLAVE_JWT_SEGURA"
```

Puedes verificar los valores configurados con:

```bash
dotnet user-secrets list
```

> Los secretos se almacenan únicamente en el equipo local y no se incluyen en Git ni en GitHub.

El archivo `appsettings.json` conserva únicamente la configuración pública de la aplicación:

```json
{
  "ConnectionStrings": {
    "PostSqlConnection": ""
  },
  "Jwt": {
    "Key": "",
    "Issuer": "GymSystemApi",
    "Audience": "GymSystemApp",
    "ExpiresInMinutes": 60
  }
}
```

---

# Ejecutar el Proyecto

Desde la carpeta `GymSystemApi` ejecuta:

```bash
dotnet restore
dotnet build
dotnet run
```

Si la configuración es correcta, la API iniciará sin inconvenientes.

---

# Verificación

Para comprobar que todo funciona correctamente:

1. La API debe iniciar sin errores.
2. PostgreSQL debe aceptar la conexión.
3. La base de datos debe contener información.
4. Deben existir las tablas del sistema.
5. El inicio de sesión debe funcionar correctamente.

Puedes verificar la existencia de datos ejecutando:

```sql
SELECT COUNT(*) FROM "Usuarios";
```

o consultar directamente:

```sql
SELECT * FROM "Usuarios";
```

---

# Solución de Problemas

## Error: `relation already exists`

Este error indica que las tablas ya existen dentro de la base de datos.

La solución recomendada es:

1. Eliminar la base de datos actual.
2. Crear una nueva base de datos vacía llamada `Gym-SystemDB`.
3. Ejecutar nuevamente el archivo `GymMaster_Backup.sql`.

---

## Error de conexión con PostgreSQL

Verifica que:

* PostgreSQL esté ejecutándose.
* El puerto configurado sea el correcto (por defecto `5432`).
* El usuario y la contraseña coincidan con los configurados mediante User Secrets.
* La base de datos `Gym-SystemDB` exista.

---

# Estructura del Proyecto

```text
GymMaster-API/
│
├── GymSystemApi/
│   ├── Controllers/
│   ├── DTOs/
│   ├── Data/
│   ├── Migrations/
│   ├── Models/
│   ├── Services/
│   ├── GymMaster_Backup.sql
│   ├── Program.cs
│   ├── appsettings.json
│   └── GymSystemApi.csproj
│
├── GymSystem.slnx
└── README.md
```

---

# Contribuciones

Si deseas contribuir al proyecto:

1. Realiza un fork del repositorio.
2. Crea una nueva rama para tus cambios.
3. Implementa las mejoras correspondientes.
4. Envía un Pull Request para su revisión.

---

# Licencia

Este proyecto fue desarrollado con fines educativos y de aprendizaje. Puedes utilizarlo, modificarlo y adaptarlo según las necesidades de tu implementación, respetando las condiciones establecidas por su autor.
