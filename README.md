## Gestión de inventario

Requisitos mínimos para considerarlo completo:

- Crear una clase ProductModel.
- Agregar, modificar y eliminar productos.
- Buscar productos por nombre o código utilizando LINQ.
- Mostrar stock disponible.
- Evitar cantidades negativas.
- Utilizar listas genéricas (List<T>).
- Persistir la lógica en métodos reutilizables.

## Catálogo de productos

Requisitos mínimos para considerarlo completo:

- Crear una jerarquía de productos utilizando herencia.
- Implementar interfaces para definir comportamientos comunes.
- Mostrar catálogos filtrados con LINQ.
- Ordenar productos por precio y nombre.
- Aplicar polimorfismo en la presentación de productos.
- Implementar operaciones asincrónicas simuladas con async/await.

- Manejar errores de carga o selección de productos.

## Informe de seguimiento

**Estado general:** los requisitos mínimos todavía no están completos. El proyecto compila, pero el CRUD presenta un problema de persistencia y varios requisitos del catálogo aún no están implementados.

### Gestión de inventario

| Requisito | Estado | Observación |
|---|---|---|
| Crear una clase `ProductModel` | ✅ Completo | La clase está definida en `Models/ProductModel.cs`. |
| Agregar, modificar y eliminar productos | ⚠️ Parcial | Hay métodos y opciones de menú, pero los cambios no se conservan correctamente: el repositorio obtiene una lista nueva del archivo en cada acceso. Las modificaciones se hacen sobre listas temporales. |
| Buscar productos por nombre o código utilizando LINQ | ✅ Completo | La búsqueda por nombre usa `Where` y la búsqueda por EAN usa `FirstOrDefault`. |
| Mostrar stock disponible | ✅ Completo | Se muestra la propiedad `Quantity` en las vistas y búsquedas. |
| Evitar cantidades negativas | ✅ Completo | El servicio valida la cantidad al agregar y actualizar productos. |
| Utilizar listas genéricas (`List<T>`) | ✅ Completo | Se usan listas de `ProductModel` en el servicio, repositorio y contexto. |
| Persistir la lógica en métodos reutilizables | ✅ Completo | La lógica está separada entre el servicio y el repositorio. |

### Catálogo de productos

| Requisito | Estado | Observación |
|---|---|---|
| Crear una jerarquía de productos utilizando herencia | ❌ Pendiente | No se encontró una jerarquía de productos implementada. |
| Implementar interfaces para definir comportamientos comunes | ❌ Pendiente | No se encontraron interfaces para los productos. |
| Mostrar catálogos filtrados con LINQ | ⚠️ Parcial | Existe búsqueda por nombre, pero no un flujo de catálogo con los filtros requeridos. |
| Ordenar productos por precio y nombre | ❌ Pendiente | No se encontró ordenamiento por esos criterios. |
| Aplicar polimorfismo en la presentación de productos | ❌ Pendiente | La presentación usa directamente las propiedades de `ProductModel`. |
| Implementar operaciones asincrónicas simuladas con `async/await` | ❌ Pendiente | Hay métodos asíncronos en el CRUD, pero las operaciones del repositorio son síncronas y no simulan I/O asíncrono. |
| Manejar errores de carga o selección de productos | ⚠️ Parcial | El menú maneja opciones inválidas y productos no encontrados; los errores al leer o cargar el archivo no se gestionan en el menú. |

### Verificación

- Compilación de `InventoryManagement.csproj`: correcta, con cero advertencias y cero errores.
- No se ejecutaron pruebas automatizadas.
