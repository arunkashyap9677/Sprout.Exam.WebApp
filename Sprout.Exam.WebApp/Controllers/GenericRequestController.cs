using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sprout.Exam.Common;
using Sprout.Exam.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Controllers
{
    [ApiController]
    public class GenericRequestController : ControllerBase
    {
        private GenericRequestController()
        {

        }

        public static IActionResult ServiceResponse<T>(ServiceResponse<T> response) 
        {
            GenericRequestController grc = new GenericRequestController();
            return grc.GetResponse(response);
        }

        private IActionResult GetResponse<T>(ServiceResponse<T> response) 
        {
            switch (response.ResponseStatus) 
            {
                case ResponseStatus.Success:
                    return Ok(response.Result);
                case ResponseStatus.NoContent:
                    return NoContent();
                case ResponseStatus.NotFound:
                    return NotFound();
                case ResponseStatus.Created:
                    return Created(response.Identifier, response.Result);
                default:
                    return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
