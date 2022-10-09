using AutoMapper;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebZooShop.Data;
using WebZooShop.Data.Entities;
using WebZooShop.Data.Entities.Identity;
using static WebZooShop.Model.CartViewModels;

namespace WebZooShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartsController : ControllerBase
    {
        private readonly AppEFContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public CartsController(AppEFContext context,
           IMapper mapper, UserManager<AppUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] CartAddViewModel model)
        {
            try
            {
                string userName = User.Claims.FirstOrDefault().Value;
                //string userName = User.FindFirst("UserName")?.Value;
                var user = await _userManager.FindByEmailAsync(userName);
                var cart = _context.Carts
                    .SingleOrDefault(x => x.UserId == user.Id && x.ProductId == model.ProductId);
                if (cart == null)
                {
                    cart = _mapper.Map<CartEntity>(model);
                    cart.UserId = user.Id;
                    _context.Carts.Add(cart);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    cart.Quantity += model.Quantity;
                    _context.SaveChanges();
                }

                var result = _context.Carts
                    .Include(x => x.Product)
                    .Where(x => x.Id == cart.Id)
                    .Select(x => _mapper.Map<CartItemViewModel>(x))
                    .First();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    invalid = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> List()
        {
            try
            {
                Thread.Sleep(2000);
                string userName = User.Claims.FirstOrDefault().Value;
                //string userName = User.FindFirst("name")?.Value;
                var user = await _userManager.FindByNameAsync(userName);
                var model = await _context.Carts
                    .Where(x => x.UserId == user.Id)
                    .Include(x => x.Product)
                    .Select(x => _mapper.Map<CartItemViewModel>(x)).ToListAsync();
                if(model!= null)
                return Ok(model);
                else
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

        [HttpPost]
        [Route("delete")]

        public async Task<IActionResult> Delete([FromBody] int id)
        {

            try
            {
                string userName = User.Claims.FirstOrDefault().Value;
                //var user =  _userManager.FindByEmailAsync(userName);
                var cart = _context.Carts
                    .SingleOrDefault(x => x.User.Email == userName && x.ProductId == id);
                if (cart == null)
                {
                    return BadRequest(new { message = "Check id!" });
                }
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Item of cart deleted" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { invalid = "Something went wrong on server " + ex.Message });
            }

                                   
        }

        [HttpPost]
        [Route("deleteCart")]
        
        public async Task<IActionResult> DeleteCart()
        {

            try
            {
                string userName = User.Claims.FirstOrDefault().Value;
                var user = await _userManager.FindByEmailAsync(userName);
                var carts = _context.Carts
                    .Where(x => x.UserId == user.Id ).ToList();
                if (carts.Count()==0)
                {
                    return BadRequest(new { message = "Check id!" });
                }


                _context.Carts.RemoveRange(carts);
                _context.SaveChanges();
                return Ok(new { message = "Cart deleted" });

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    invalid = ex.Message
                });
            }
        }


        [HttpPost]
        [Route("quantityPlus")]
        public IActionResult QuantityPlus([FromBody] int id)
        {
            try
            {
                string userName = User.Claims.FirstOrDefault().Value;
                //var user =  _userManager.FindByEmailAsync(userName);
                var cart = _context.Carts
                    .SingleOrDefault(x => x.User.Email == userName && x.ProductId == id);
                var prod = _context.Products.SingleOrDefault(x => x.Id == id);
                if (cart != null && cart.Quantity < prod.Quantity)
                {
                    cart.Quantity = cart.Quantity+1;
                    _context.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    invalid = ex.Message
                });
            }
        }

        [HttpPost]
        [Route("quantityMinus")]
        public IActionResult QuantityMinus([FromBody] int id)
        {
            try
            {
                string userName = User.Claims.FirstOrDefault().Value;
                //var user =  _userManager.FindByEmailAsync(userName);
                var cart = _context.Carts
                    .SingleOrDefault(x => x.User.Email == userName && x.ProductId == id);
                if (cart != null && cart.Quantity>0)
                {
                    cart.Quantity = cart.Quantity - 1;
                    _context.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    invalid = ex.Message
                });
            }
        }

       /* public ActionResult GetListUsers()
        {
            DataTable dt = getData(siteId);
            //Name of File  
            var domain = Request.Host.Host;
            string fileName = $"{domain}.xlsx";
            using (XLWorkbook wb = new XLWorkbook())
            {
                //Add DataTable in worksheet  
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    //Return xlsx Excel File  
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
        }*/
    }
}

