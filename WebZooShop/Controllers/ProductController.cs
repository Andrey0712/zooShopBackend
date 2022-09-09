using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using WebZooShop.Data;
using WebZooShop.Data.Entities;
using WebZooShop.Model;

namespace WebZooShop.Controllers
{
   

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppEFContext _context;
        public ProductController(IMapper mapper, AppEFContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// Список продуктів
        /// </summary>
        /// <returns>Повертає лист</returns>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">List products</response>
        /// <response code="400">List products has missing/invalid values</response>
        /// <response code="500">Oops! Can't get  products list right now</response>

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> List()
        {
            try
            {
                Thread.Sleep(2000);
                var model = await _context.Products
                     .Include(x => x.Category)
                     .Include(x => x.InventoryStatus)
                    .Select(x => _mapper.Map<ProductItemViewModel>(x)).ToListAsync();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    invalid = ex.Message
                });
            }
        }
        /// <summary>
        /// Реквізіти
        /// </summary>
        /// <returns>Повертає фаил</returns>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">Реквізіти для оплати</response>
        /// <response code="400">Has missing/invalid values</response>
        /// <response code="500">Oops! Can't get list right now</response>

        /*[HttpGet]
        [Route("getfile/{name}")]
        public IActionResult DownloadFile([FromRoute] string name)
        {
            //Зробити перевірку доступа

            //Determine the Content Type of the File.
            string contentType = "";
            string file = Directory.GetCurrentDirectory() +
                $"/dataRahunok/{name}";

            new FileExtensionContentTypeProvider()
                .TryGetContentType(file, out contentType);

            return new PhysicalFileResult(file, contentType);
        }*/

        [HttpGet]
        [Route("details_for_payment")]
        public IActionResult DounloudFile()
        {
            string contentType = "";
            string file = Directory.GetCurrentDirectory() +
                "/dataRahunok/details_for_payment.pdf";
            new FileExtensionContentTypeProvider()
                .TryGetContentType(file, out contentType);

            return new PhysicalFileResult(file, contentType);
        }

        /// <summary>
        /// Список продуктів по категоріям
        /// </summary>
        /// <returns>Повертає лист по категоріям</returns>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">List products</response>
        /// <response code="400">List products has missing/invalid values</response>
        /// <response code="500">Oops! Can't get  products list right now</response>

        [HttpGet]
        [Route("listByCatecory")]
        //public async Task<IActionResult> ListByCatecory([FromBody] SearchByCategoryModel model)
        public async Task<IActionResult> ListByCatecory(string name)
        {
             if (name != "null")
            {
              try
            {                   
                     //Thread.Sleep(2000);
                var list = await _context.Products
                    .Include(c => c.Category)
                      .Include(x => x.InventoryStatus)
                      .Where(x => x.Category.Name == name)
                    .Select(x => _mapper.Map<ProductItemViewModel>(x)).ToListAsync();
                return Ok(list);
                    
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    invalid = ex.Message
                });
            }
            }
            else
            {
                try
                {
                    Thread.Sleep(2000);
                    var model = await _context.Products
                         .Include(x => x.Category)
                         .Include(x => x.InventoryStatus)
                        .Select(x => _mapper.Map<ProductItemViewModel>(x)).ToListAsync();
                    return Ok(model);
                }
                catch (Exception ex)
                {
                    return BadRequest(new
                    {
                        invalid = ex.Message
                    });
                }
            }
           
        }

        /// <summary>
        /// Список продуктів по пошуку
        /// </summary>
        /// <returns>Повертає лист по запиту</returns>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">List products</response>
        /// <response code="400">List products has missing/invalid values</response>
        /// <response code="500">Oops! Can't get  products list right now</response>
        [HttpGet("listBySearch")]
        public async Task<IActionResult> ListBySearch(string name)
        {
            try
            {
                Thread.Sleep(2000);
                var query = _context.Products
                     .Include(c => c.Category)
                      .Include(x => x.InventoryStatus)
                             .AsQueryable();
                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(x => x.Name.ToLower().Contains(name.ToLower()));
                }
                var data = await query

                    .Select(x => _mapper.Map<ProductItemViewModel>(x)).ToListAsync();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    invalid = ex.Message
                });
            }
        }

        /// <summary>
        /// Продукт по ID
        /// </summary>
        /// <param name="id">Понель із даними</param>
        /// <returns>Повертає родукт по id</returns>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">Get product</response>
        /// <response code="400">Get product has missing/invalid values</response>
        /// <response code="500">Oops! Can't get  product  right now</response>

        [HttpGet]
        [Route("get/{id}")]
        public IActionResult GetData(int id)
        {
            var cultureInfo = new CultureInfo("uk-UA");
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            ProductViewModelImages model = new ProductViewModelImages
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                StartPhoto = product.StartPhoto,
                DateCreate = product.DateCreated.ToString("dd.MM.yyyy HH:mm:ss"),

            };
            return Ok(model);
        }

        /// <summary>
        /// Видалення продукту
        /// </summary>
        /// <param name="id">Понель із даними</param>
        /// <returns>Повертає повідомлення</returns>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">Delete product</response>
        /// <response code="400">Delete product has missing/invalid values</response>
        /// <response code="500">Oops! Can't delete product now</response>

        [HttpPost]
        [Route("delete")]
        //public IActionResult Delete([FromBody] ProductItemViewModel model)
            public IActionResult Delete([FromBody] int id)
            
        {
            
            var res = _context.Products.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return BadRequest(new { message = "Check id!" });
            }

            _context.Products.Remove(res);
            _context.SaveChanges();
            return Ok(new { message = "Product deleted" });
        }

        /// <summary>
        /// Додати продукт
        /// </summary>
        /// <param name="model">Понель із даними</param>
        /// <returns>Повертає ok</returns>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">Add product</response>
        /// <response code="400">Add product has missing/invalid values</response>
        /// <response code="500">Oops! Can't add product right now</response>

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromForm] ProductAddViewModel model)
        {
            try
            {
                /* List<string> fileNames = new List<string>();
                 foreach (var item in model.Images)
                 {
                     string fileName = "";
                     if (item != null)
                     {
                         var ext = Path.GetExtension(item.FileName);
                         fileName = Path.GetRandomFileName() + ext;
                         var filePath = Path.Combine(Directory.GetCurrentDirectory(),
                             "uploads", fileName);
                         using (var stream = System.IO.File.Create(filePath))
                         {
                             item.CopyTo(stream);
                         }
                         fileNames.Add(fileName);
                     }
                 }*/

                string startFoto = String.Empty;
                var product = _mapper.Map<ProductEntity>(model);

                if (model.StartPhoto != null)
                {
                    string randomFilename = Path.GetRandomFileName() +
                        Path.GetExtension(model.StartPhoto.FileName);

                    string dirPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
                    startFoto = Path.Combine(dirPath, randomFilename);
                    using (var file = System.IO.File.Create(startFoto))
                    {
                        model.StartPhoto.CopyTo(file);
                    }
                    product.StartPhoto = randomFilename;
                }
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                /*foreach (var img in fileNames)
                {
                    ProductImageEntity productImage = new ProductImageEntity()
                    {
                        Name = img,
                        ProductId = product.Id
                    };
                    _context.ProductImages.Add(productImage);
                    _context.SaveChanges();
                }*/
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    invalid = ex.Message
                });
            }
        }

        /// <summary>
        /// Редагування продукту
        /// </summary>
        /// <param name="model">Понель із даними</param>
        /// <returns>Повертає message</returns>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">Edit product</response>
        /// <response code="400">Edit product has missing/invalid values</response>
        /// <response code="500">Oops! Can't edit product now</response>

        [HttpPost]
        [Route("editProduct")]
        public IActionResult Edit([FromForm] ProductToEdit model)
        {


            if (ModelState.IsValid)
            {
                var itemProd = _context.Products
                     .Include(x => x.Category)
                     .Include(x => x.InventoryStatus)
                    .FirstOrDefault(x => x.Id == model.Id);
                string dirPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

                itemProd.Id = model.Id;
                if (model.Name != null)
                {
                    itemProd.Name = model.Name;
                }
                if (model.Description != null)
                {
                    itemProd.Description = model.Description;
                }
                if (model.Price != 0)
                {
                    itemProd.Price = model.Price;
                }
                if (model.InventoryStatusId != 0)
                {
                    itemProd.InventoryStatusId = model.InventoryStatusId;
                }
                if (model.CategoryId != 0)
                {
                    itemProd.CategoryId = model.CategoryId;
                }
                if (model.Rating != 0)
                {
                    itemProd.Rating = model.Rating;
                }
                if (model.Quantity != 0)
                {
                    itemProd.Quantity = model.Quantity;
                }
                else
                {
                    itemProd.Quantity = 0;
                    itemProd.InventoryStatusId = 2;
                }
                if (model.StartPhoto != null)
                {
                    string randomFilename = Path.GetRandomFileName() +
                        Path.GetExtension(model.StartPhoto.FileName);

                    var startFoto = Path.Combine(dirPath, randomFilename);
                    using (var file = System.IO.File.Create(startFoto))
                    {
                        model.StartPhoto.CopyTo(file);
                    }
                    itemProd.StartPhoto = randomFilename;
                }

                /* //видаляємо сторі фотки
                 if (model.deletedImages != null)
                 {
                     foreach (var delProduct in model.deletedImages)
                     {
                         var delProductImage = itemProd.ProductImages.SingleOrDefault(x => delProduct.Contains(x.Name));
                         string imgPath = Path.Combine(dirPath, delProductImage.Name);
                         if (System.IO.File.Exists(imgPath))
                         {
                             System.IO.File.Delete(imgPath);
                         }
                         _context.ProductImages.Remove(delProductImage);
                     }
                 }
                 //Додати нові фотки
                 if (model.Images != null)
                 {
                     foreach (var newImages in model.Images)
                     {
                         string ext = Path.GetExtension(newImages.FileName);
                         string fileName = Path.GetRandomFileName() + ext;

                         string filePath = Path.Combine(dirPath, fileName);
                         using (var stream = System.IO.File.Create(filePath))
                         {
                             newImages.CopyTo(stream);
                         }

                         _context.ProductImages.Add(new Data.Entities.ProductImageEntity
                         {
                             Name = fileName,
                             ProductId = itemProd.Id
                         });
                     }
                 }*/

                _context.SaveChanges();

            }
            //return Ok(model);
            return Ok(new { message = "ok edit" });

        }

    }

       
    
}

