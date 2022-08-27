using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebZooShop.Data;
using WebZooShop.Data.Entities;
using WebZooShop.Data.Entities.Identity;
using WebZooShop.Model;

namespace WebZooShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppEFContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public OrdersController(AppEFContext context,
            UserManager<AppUser> userManager,
           IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        /// <summary>
        /// Список статусів замовленя
        /// </summary>
        /// <returns>Повертає лист</returns>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">List status</response>
        /// <response code="400">List status has missing/invalid values</response>
        /// <response code="500">Oops! Can't get  status list right now</response>

        [HttpGet]
        [Route("status")]
        public async Task<IActionResult> StatusList()
        {
            try
            {
                //Thread.Sleep(2000);
                var model = await _context.OrderStatuses
                    .Select(x => _mapper.Map<OrderStatusItemViewModel>(x))
                    .ToListAsync();
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
        /// Додати замовленя
        /// </summary>
        /// <param name="model">Понель із даними</param>
        /// <returns>Повертає ok</returns>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">Add order</response>
        /// <response code="400">Add order has missing/invalid values</response>
        /// <response code="500">Oops! Can't add order right now</response>

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] OrderAddViewModel model)
        {
            try
            {
               
                string userName = User.Claims.FirstOrDefault().Value;
                var user = await _userManager.FindByEmailAsync(userName);

                var entity = _mapper.Map<OrderEntity>(model);
                entity.UserId = user.Id;
                _context.Orders.Add(entity);
                _context.SaveChanges();

                var entityItems = model.OrderItems.Select(x => _mapper.Map<OrderItemEntity>(x));
                foreach (var item in entityItems)
                {
                    item.OrderId = entity.Id;
                    _context.OrderItems.Add(item);
                   
                }
                 _context.SaveChanges();

                var cartData = _context.Carts.Where(x => x.UserId == user.Id).ToArray();
                _context.Carts.RemoveRange(cartData);
                _context.SaveChanges();
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
        /// Список  замовлень
        /// </summary>
        /// <returns>Повертає лист замовлень</returns>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">List orders</response>
        /// <response code="400">List orders has missing/invalid values</response>
        /// <response code="500">Oops! Can't get  orders list right now</response>

        [HttpGet]
        [Route("list")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> List()
        {
            try
            {
               
                var model = await _context.Orders
                    .Include(x => x.OrderItems)
                    .ThenInclude(x => x.Product)
                    //.ThenInclude(x => x.ProductImages)
                    .Include(x => x.OrderStatus)
                    .OrderByDescending(x => x.DateCreated)
                    .Select(x => _mapper.Map<OrderViewModel>(x))
                    .ToListAsync();
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
        /// Список позицій в замовлені
        /// </summary>
        /// <returns>Повертає лист </returns>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">List items in order</response>
        /// <response code="400">List has missing/invalid values</response>
        /// <response code="500">Oops! Can't get list right now</response>

        [HttpPost]
        [Route("listItemOrder")]
        public IActionResult ListItemOrder([FromBody] OrderListItemsViewModel model)
        {
            try
            {
                var list = _context.OrderItems
                    .Where(x => x.OrderId == model.Id)
                    .Select(x => _mapper.Map<OrderItemViewModel>(x));
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

        /// <summary>
        ///Зміна статусу замовленя
        /// </summary>
        /// <param name="model">Понель із даними</param>
        /// <returns>Повертає ok</returns>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">Change order</response>
        /// <response code="400">Change order has missing/invalid values</response>
        /// <response code="500">Oops! Can't Change order right now</response>

        [HttpPost]
        [Route("changeStatus")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> ChangeStatus([FromBody] OrderChangeStatusViewModel model)
        {
            try
            {
               var order = await _context.Orders.SingleOrDefaultAsync(x => x.Id == model.Id);
                order.StatusId = model.StatusId;
                _context.SaveChanges();
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

        /*[HttpGet]
        [Route("user/list")]
        public async Task<IActionResult> UserList()
        {
            try
            {
                string userName = User.Claims.FirstOrDefault().Value;
                var user = await _userManager.FindByEmailAsync(userName);
                //Thread.Sleep(2000);
                var model = await _context.Orders
                    .Include(x => x.OrderItems)
                    .ThenInclude(x => x.Product)
                    //.ThenInclude(x => x.ProductImages)
                    .Include(x => x.OrderStatus)
                    .Where(x => x.UserId == user.Id)
                    .OrderByDescending(x => x.DateCreated)
                    .Select(x => _mapper.Map<OrderViewModel>(x))
                    .ToListAsync();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    invalid = ex.Message
                });
            }
        }*/
    }
}
