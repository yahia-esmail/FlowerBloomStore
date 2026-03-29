
using FlowerBloomStore.Application.Interfaces.Orders;
using FlowerBloomStore.Domain.Enums;


namespace FlowerBloomStore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: /Admin/Order
        public async Task<IActionResult> Index(
            OrderStatus? status = null,
            string? userId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int page = 1)
        {
            const int pageSize = 5;

            var orders = await _orderService.GetFilteredOrdersAsync(
                status: status,
                userId: userId,
                fromDate: fromDate,
                toDate: toDate,
                pageNumber: page,
                pageSize: pageSize);

            //   View (Pagination + Filters)
            var totalOrdersCount = await _orderService.GetFilteredOrdersCountAsync(status, userId, fromDate, toDate);
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalOrdersCount / pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.Status = status;
            ViewBag.UserId = userId;
            ViewBag.FromDate = fromDate?.ToString("yyyy-MM-dd");
            ViewBag.ToDate = toDate?.ToString("yyyy-MM-dd");

            return View(orders);
        }

        // GET: /Admin/Order/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderService.GetOrderDetailsAsync(id);
            if (order == null)
                return NotFound();

            return View(order);
        }

        // POST: /Admin/Order/UpdateStatus/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, OrderStatus newStatus)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null)
                return NotFound();

            order.Status = newStatus;

            if (newStatus == OrderStatus.Delivered)
            {
                order.DeliveryDate = DateTime.UtcNow;
            }

            await _orderService.UpdateAsync(order);

            TempData["Success"] = $"تم تغيير حالة الطلب رقم {id} إلى {newStatus}";
            string returnUrl = Request.Headers["Referer"].ToString();

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Admin/Order/Pending
        public async Task<IActionResult> Pending(int page = 1)
        {
            const int pageSize = 10;

            var pendingOrders = await _orderService.GetOrdersByStatusAsync(OrderStatus.Pending);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)await _orderService.GetOrdersCountByStatusAsync(OrderStatus.Pending) / pageSize);

            return View("Index", pendingOrders);
        }

        // GET: /Admin/Order/Processing
        public async Task<IActionResult> Processing(int page = 1)
        {
            const int pageSize = 10;

            var processingOrders = await _orderService.GetOrdersByStatusAsync(OrderStatus.Processing);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)await _orderService.GetOrdersCountByStatusAsync(OrderStatus.Processing) / pageSize);

            return View("Index", processingOrders);
        }

        // GET: /Admin/Order/Delivered
        public async Task<IActionResult> Delivered(int page = 1)
        {
            const int pageSize = 10;

            var deliveredOrders = await _orderService.GetOrdersByStatusAsync(OrderStatus.Delivered);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)await _orderService.GetOrdersCountByStatusAsync(OrderStatus.Delivered) / pageSize);

            return View("Index", deliveredOrders);
        }

        // GET: /Admin/Order/Cancelled
        public async Task<IActionResult> Cancelled(int page = 1)
        {
            const int pageSize = 10;

            var cancelledOrders = await _orderService.GetOrdersByStatusAsync(OrderStatus.Cancelled);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)await _orderService.GetOrdersCountByStatusAsync(OrderStatus.Cancelled) / pageSize);

            return View("Index", cancelledOrders);
        }
    }
}