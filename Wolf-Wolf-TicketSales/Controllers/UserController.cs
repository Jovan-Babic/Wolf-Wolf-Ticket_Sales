using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Wolf_Wolf_TicketSales.Models;
using Wolf_Wolf_TicketSales.Services;

namespace Wolf_Wolf_TicketSales.Controllers
{
    [Authorize(Roles = "USER")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userService.GetUserByUsernameAsync(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value.ToString());

            var ticketsOwned = await _userService.GetTicketsAsync(user.Id);

            var concerts = _userService.OfferConcertTickets();
            
            return View(new UserTicketsWithOffersModel {UserTickets = ticketsOwned, ConcertOffers = concerts });
        }
               
        public async Task<IActionResult> BuyTickets(int concertId)
        {
            var concert = await _userService.GetConcertInfoAsync(concertId);

            return View(concert);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BuyTickets(IFormCollection collection)
        {
            int concertId = Convert.ToInt32(collection["concertId"]);
            try
            {
                int userId = (await _userService.GetUserByUsernameAsync(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value.ToString())).Id;
                                
                int tickets = Convert.ToInt32(collection["tickets"]);

                await _userService.BuyTicketsAsync(userId, concertId, tickets);

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                TempData["BuyError"] = $"{ex.GetBaseException().Message}";
                var concert = await _userService.GetConcertInfoAsync(concertId);
                return View(concert);
            }
        }
    }
}
