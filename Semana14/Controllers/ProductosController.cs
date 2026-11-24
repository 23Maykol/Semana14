
using Microsoft.AspNetCore.Mvc;
using Semana14.Models;

namespace Semana14.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private static readonly List<Producto> Productos = new()
    {
        new Producto { Id = 1, Nombre = "Laptop", Precio = 2500, Stock = 10 },
        new Producto { Id = 2, Nombre = "Mouse", Precio = 50, Stock = 25 },
        new Producto { Id = 3, Nombre = "Teclado", Precio = 120, Stock = 15 }
    };

    private static readonly object Candado = new();

    // GET: api/productos
    [HttpGet]
    public IActionResult GetAll()
    {
        lock (Candado)
        {
            return Ok(Productos.ToList());
        }
    }

    // GET: api/productos/1
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        lock (Candado)
        {
            var producto = Productos.FirstOrDefault(p => p.Id == id);

            if (producto == null)
                return NotFound(new { mensaje = "Producto no encontrado" });

            return Ok(producto);
        }
    }

    // POST: api/productos
    [HttpPost]
    public IActionResult Create([FromBody] Producto producto)
    {
        lock (Candado)
        {
            if (string.IsNullOrWhiteSpace(producto.Nombre) ||
                producto.Precio < 0 || producto.Stock < 0)
            {
                return BadRequest(new { mensaje = "Datos del producto no válidos" });
            }

            producto.Id = Productos.Count == 0
                ? 1
                : Productos.Max(p => p.Id) + 1;

            Productos.Add(producto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = producto.Id },
                producto
            );
        }
    }

    // PUT: api/productos/1
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] Producto datos)
    {
        lock (Candado)
        {
            var producto = Productos.FirstOrDefault(p => p.Id == id);

            if (producto == null)
                return NotFound(new { mensaje = "Producto no encontrado" });

            if (string.IsNullOrWhiteSpace(datos.Nombre) ||
                datos.Precio < 0 || datos.Stock < 0)
            {
                return BadRequest(new { mensaje = "Datos del producto no válidos" });
            }

            producto.Nombre = datos.Nombre;
            producto.Precio = datos.Precio;
            producto.Stock = datos.Stock;

            return Ok(producto);
        }
    }

    // DELETE: api/productos/1
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        lock (Candado)
        {
            var producto = Productos.FirstOrDefault(p => p.Id == id);

            if (producto == null)
                return NotFound(new { mensaje = "Producto no encontrado" });

            Productos.Remove(producto);

            return Ok(new { mensaje = "Producto eliminado correctamente" });
        }
    }
}
