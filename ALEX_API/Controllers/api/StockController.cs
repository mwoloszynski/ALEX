using BLL.Services;
using COMMON.Helpers;
using DBMODEL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ALEX_API.Controllers.api
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : Controller
    {
        private readonly StockService service = new StockService();

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<List<Stock>> GetAll(int? page = null, int? pageSize = null)
        {
            return service.GetAll(page, pageSize);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<Stock> GetByCode(string code)
        {
            return service.GetByCode(x => x.Code == code);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<Stock> GetById(int id)
        {
            return service.GetById(id);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddStock(Stock stock)
        {
            var result = await service.Add(stock);
            if (result == Enums.Result.Success)
                return Ok();
            else
                return BadRequest();
        }
    }
}
