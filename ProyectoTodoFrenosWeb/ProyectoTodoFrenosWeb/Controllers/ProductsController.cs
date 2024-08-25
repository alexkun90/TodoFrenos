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
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var productos = await productService.GetProduct();
            return View(productos);

            /*var todoFrenosDbContext = _context.Products.Include(p => p.Category);
            return View(await todoFrenosDbContext.ToListAsync());*/
        }
        // GET: Products
        [Authorize(Roles = "User")]
        public async Task<IActionResult> IndexCliente()
        {
            var productos = await productService.GetProduct();
            var priceWithTax = new Dictionary<long, decimal?>();
            foreach(var i in productos)
            {
                priceWithTax.Add(i.ProductId, i.Price * 0.13m);//esa m al final indica que es decimal el 0.13
            }
            ViewBag.PriceWithTaxes = priceWithTax;
            return View(productos);

        }

        // GET: Products/Details/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
           
            return View();
        }

        // POST: Products/Create
        [Authorize(Roles = "Admin")]
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
            }
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product product = await productService.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            IFormFile imagenFormFile = null;
            if (product.ImageProduct != null)
            {
                var stream = new MemoryStream(product.ImageProduct);
                imagenFormFile = new FormFile(stream, 0, stream.Length, "imagen", "imagen.jpg")
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/jpeg"
                };
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewData["ImagenFormFile"] = imagenFormFile;
            return View(product);
        }

        // POST: Products/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Product product, IFormFile? imagen)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            // Aquí validamos manualmente la imagen para que no afecte el ModelState
            if (imagen != null && imagen.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imagen.CopyToAsync(memoryStream);
                    product.ImageProduct = memoryStream.ToArray();
                }
            }
            else
            {
                // Aquí conservamos la imagen existente del producto en caso de no seleccionar una nueva
                var existingProduct = await productService.GetProduct(id);
                product.ImageProduct = existingProduct?.ImageProduct;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Aquí editamos el producto usando el servicio.
                    var resultado = await productService.EditProduct((long)id, product);

                    if (resultado != null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
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
            }

            // Reenviar la lista de categorías en caso de error
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);

            return View(product);
        }



        // GET: Products/Delete/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin,User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(long productId, int quantity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var message = await shoppingCartService.AddToCart(userId, productId, quantity);
            TempData["Message"] = message; 
            return RedirectToAction("IndexCliente");
        }

        private bool ProductExists(long id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
