using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Wolf_Wolf_TicketSales.Models;
using Wolf_Wolf_TicketSales.Services;

namespace Wolf_Wolf_TicketSales.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        
        public IActionResult Index()
        {
            var concerts = _adminService.GetConcertsInfo();
            return View(concerts);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection collection)
        {
            try
            {
                if (collection.Keys.Count > 0)
                {
                    var model = new ConcertCreateModel()
                    {
                        Location = collection["Location"].ToString(),
                        Name = collection["Name"].ToString(),
                        Tickets = Convert.ToInt32(collection["Tickets"])
                    };
                    await _adminService.CreateConcertAsync(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }       
    }
}
