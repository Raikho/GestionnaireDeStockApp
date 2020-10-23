using System.Linq;
using DataLayer;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ODataController
    {
        readonly private StockContext _db;

        public UsersController(StockContext context)
        {
            _db = context;
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_db.Users);
        }

        [EnableQuery]
        public IActionResult Get(string key)
        {
            return Ok(_db.Users.FirstOrDefault(c => c.Username == key));
        }
    }
}
