using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _context;
        public BuggyController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet("testauth")]
        [Authorize]
        public ActionResult<string> getSecretText()
        {
            return "secret stuff";
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundResult()
        {
            var product = _context.Products.Find(999);

            if(product == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok();
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var product = _context.Products.Find(999);

            var productToReturn = product.ToString();

            return Ok();
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetNotFoundResult(int id)
        {
            return Ok();
        }
    }
}