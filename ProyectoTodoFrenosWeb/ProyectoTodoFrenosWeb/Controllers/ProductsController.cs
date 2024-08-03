using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using ProyectoTodoFrenosWeb.ConsumoServices;
using System.Security.Claims;

namespace ProyectoTodoFrenosWeb.Controllers
{
    public class ProductsController : Controller
    {
        private readonly TodoFrenosDbContext _context;

        ProductService productService;
        ShoppingCartService shoppingCartService;

        public ProductsController(TodoFrenosDbContext context, IConfiguration config)
        {
            _context = context;
            productService = new ProductService(config);
            shoppingCartService = new ShoppingCartService(config);
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var productos = await productService.GetProduct();
            return View(productos);

            /*var todoFrenosDbContext = _context.Products.Include(p => p.Category);
            return View(await todoFrenosDbContext.ToListAsync());*/
        }
        // GET: Products
        public async Task<IActionResult> IndexCliente()
        {
            var productos = await productService.GetProduct();
            return View(productos);

        }

        // GET: Products/Details/5
        [Route("/Producto/Detalles/{id}")]
        public async Task<IActionResult> Details(long? id)
        {
            Product producto = await productService.GetProduct(id);

            if (id == null)
            {
                return NotFound();
            }

            /*var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }*/

            return View(producto);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
           
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile imagen)
        {
            if (ModelState.IsValid)
            {
                // Iformefile imagen
                //Validar imagen
                byte[]? imagenMas = null;

                if (imagen != null && imagen.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await imagen.CopyToAsync(memoryStream);
                        imagenMas = memoryStream.ToArray();
                    }
                }
                //Configuracion de la imagen
                product.ImageProduct = imagenMas;

                var resultado = await productService.CreateProduct(product);

                if (resultado != null)
                {
                    TempData["MenasajeExito"] = "Se guardo el producto exitosamente";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(product);
                }

               /* _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));*/
            }
            /*ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            */
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product product = await productService.GetProduct(id);

           /* var product = await _context.Products.FindAsync(id);*/

            if (product == null)
            {
                return NotFound();
            }
           /* ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            */
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Product product, IFormFile imagen)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Iformefile imagen
                //Validar imagen
                byte[]? imagenMas = null;

                if (imagen != null && imagen.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await imagen.CopyToAsync(memoryStream);
                        imagenMas = memoryStream.ToArray();
                    }
                }
                else
                {
                    imagenMas = product.ImageProduct;
                }

                try
                {
                    //Configuracion de la imagen
                    product.ImageProduct = imagenMas;
                    var resultado = await productService.EditProduct((long)id, product);

                    if (resultado != null)
                    {
                        TempData["MenasajeExito"] = "Se edito el producto exitosamente";
                        return RedirectToAction(nameof(Index));
                    }

                   /* _context.Update(product);
                    await _context.SaveChangesAsync();*/
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                /*return RedirectToAction(nameof(Index));*/
            }
            /*ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            */
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultado = await productService.DeleteProduct(id.Value);

           /* var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);*/

            if (resultado)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "Error al inactivar el vehiculo.");
            var product = await productService.GetProduct(id);

            
            return View(product);
        }

        // GET: Products/Activate/5
        public async Task<IActionResult> Activate(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultado = await productService.ActivarProduct(id.Value);

            /* var product = await _context.Products
                 .Include(p => p.Category)
                 .FirstOrDefaultAsync(m => m.ProductId == id);*/

            if (resultado)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "Error al activar el vehiculo.");
            var product = await productService.GetProduct(id);


            return View(product);
        }

        public async Task<IActionResult> AddToCart(long productId, int quantity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Obtener el UserId del usuario logueado
            var message = await shoppingCartService.AddToCart(userId, productId, quantity);
            TempData["Message"] = message; // Mostrar el mensaje en la vista actual
            return RedirectToAction("IndexCliente");
        }

        private bool ProductExists(long id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
