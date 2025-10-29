using Microsoft.AspNetCore.Mvc;

namespace Store.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyErrorController :ControllerBase
    {
      [HttpGet("notfound")]
        public ActionResult GetNotFound()
        {
            return NotFound("This is not found result from buggy controller");
        }
        [HttpGet("NotFoundEndpoint")]
        public ActionResult GetNotFoundEndpoint()
        {
            return NotFound("This is not found result from buggy controller - NotFoundEndpoint");
        }
        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            throw new Exception("This is a server error from buggy controller");
        }
        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest("This is a bad request from buggy controller");
        }
        [HttpGet("unauthorized")]
        public ActionResult GetUnauthorized()
        {
            return Unauthorized();
        }
        [HttpGet("validationerror")]    
        public ActionResult GetValidationError()
        {
            ModelState.AddModelError("Problem1", "This is the first validation error");
            ModelState.AddModelError("Problem2", "This is the second validation error");
            return ValidationProblem();
        }
    }
}
