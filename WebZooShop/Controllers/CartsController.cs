using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpPost]
        [Route("delete")]

        public async Task<IActionResult> Delete([FromBody] int id)
        {
            try
            {
                string userName = User.Claims.FirstOrDefault().Value;
                //string userName = User.FindFirst("UserName")?.Value;
                var user = await _userManager.FindByEmailAsync(userName);
                var cart = _context.Carts
                    .SingleOrDefault(x => x.UserId == user.Id && x.ProductId == id);
                if (cart == null)
                {
                    return BadRequest(new { message = "Check id!" });
                }
                
               
                _context.Carts.Remove(cart);
            _context.SaveChanges();
            return Ok(new { message = "Item of cart deleted" });

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
        [Route("deleteCart")]
        
        public async Task<IActionResult> DeleteCart()
        {

            try
            {
                string userName = User.Claims.FirstOrDefault().Value;
                var user = await _userManager.FindByEmailAsync(userName);
                var cart = _context.Carts
                    .SingleOrDefault(x => x.UserId == user.Id );
                if (cart == null)
                {
                    return BadRequest(new { message = "Check id!" });
                }


                _context.Carts.Remove(cart);
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
                var cart = _context.Carts
                    .SingleOrDefault(x => x.Id == id);
                if (cart != null)
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
                var cart = _context.Carts
                    .SingleOrDefault(x => x.Id == id);
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
    }
}

