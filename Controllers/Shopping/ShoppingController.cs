using Dashboard.Data;
using Dashboard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;

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
        public IActionResult ProductDetails(int id)
        {
            var productDetails = _context.ProductDetail.Where(p => p.Id == id).ToList();
            return View(productDetails);
        }

        [Authorize]
        public IActionResult Checkout(int id)
        {
            var user = HttpContext.User.Identity.Name;
            var productDetails = _context.ProductDetail.SingleOrDefault(p => p.Id == id);
            var cart = new Cart()
            {
                CustomerId = user,
                MyProductId = productDetails.ProductId,
                Color = productDetails.Color,
                Image = productDetails.Image,
                Price = productDetails.Price,
                ProductName = productDetails.ProductName,
                Tax = 0.15,
                Total = (productDetails.Price * (15 / 100) + productDetails.Price),
                Qty = productDetails.Qty

            };

            _context.Carts.Add(cart);
            _context.SaveChanges();
            return View(cart);
        }

        public IActionResult ConShopping()
        {
            return RedirectToAction("Index");
        }
        public IActionResult Invoice(int id)
        {
            var user = HttpContext.User.Identity.Name;
            var productDetails = _context.ProductDetail.SingleOrDefault(p => p.Id == id);
            var invoice = new Invoice()
            {
                CostumerId = user,
                ProductId = productDetails.ProductId,
                Price = productDetails.Price,
                ProductName = productDetails.ProductName,
                Total = (productDetails.Price * (15 / 100) + productDetails.Price),
                QTY = productDetails.Qty

            };

            _context.Invoices.Add(invoice);
            _context.SaveChanges();
            return View(invoice);
        }
        public async Task<string> SendMail()
        {
            //nkgwwsxcqrzinbup

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("TestMessage", "i.sooomx@gmail.com"));
            message.To.Add(MailboxAddress.Parse("AsmaaAlharbii@hotmail.com"));
            message.Subject = "test email from asp.net ";
            message.Body = new TextPart("plain")
            {
                Text = "Welcome Asmaa"
            };
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("smtp.gmail.com", 587);
                    client.Authenticate("i.sooomx@gmail.com", "nkgwwsxcqrzinbup");
                    await client.SendAsync(message);
                    client.Disconnect(true);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message.ToString());
                }



            };

            return "ok"; // Invoice

        }

    }
}
