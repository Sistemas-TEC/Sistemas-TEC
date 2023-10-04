using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema_Tec_Web_API.Models;

namespace Sistema_Tec_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampusController : ControllerBase
    {
        private Sistema_TecContext _campusContext;

        public CampusController(Sistema_TecContext campusContext)
        {
            _campusContext = campusContext;

        }

        //GET API

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var campuses = await _campusContext.Campuses.ToListAsync();
            if (campuses == null) { return NotFound(); }

            return Ok(campuses);
        }
    }
}