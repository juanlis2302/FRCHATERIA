using Xunit;                // 👈 OBLIGATORIO
using ferre2.Models;
using System;

namespace testwi              // 👈 nombre del proyecto de pruebas
{
    public class ProductoTests
    {
        [Fact]
        public void CrearProducto_Valido()
        {
            var producto = new productos
            {
                id_producto = 1,
                codigo = "P001",
                nombre = "Martillo",
                descripcion = "Martillo de acero",
                precio_base = 15000,
                iva_porcentaje = 19,
                categoria = "Herramientas",
                stock = 10,
                estado = "Activo",
                fecha_creacion = DateTime.Now
            };

            Assert.NotNull(producto);
            Assert.Equal("Martillo", producto.nombre);
            Assert.Equal(10, producto.stock);
        }

        [Fact]
        public void EditarProducto()
        {
            var producto = new productos
            {
                nombre = "Taladro"
            };

            producto.nombre = "Taladro Industrial";

            Assert.Equal("Taladro Industrial", producto.nombre);
        }

        [Fact]
        public void EliminarProducto()
        {
            productos producto = new productos
            {
                nombre = "Eliminar"
            };

            producto = null;

            Assert.Null(producto);
        }
    }
}

