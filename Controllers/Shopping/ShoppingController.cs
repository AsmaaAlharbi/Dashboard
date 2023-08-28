using Dashboard.Data;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Controllers.Shopping
{
    public class ShoppingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShoppingController(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
            var productDetail = _context.ProductDetail.ToList();
            return View(productDetail);
        }
    }
}
