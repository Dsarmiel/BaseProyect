using First_Proyect.Extensions;
using First_Proyect.Interfaces;
using First_Proyect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace First_Proyect.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase {
        private readonly UserManager<AppUser> _userManager;
        private readonly IPortfolioRepository _portfolioRepo;
        private readonly IStockRepostory _stockRepo;

        public PortfolioController(UserManager<AppUser> userManager, IStockRepostory stockRepostory, IPortfolioRepository portfolioRepostory) 
        {
            _userManager = userManager;
            _stockRepo = stockRepostory;
            _portfolioRepo = portfolioRepostory;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio() 
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortafolio = await _portfolioRepo.GetUserPortafolio(appUser);
            return Ok(userPortafolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol) 
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepo.GetBySymbolAsync(symbol);
            if (stock == null) return NotFound("Stock Not Found");
            var userPortfolio = await _portfolioRepo.GetUserPortafolio(appUser);
            if (userPortfolio.Any(e => e.Symbol.ToLower() == symbol.ToLower())) return BadRequest("Cannot add same stock to portfolio ");
            var portfolioModel = new Portafolio
            {
                AppUserId = appUser.Id,
                StockId = stock.Id
            };
            await _portfolioRepo.CreateAsync(portfolioModel);
            if (portfolioModel == null) 
            {
                return StatusCode(500, "Could not create");
            }
            else 
            {
                return Created();
            }
        }
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string symbol) 
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            if (appUser == null) return NotFound("User not found");
            var userPortfolio = await _portfolioRepo.GetUserPortafolio(appUser);
            var filterStock = userPortfolio.Where(x => x.Symbol.ToLower().Equals(symbol.ToLower())).ToList();
            if (filterStock.Count() == 1) 
            {
                await _portfolioRepo.DeleteAsync(appUser, symbol);
            }else 
            {
                return BadRequest("Stock not in your portfolio");
            }
            return Ok();
        }
    }
}
