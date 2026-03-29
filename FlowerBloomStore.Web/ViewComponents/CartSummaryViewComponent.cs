
public class CartSummaryViewComponent : ViewComponent
{
    private readonly ICartService _cartService;

    public CartSummaryViewComponent(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        // Fix: Cast User to ClaimsPrincipal to access FindFirstValue extension method
        var userId = (User as ClaimsPrincipal)?.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return View(0);
        }

        var cart = await _cartService.GetUserCartWithItemsAsync(userId);
        var count = cart?.Items?.Count() ?? 0;

        return View(count);
    }
}